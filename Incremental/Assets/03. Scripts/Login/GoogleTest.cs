using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using GooglePlayGames;

public class GoogleTest : MonoBehaviour
{
    public TMP_Text _TMP_Google = null;

    private void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Login();
    }

    void Login()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success) _TMP_Google.text = $"{Social.localUser.id} \n {Social.localUser.userName}";
            else _TMP_Google.text = "Failed";
        });
    }

    public void LogOut()
    {
        //((PlayGamesPlatform)Social.Active).SignOut();
    }
}
