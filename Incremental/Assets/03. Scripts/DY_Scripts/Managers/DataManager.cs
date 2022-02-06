using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    [Header("--- 접근 가능 데이터 ---")]
    [Tooltip("Dictionary<string, object> - 몬스터 정보 데이터")]
    public Dictionary<string, object> _dic_monsters = null;

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
        string getMonsters = Managers.ResourceM.Load<TextAsset>("DataManager", "Data/Monsters").ToString();
        _dic_monsters = Utils.JsonToObject(getMonsters) as Dictionary<string, object>;
    }
}
