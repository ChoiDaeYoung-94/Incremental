using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;

public class ServerManager
{
    #region 
    /// <summary>
    /// 초기 데이터가 없을 경우 기본 데이터 세팅
    /// </summary>
    internal void SetBasicData()
    {
        var request = new UpdateUserDataRequest() { Data = Managers.DataM._dic_player, Permission = UserDataPermission.Public };
        PlayFabClientAPI.UpdateUserData(request,
            (result) => GetAllData(),
            (error) => Debug.LogWarning("Failed to SetBasicData with PlayFab"));
    }

    /// <summary>
    /// 서버에서 데이터 가져오기
    /// </summary>
    internal void GetAllData()
    {
        var request = new GetUserDataRequest() { PlayFabId = Managers.DataM.GetPlayerID() };
        PlayFabClientAPI.GetUserData(request,
            (result) =>
            {
                Managers.DataM._dic_PlayFabPlayerData = result.Data;
                Managers.DataM.CheckBasicData();
            },
            (error) => Debug.LogWarning($"Failed to GetData with PlayFab: {error}"));
    }

    /// <summary>
    /// 서버에 데이터 저장
    /// </summary>
    /// <param name="dic"></param>
    internal void SetData(Dictionary<string, string> dic)
    {
        var request = new UpdateUserDataRequest() { Data = dic, Permission = UserDataPermission.Public };
        PlayFabClientAPI.UpdateUserData(request,
            (result) => Debug.Log("Success to SetData with PlayFab"),
            (error) => Debug.LogWarning("Failed to SetData with PlayFab"));
    }
    #endregion
}
