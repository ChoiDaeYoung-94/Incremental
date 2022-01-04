using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    [Header("--- 세팅 ---")]
    [SerializeField, Tooltip("어떤 몬스터 인지")]
    Define.Monsters _monster = Define.Monsters.Base;

    [Header("--- 참고용 ---")]
    [SerializeField, Tooltip("몬스터 체력")]
    protected int _hp = 0;
    [SerializeField, Tooltip("몬스터 스피드")]
    protected int _speed = 0;
    [SerializeField, Tooltip("몬스터 생성 위치")]
    protected Vector2 _startPos = new Vector2();

    /// <summary>
    /// 몬스터 정보 DataM에서 받고 초기화
    /// </summary>
    protected virtual void Init()
    {
        Dictionary<string, object> cur_monster = Managers.DataM._dic_monsters[_monster.ToString()] as Dictionary<string, object>;

        if (!int.TryParse(cur_monster["Hp"].ToString(), out _hp))
            DebugError.Parse("MonsterBase", _monster.ToString() + " - Hp");

        if (!int.TryParse(cur_monster["Speed"].ToString(), out _speed))
            DebugError.Parse("MonsterBase", _monster.ToString() + " - Speed");
    }

    public abstract void Clear();
}
