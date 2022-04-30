// 추후 IOS때 사용 할 GO의 Warning
#pragma warning disable 0414

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_ANDROID
using GooglePlayGames;
#endif

using PlayFab;
using PlayFab.ClientModels;

public class Login : MonoBehaviour
{
    [Header("--- 세팅 ---")]
    [SerializeField, Tooltip("GO - Android Login Button")]
    GameObject _go_GooglePlay = null;
    [SerializeField, Tooltip("GO - IOS Login Button")]
    GameObject _go_GameCenter = null;
    [SerializeField, Tooltip("GO - Loading")]
    GameObject _go_Loading = null;
    [SerializeField, Tooltip("GO - Retry")]
    GameObject _go_Retry = null;
    [SerializeField, Tooltip("TMP - Load")]
    TMPro.TMP_Text _TMP_load = null;
    [SerializeField, Tooltip("GO - NickName")]
    GameObject _go_NickName = null;
    [SerializeField, Tooltip("TMP - NickName")]
    TMPro.TMP_Text _TMP_NickName = null;
    [SerializeField, Tooltip("GO - Warning")]
    GameObject _go_Warning = null;

    [Header("--- 참고용 ---")]
    Coroutine _co_Login = null;

    private void Awake()
    {
#if UNITY_ANDROID
        //_go_GameCenter.SetActive(false);

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
#endif

#if UNITY_IOS
        //_go_GooglePlay.SetActive(false);
#endif
    }

    private void Start()
    {
        _TMP_load.text = "LogIn...";
        CheckConnection();
    }

    #region Functions

    #region Checking Connection
    /// <summary>
    /// 인터넷 연결 확인 후 로그인 진입
    /// * Retry 버튼 클릭시 연결 재시도
    /// </summary>
    public void CheckConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            ActivateRetry();
        else
            StartLogin();
    }

    /// <summary>
    /// Retry Panel 활성화
    /// </summary>
    void ActivateRetry()
    {
        _go_Loading.SetActive(false);
        _go_Retry.SetActive(true);
    }

    /// <summary>
    /// 로그인 진입
    /// </summary>
    void StartLogin()
    {
        _go_Retry.SetActive(false);
        _go_Loading.SetActive(true);

#if UNITY_EDITOR
        LoginWithTestAccount();
#elif UNITY_ANDROID
        LoginWithGoogle();
#endif
    }
    #endregion

    #region Login & SignUp
    void LoginWithGoogle()
    {
        if (Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success, string error) =>
            {
                if (success)
                {
                    Debug.Log("Success LoginWithGoogle");
                    LoginWithPlayFab();
                }
                else
                {
                    Debug.LogWarning($"Failed LoginWithGoogle -> {error}");
                    _TMP_load.text = "Failed LoginWithGoogle... :'(";
                }
            });
        }
    }

    void LoginWithPlayFab()
    {
        string id = $"{Social.localUser.id}@AeDeong.com";

        var request = new LoginWithEmailAddressRequest { Email = id, Password = Social.localUser.userName };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginWithPlayFabSuccess, OnLoginWithPlayFabFailure);
    }

    void OnLoginWithPlayFabSuccess(LoginResult result)
    {
        Debug.Log("Success LoginWithPlayFab");
        _TMP_load.text = "Success!!";

        Managers.DataM.SetPlayerID(result.PlayFabId);

        GoGame();
    }

    void OnLoginWithPlayFabFailure(PlayFabError error)
    {
        Debug.Log("Failed LoginWithPlayFab -> SignUpWithPlayFab");

        // nickname 먼저 설정
        _go_NickName.SetActive(true);
    }

    /// <summary>
    /// Panel_NickName -> Btn_Confirm
    /// </summary>
    public void CheckNickName()
    {
        string str_temp = _TMP_NickName.text;

        if (string.IsNullOrEmpty(str_temp) || str_temp.Contains(" ") || str_temp.Length < 2 || str_temp.Length > 10)
            _go_Warning.SetActive(true);
        else
        {
            _go_NickName.SetActive(false);
            _go_Warning.SetActive(false);

            Managers.DataM._str_NickName = str_temp;
            SignUpWithPlayFab();
        }
    }

    void SignUpWithPlayFab()
    {
        string id = $"{Social.localUser.id}@AeDeong.com";

        var request = new RegisterPlayFabUserRequest { Email = id, Password = Social.localUser.userName, Username = Social.localUser.userName };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterWithPlayFabSuccess, OnRegisterWithPlayFabFailure);
    }

    void OnRegisterWithPlayFabSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Success SignUpWithPlayFab");
        _TMP_load.text = "Success!!";

        Managers.DataM.SetPlayerID(result.PlayFabId);

        GoGame();
    }

    void OnRegisterWithPlayFabFailure(PlayFabError error)
    {
        Debug.LogWarning($"Failed SignUpWithPlayFab -> {error}");
        _TMP_load.text = "Failed SignUpWithPlayFab... :'(";
    }
    #endregion

    #region LoginWithTestAccount
    void LoginWithTestAccount()
    {
        var request = new LoginWithEmailAddressRequest { Email = "Test@AeDeong.com", Password = "TestAccount" };
        PlayFabClientAPI.LoginWithEmailAddress(request,
            (success) =>
            {
                Managers.DataM.SetPlayerID(success.PlayFabId);
                GoGame();
            },
            (failed) => SignUpWithTestAccount());
    }

    void SignUpWithTestAccount()
    {
        var request = new RegisterPlayFabUserRequest { Email = "Test@AeDeong.com", Password = "TestAccount", Username = "TestAccount" };
        PlayFabClientAPI.RegisterPlayFabUser(request,
            (success) =>
            {
                Managers.DataM.SetPlayerID(success.PlayFabId);
                GoGame();
            },
            (failed) => Debug.Log("Failed SignUpWithTestAccount"));
    }
    #endregion

    #region ETC
    void GoGame()
    {
        _TMP_load.text = "Check Data...";

        Managers.DataM.InitPlayerData();

        _co_Login = StartCoroutine(InitPlayerData());
    }

    IEnumerator InitPlayerData()
    {
        while (!Managers.DataM._isFinished)
            yield return null;

        StopInitPlayerDataCoroutine();
    }

    void StopInitPlayerDataCoroutine()
    {
        if (_co_Login != null)
        {
            StopCoroutine(_co_Login);
            _co_Login = null;

            SceneManager.LoadScene("Game");
            Resources.UnloadUnusedAssets();
        }
    }
    #endregion

    #endregion

#if UNITY_EDITOR
    [CustomEditor(typeof(Login))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Login with PlayFab", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}