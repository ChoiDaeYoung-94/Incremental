using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    [Header("--- 접근 가능 데이터 ---")]
    [Tooltip("Dictionary<string, object> - 플레이어 초기 데이터")]
    public Dictionary<string, string> _dic_player = null;
    [Tooltip("Dictionary<string, object> - 몬스터 정보 데이터")]
    public Dictionary<string, object> _dic_monsters = null;

    [Header("--- 참고용 ---")]
    [SerializeField, Tooltip("현재 Player가 PlayFab에 접속한 ID")]
    string _str_ID = string.Empty;

    [Header("--- 임시용 ---")]
    public int _ply_level = 10;
    public int _ply_power = 30;
    public float _ply_attackSpeed = 0.5f;

    /// <summary>
    /// Managers - Awake() -> Init()
    /// 필요한 데이터 미리 받아 둠
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

    /// <summary>
    /// 서버에 존재하는 플레이어 데이터 초기화
    /// * 값이 존재하지 않는 경우에는 최솟값 등록
    /// </summary>
    internal void InitPlayerData()
    {
        Managers.ServerM.CheckBasicData();
    }
    #endregion
}
