using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    [Header("--- 참고용 ---")]
    [Tooltip("Dictionary<string, object> - 몬스터 정보 데이터")]
    public Dictionary<string, object> _dic_monsters = null;

    /// <summary>
    /// Managers - Awake() -> Init()
    /// 필요한 데이터 미리 받아 둠
    /// </summary>
    public void Init()
    {
        string getMonsters = Managers.ResourceM.Load<TextAsset>("DataManager", "Data/Monsters").ToString();
        _dic_monsters = Utils.JsonToObject(getMonsters) as Dictionary<string, object>;
    }
}
