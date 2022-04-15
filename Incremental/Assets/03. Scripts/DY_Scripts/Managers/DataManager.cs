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
    [Tooltip("현재 Player가 설정한 NickName")]
    internal string _str_NickName = string.Empty;
    [Tooltip("접속 후 PlayerData 를 다 받고 세팅이 끝났는지 여부 확인")]
    internal bool _isFinished = false;

    [Header("--- 참고용 [ 플레이어 데이터 미리 가공 ] ---")]
    internal int _ply_gold = 0;
    internal int _ply_diamond = 0;
    internal int _ply_level = 0;
    internal long _ply_experience = 0;
    internal float _ply_power = 0;
    internal float _ply_attackSpeed = 0;
    internal int _ply_str = 0;
    internal int _ply_bgIndex = 0;

    [Header("--- 참고용 [ 플레이어 데이터를 통해 임시 계산 ] ---")]
    [SerializeField, Tooltip("float - 플레이어의 현재 레벨에 필요한 총 경험치")]
    internal long _totalExp = 0;

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
            SetPlayerData();
    }

    /// <summary>
    /// Player Data 미리 가공
    /// </summary>
    void SetPlayerData()
    {
        if (string.IsNullOrEmpty(_str_NickName))
            _str_NickName = _dic_PlayFabPlayerData["NickName"].Value;
        else
            Managers.ServerM.SetData(new Dictionary<string, string> { { "NickName", _str_NickName } });

        _ply_gold = int.Parse(_dic_PlayFabPlayerData["Gold"].Value);
        _ply_diamond = int.Parse(_dic_PlayFabPlayerData["Diamond"].Value);
        _ply_level = int.Parse(_dic_PlayFabPlayerData["Level"].Value);
        _ply_experience = long.Parse(_dic_PlayFabPlayerData["Experience"].Value);
        _ply_power = float.Parse(_dic_PlayFabPlayerData["Power"].Value);
        _ply_attackSpeed = float.Parse(_dic_PlayFabPlayerData["AttackSpeed"].Value);
        _ply_str = int.Parse(_dic_PlayFabPlayerData["STR"].Value);
        _ply_bgIndex = int.Parse(_dic_PlayFabPlayerData["BgIndex"].Value);

        GetTotalExp();

        _isFinished = true;
    }
    #endregion

    #region PlayerData
    /// <summary>
    /// 현재 레벨에서 필요한 경험치 계산
    /// * 따로 경험치 테이블 안 만듬 -> 임시 계산식...
    /// </summary>
    void GetTotalExp()
    {
        int totalExp = 0;

        for (int i = 0; ++i <= _ply_level;)
            totalExp += i * 10 + (i * 5) * 10;

        _totalExp = totalExp;
    }

    internal string ExpToPercentage()
    {
        float percentage = _ply_experience / _totalExp * 100f;

        return $"{string.Format("{0:0.00}", percentage)} %";
    }

    internal float GetPlayerData(DY.Define.Stat stat)
    {
        if (stat == DY.Define.Stat.Power)
            return _ply_power;
        if (stat == DY.Define.Stat.AttackSpeed)
            return _ply_attackSpeed;

        return 0f;
    }

    internal object SetPlayerData(DY.Define.Stat stat, float plus)
    {
        if (stat == DY.Define.Stat.Power)
            return _ply_power += (int)plus;
        if (stat == DY.Define.Stat.AttackSpeed)
            return _ply_attackSpeed += plus;

        return null;
    }

    bool isChange = false;
    internal void LevelUpCheck()
    {
        if (_ply_experience >= _totalExp)
        {
            _ply_experience -= _totalExp;
            ++_ply_level;

            Panel_characterStatus.Instance._panel_playerInfo.Init();
            Top_Menu.Instance.SetLv();

            if (_ply_level % 10 == 0)
            {
                if (++_ply_bgIndex > BgManage.Instance._maxIndex)
                    _ply_bgIndex = 0;

                BgManage.Instance.SetBg(_ply_bgIndex);
                isChange = true;
            }
        }
    }

    internal void UpdateLevel()
    {
        Managers.ServerM.
            SetData(new Dictionary<string, string> { { "Level", _ply_level.ToString() }, { "Experience", _ply_experience.ToString() } });

        if (isChange)
        {
            Managers.ServerM.SetData(new Dictionary<string, string> { { "BgIndex", _ply_bgIndex.ToString() } });
            isChange = false;
        }
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