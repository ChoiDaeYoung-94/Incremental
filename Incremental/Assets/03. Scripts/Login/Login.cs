#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using PlayFab;
using PlayFab.ClientModels;

public class Login : MonoBehaviour
{
    [Header("--- 세팅 ---")]
    [SerializeField, Tooltip("TMP_Text - Email_ID")]
    TMP_Text _IF_emailID = null;
    [SerializeField, Tooltip("TMP_Text - Email_passward")]
    TMP_Text _IF_emailPassward = null;

    public void LoginTest()
    {
        var request = new LoginWithEmailAddressRequest { Email = _IF_emailID.text, Password = _IF_emailPassward.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    public void SignUpTest()
    {
        var request = new RegisterPlayFabUserRequest { Email = _IF_emailID.text, Password = _IF_emailPassward.text, Username = "testsss" };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("로그인 성공");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("로그인 실패");
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원가입 성공");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogWarning("회원가입 실패");
    }

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
