using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab.ClientModels;

public class DataManager
{
    [Header("--- 접근 가능 데이터 ---")]
    [Tooltip("Dictionary<string, object> - 플레이어 초기 데이터")]
    public Dictionary<string, string> _dic_player = null;
    [Tooltip("Dictionary<string, object> - 몬스터 정보 데이터")]
    public Dictionary<string, object> _dic_monsters = null;
    [Tooltip("Dictionary<string, UserDataRecord> - PlayFab에서 받아온 PlayerData")]
    public Dictionary<string, UserDataRecord> _dic_PlayFabPlayerData = null;

    [Header("--- 참고용 ---")]
    [Tooltip("현재 Player가 PlayFab에 접속한 ID")]
    string _str_ID = string.Empty;
    [Tooltip("접속 후 PlayerData 를 다 받고 세팅이 끝났는지 여부 확인")]
    internal bool _isFinished = false;

    [Header("--- 임시용 ---")]
    public int _ply_level = 10;
    public int _ply_power = 30;
    public float _ply_attackSpeed = 0.5f;

    /// <summary>
    /// Managers - Awake() -> Init()
    /// 필요한 기본 데이터 미리 받아 둠
    /// </summary>
    public void Init()
    {
        _dic_player = new Dictionary<string, string>();

        string getPlayer = Managers.ResourceM.Load<TextAsset>("DataManager", "Data/Player").ToString();
        Dictionary<string, object> dic_temp = Utils.JsonToObject(getPlayer) as Dictionary<string, object>;
        foreach (KeyValuePair<string, object> content in dic_temp)
            _dic_player.Add(content.Key, content.Value.ToString());

        string getMonsters = Managers.ResourceM.Load<TextAsset>("DataManager", "Data/Monsters").ToString();
        _dic_monsters = Utils.JsonToObject(getMonsters) as Dictionary<string, object>;
    }

    #region Functions

    #region Server Data Checking
    /// <summary>
    /// 서버에 존재하는 플레이어 데이터 초기화
    /// * 게임 씬 진입 전 호출 됨
    /// </summary>
    internal void InitPlayerData()
    {
        Managers.ServerM.GetAllData();
    }

    /// <summary>
    /// 서버에서 받아온 데이터 체킹 후
    /// * 데이터가 존재하지 않을 경우 초기화 진행
    /// * 기본 Player데이터 보다 적을 경우 [ 어딘가에서 꼬인 느낌이라 ]
    /// </summary>
    internal void CheckBasicData()
    {
        if (_dic_PlayFabPlayerData == null || _dic_PlayFabPlayerData.Count < _dic_player.Count)
            Managers.ServerM.SetBasicData();
        else
            _isFinished = true;
    }
    #endregion

    #region ETC
    /// <summary>
    /// PlayerID string 형식으로 적용
    /// </summary>
    internal void SetPlayerID(string id)
    {
        _str_ID = id;
    }

    /// <summary>
    /// PlayerId string 형식으로 반환
    /// </summary>
    /// <returns></returns>
    internal string GetPlayerID()
    {
        return _str_ID;
    }
    #endregion

    #endregion
}
