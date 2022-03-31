using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Events;

public class GoogleTest : MonoBehaviour
{
    public TMP_Text _TMP_Google = null;

    private void Awake()
    {


    }

    private void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Login();
    }

    void Login()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated == false) //구글 콘솔 로그인이 되어 있지 않을 때
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success) _TMP_Google.text = $"{Social.localUser.id} \n {Social.localUser.userName}";
                else _TMP_Google.text = "Failed";
            });
        }


    }

    public void LogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }
}
