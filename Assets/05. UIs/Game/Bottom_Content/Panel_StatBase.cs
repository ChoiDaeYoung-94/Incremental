using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Panel_StatBase : MonoBehaviour
{
    [Header("--- 세팅 ---")]
    [SerializeField, Tooltip("어떤 Stat 인지")]
    DY.Define.Stat _stat = DY.Define.Stat.Base;
    [SerializeField, Tooltip("TMP - StatLevel")]
    TMP_Text _TMP_statLevel = null;
    [SerializeField, Tooltip("TMP - StatInformation")]
    TMP_Text _TMP_statInformation = null;
    [SerializeField, Tooltip("TMP - StatRequiredMoney")]
    TMP_Text _TMP_statRequiredMoney = null;
    [SerializeField, Tooltip("int - 이 stat의 MaxLevel")]
    int _maxLevel = 0;

    [Header("--- 참고용 ---")]
    int curLevel = 0;
    int requiredmoney = 0;
    float plus = 0;
    float curStat = 0;

    internal void Init()
    {
        _TMP_statLevel.text = GetLevel();
        _TMP_statInformation.text = GetInformation();
        _TMP_statRequiredMoney.text = RequiredMoney();
    }

    string GetLevel()
    {
        if (curLevel != 0)
            ++curLevel;
        else
        {
            var curStat = Managers.DataM.GetPlayerData(_stat);
            float orgStat = float.Parse(Managers.DataM._dic_player[_stat.ToString()]);

            if (_stat == DY.Define.Stat.AttackSpeed)
                curLevel = (int)(((float)curStat - orgStat) / DY.Define._plusAttackSpeedStat);
            else
                curLevel = (int)(((int)curStat - orgStat) / DY.Define._plusStat);
        }

        return $"Lv. {curLevel}";
    }

    string GetInformation()
    {
        if (_stat == DY.Define.Stat.AttackSpeed)
            plus = DY.Define._plusAttackSpeedStat;
        else
            plus = DY.Define._plusStat;

        curStat = (float)Managers.DataM.SetPlayerData(_stat, plus);

        return $"{curStat} -> {curStat + plus}";
    }

    string RequiredMoney()
    {
        requiredmoney = curLevel * 100;
        return string.Format("{0:#,##0}", curLevel * 100);
    }

    internal void UpdateStat()
    {
        if (Managers.DataM._ply_gold >= requiredmoney && curLevel < _maxLevel)
        {
            Managers.DataM._ply_gold -= requiredmoney;
            Middle_Menu.Instance.StartInit();
            
            Init();
        }
    }

    internal void UpdateServerStat()
    {
        Managers.ServerM.SetData(new Dictionary<string, string> { { "Gold", Managers.DataM._ply_gold.ToString() } });

        Managers.ServerM.SetData(new Dictionary<string, string> { { _stat.ToString(), curStat.ToString() } });
    }
}
