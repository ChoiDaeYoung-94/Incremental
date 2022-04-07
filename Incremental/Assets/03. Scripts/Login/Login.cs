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
                    Debug.LogWarning($"Failed LoginWithGoogle -> {error}");
            });
        }
    }

    void LoginWithPlayFab()
    {
        string id = Social.localUser.userName + "@AeDeong.com";

        Managers.DataM.SetPlayerID(id);

        var request = new LoginWithEmailAddressRequest { Email = id, Password = Social.localUser.id };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginWithPlayFabSuccess, OnLoginWithPlayFabFailure);
    }

    void OnLoginWithPlayFabSuccess(LoginResult result)
    {
        Debug.Log("Success LoginWithPlayFab");

        GoGame();
    }

    void OnLoginWithPlayFabFailure(PlayFabError error)
    {
        Debug.Log("Failed LoginWithPlayFab -> SignUpWithPlayFab");

        SignUpWithPlayFab();
    }

    void SignUpWithPlayFab()
    {
        var request = new RegisterPlayFabUserRequest { Email = Social.localUser.userName + "@AeDeong.com", Password = Social.localUser.id, Username = Social.localUser.userName };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterWithPlayFabSuccess, OnRegisterWithPlayFabFailure);
    }
    void OnRegisterWithPlayFabSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Success SignUpWithPlayFab");

        GoGame();
    }

    void OnRegisterWithPlayFabFailure(PlayFabError error)
    {
        Debug.LogWarning($"Failed SignUpWithPlayFab -> {error}");
    }
    #endregion

    #region LoginWithTestAccount
    void LoginWithTestAccount()
    {
        Managers.DataM.SetPlayerID("Test@AeDeong.com");

        var request = new LoginWithEmailAddressRequest { Email = "Test@AeDeong.com", Password = "TestAccount" };
        PlayFabClientAPI.LoginWithEmailAddress(request, (success) => GoGame(), (failed) => SignUpWithTestAccount());
    }

    void SignUpWithTestAccount()
    {
        var request = new RegisterPlayFabUserRequest { Email = "Test@AeDeong.com", Password = "TestAccount", Username = "TestAccount" };
        PlayFabClientAPI.RegisterPlayFabUser(request, (success) => GoGame(), (failed) => Debug.Log("Failed SignUpWithTestAccount"));
    }
    #endregion

    #region ETC
    void GoGame()
    {
        _TMP_load.text = "Check Data...";

        Managers.DataM.InitPlayerData();

        SceneManager.LoadScene("Game");
        Resources.UnloadUnusedAssets();
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