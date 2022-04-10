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
    [Header("--- 세팅(추후 Android, IOS 둘 다 진행 시) ---")]
    public GameObject _go_GooglePlay = null;
    public GameObject _go_GameCenter = null;
    public TMPro.TMP_Text _TMP_load = null;

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
#if UNITY_EDITOR
        LoginWithTestAccount();
#elif UNITY_ANDROID
        LoginWithGoogle();
#endif
    }

    #region Functions

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
                    _TMP_load.text = "Failed... :'(";
                }
            });
        }
    }

    void LoginWithPlayFab()
    {
        string id = $"{Social.localUser.userName}@AeDeong.com";

        var request = new LoginWithEmailAddressRequest { Email = id, Password = Social.localUser.id };
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

        SignUpWithPlayFab();
    }

    void SignUpWithPlayFab()
    {
        string id = $"{Social.localUser.userName}@AeDeong.com";

        var request = new RegisterPlayFabUserRequest { Email = id, Password = Social.localUser.id, Username = Social.localUser.userName };
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
        _TMP_load.text = "Failed... :'(";
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