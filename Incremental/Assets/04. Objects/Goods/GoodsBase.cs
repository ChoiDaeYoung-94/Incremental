using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoodsBase : MonoBehaviour
{
    [Header("--- 세팅 ---")]
    [SerializeField, Tooltip("어떤 아이템 인지")]
    Define.DropItems _item = Define.DropItems.Base;

    [Header("--- 미리 받아야 할 data ---")]
    [SerializeField, Tooltip("몬스터가 죽고 드롭이 시작 될 Transfrom -> tr 받고 position만 사용")]
    protected Transform _tr_start = null;
    [SerializeField, Tooltip("점프 최종 position.y -> 점프 끝난 뒤 위아래 연출 위함")]
    protected float _pos_y = 0f;
    [SerializeField, Tooltip("점프 뛸 거리 - end position")]
    protected Vector3 _vec3_end = Vector3.zero;
    [SerializeField, Tooltip("점프 파워")]
    protected float _jumpPower = 0f;
    [SerializeField, Tooltip("점프 횟수")]
    protected int _jumpCount = 0;
    [SerializeField, Tooltip("점프 duration")]
    protected float _jumpDuration = 0f;

    [Header("--- 정해진 값 ---")]
    [SerializeField, Tooltip("점프 끝난 뒤 위아래 연출 range")]
    protected float _pos_y_range = 0.05f;

    #region Functions
    public void SettingBase(Transform tr_start)
    {
        _tr_start = tr_start;
        transform.position = _tr_start.position;

        // 추후 경험치 등 생기면 switch로
        if (_item == Define.DropItems.Gold)
        {
            transform.position = new Vector3(transform.position.x, 0.32f, transform.position.z);
        }

        _pos_y = transform.position.y;

        float _pos_x = Random.Range(1f, 3f);
        _vec3_end = transform.position + new Vector3(_pos_x, 0, 0);

        _jumpPower = Random.Range(1f, 3f); // 임시

        _jumpCount = Random.Range(1, 4); // 임시

        _jumpDuration = Random.Range(1f, 3f); // 임시
    }
    #endregion

    public abstract void Clear();
}
