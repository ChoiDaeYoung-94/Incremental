#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

using GooglePlayGames;

using PlayFab;
using PlayFab.ClientModels;

public class Login : MonoBehaviour
{
    [Header("--- 세팅(추후 Android, IOS 둘 다 진행 시) ---")]
    public GameObject _go_GooglePlay = null;
    public GameObject _go_GameCenter = null;

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
#if UNITY_ANDROID
        LoginWithGoogle();
#endif
    }

    #region Functions

    #region Login & SignUp
    public void LoginWithGoogle()
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
        var request = new LoginWithEmailAddressRequest { Email = Social.localUser.userName + "@AeDeong.com", Password = Social.localUser.id };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginWithPlayFabSuccess, OnLoginWithPlayFabFailure);
    }

    private void OnLoginWithPlayFabSuccess(LoginResult result)
    {
        Debug.Log("Success LoginWithPlayFab");

        GoGame();
    }

    private void OnLoginWithPlayFabFailure(PlayFabError error)
    {
        Debug.Log("Failed LoginWithPlayFab -> SignUpWithPlayFab");

        SignUpWithPlayFab();
    }

    public void SignUpWithPlayFab()
    {
        var request = new RegisterPlayFabUserRequest { Email = Social.localUser.userName + "@AeDeong.com", Password = Social.localUser.id, Username = Social.localUser.userName };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterWithPlayFabSuccess, OnRegisterWithPlayFabFailure);
    }
    private void OnRegisterWithPlayFabSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Success SignUpWithPlayFab");

        GoGame();
    }

    private void OnRegisterWithPlayFabFailure(PlayFabError error)
    {
        Debug.LogWarning($"Failed SignUpWithPlayFab -> {error}");
    }
    #endregion

    #region ETC
    void GoGame()
    {
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