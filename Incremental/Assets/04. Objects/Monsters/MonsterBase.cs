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

    protected virtual void Init()
    {
        Debug.Log(_monster.ToString());
    }

    public abstract void Clear();
}
