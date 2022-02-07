using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MonsterBase : MonoBehaviour
{
    [Header("--- 세팅 ---")]
    [SerializeField, Tooltip("어떤 몬스터 인지")]
    Define.Monsters _monster = Define.Monsters.Base;
    [SerializeField, Tooltip("이동 속도 - 다 통일")]
    protected float _speed = 0.6f;

    [Header("--- 미리 가지고 있어야 할 data ---")]
    [SerializeField, Tooltip("HP표기 위한 slider")]
    protected Slider _sld_hp = null;

    [Header("--- 참고용 ---")]
    [SerializeField, Tooltip("몬스터 original 체력")]
    protected int _org_hp = 0;
    [SerializeField, Tooltip("몬스터 now 체력")]
    protected int _hp = 0;
    [SerializeField, Tooltip("몬스터 original position")]
    protected Vector3 _position = Vector3.zero;

    /// <summary>
    /// 몬스터 정보 DataM에서 받고 초기화
    /// </summary>
    protected virtual void Init()
    {
        Dictionary<string, object> cur_monster = Managers.DataM._dic_monsters[_monster.ToString()] as Dictionary<string, object>;

        if (!int.TryParse(cur_monster["Hp"].ToString(), out _hp))
            DebugError.Parse("MonsterBase", _monster.ToString() + " - Hp");
        else
        {
            _sld_hp.value = _sld_hp.maxValue = _org_hp = _hp;
        }

        _position = transform.position;
    }

    public abstract void Clear();
}
