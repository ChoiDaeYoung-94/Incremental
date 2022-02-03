using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("--- 참고용 ---")]
    [SerializeField, Tooltip("전투 시 배경 및 몬스터 움직임 제어 + 플레이어 전투 돌입 체크")]
    bool isBattle = false;
    [SerializeField, Tooltip("제어하고 있는 몬스터 amount")]
    int _monsterAmount = 0;

    #region Functions
    public void StartBattle()
    {
        isBattle = true;
    }

    public void EndBattle()
    {
        isBattle = false;
    }

    public bool CheckBattle()
    {
        if (isBattle)
            return true;
        else
            return false;
    }

    public void PlusMonster()
    {
        _monsterAmount += 1;
    }

    public void MinusMonster()
    {
        _monsterAmount -= 1;

        if (_monsterAmount <= 0)
            _monsterAmount = 0;
    }

    public bool CheckMonsterAmount()
    {
        if (_monsterAmount <= 0)
        {
            _monsterAmount = 0;
            return true;
        }
        else
            return false;
    }
    #endregion
}
