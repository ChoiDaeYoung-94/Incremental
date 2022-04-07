using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;

public class ServerManager
{
    #region 
    /// <summary>
    /// 플레이어의 초기 데이터 존재 유무 확인
    /// </summary>
    internal void CheckBasicData()
    {
        if (string.IsNullOrEmpty(GetData("Level", forceAdd: false)))
            SetData(Managers.DataM._dic_player);
    }

    /// <summary>
    /// 서버에서 데이터 가져오기
    /// * 만약 데이터가 없을 경우 최솟값으로 적용
    /// </summary>
    internal string GetData(string key, bool forceAdd = true)
    {
        string value = string.Empty;

        var request = new GetUserDataRequest() { PlayFabId = Managers.DataM.GetPlayerID() };
        PlayFabClientAPI.GetUserData(request, (result) => value = result.Data[key].Value,
            (error) =>
            {
                Debug.LogWarning($"Failed to GetData with PlayFab - key: {key}");

                if (forceAdd)
                    SetData(new Dictionary<string, string>() { { key, "0" } });
            });

        return value;
    }

    /// <summary>
    /// 서버에 데이터 저장
    /// </summary>
    /// <param name="dic"></param>
    internal void SetData(Dictionary<string, string> dic)
    {
        var request = new UpdateUserDataRequest() { Data = dic, Permission = UserDataPermission.Public };
        PlayFabClientAPI.UpdateUserData(request, (result) => Debug.Log("Success to SetData with PlayFab"), (error) => Debug.LogWarning("Failed to SetData with PlayFab"));
    }
    #endregion
}
