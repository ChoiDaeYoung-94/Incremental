using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Run,
        Attack
    }

    [SerializeField] PlayerState _plyState = PlayerState.Run;

    public PlayerState PlyState
    {
        get { return _plyState; }
        set
        {
            _plyState = value;

            switch (_plyState)
            {
                case PlayerState.Run:
                    _playerAni.CrossFade("Run", 0.5f);
                    break;
                case PlayerState.Attack:
                    _playerAni.CrossFade("Attack", 0.1f);
                    break;
            }
        }
    }

    [Header("--- 미리 가지고 있어야 할 data ---")]
    [SerializeField, Tooltip("플레이어 Animator")]
    Animator _playerAni = null;

    [Header("--- 참고용 ---")]
    [SerializeField, Tooltip("플레이어 레벨")]
    int _level = 0;
    [SerializeField, Tooltip("플레이어 공격력")]
    float _power = 0;
    [SerializeField, Tooltip("플레이어 공격 속도")]
    float _attackSpeed = 0;
    [SerializeField, Tooltip("공격 대상 몬스터 script")]
    Monster _curMonster = null;

    /// <summary>
    /// Initialize_Game.cs 에서 호출
    /// </summary>
    public void StartInit()
    {
        _plyState = PlayerState.Run;

        _level = Managers.DataM._ply_level;
        _power = Managers.DataM._ply_power;
        _attackSpeed = Managers.DataM._ply_attackSpeed;

        _playerAni.SetFloat("AttackSpeed", _attackSpeed);
    }

    private void Update()
    {
        switch (_plyState)
        {
            case PlayerState.Run:
                Run();
                break;
            case PlayerState.Attack:
                Attack();
                break;
        }
    }

    #region Functions
    /// <summary>
    /// Event in player ani(Attack)
    /// </summary>
    void Attack_Ani()
    {
        if (_curMonster != null)
        {
            _curMonster.Hit((int)Managers.DataM._ply_power);
        }
    }

    void Attack()
    {
        if (!Managers.GameM.CheckBattle())
        {
            if (PlyState == PlayerState.Attack)
                PlyState = PlayerState.Run;
        }
    }

    void Run()
    {
        if (Managers.GameM.CheckBattle())
        {
            if (PlyState == PlayerState.Run)
                PlyState = PlayerState.Attack;
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Monster"))
        {
            Managers.GameM.StartBattle();
            _curMonster = col.GetComponent<Monster>();
        }

        if (col.CompareTag("DropItem"))
        {
            col.GetComponent<Goods>().GetItems();
        }
    }
}
