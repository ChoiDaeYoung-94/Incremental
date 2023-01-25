using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoodsBase : MonoBehaviour
{
    [Header("--- 세팅 ---")]
    [SerializeField, Tooltip("어떤 아이템 인지")]
    protected DY.Define.DropItems _item = DY.Define.DropItems.Base;
    [SerializeField, Tooltip("이동 속도 - 다 통일")]
    protected float _speed = 0.6f;

    [Header("--- 미리 받아야 할 data ---")]
    [SerializeField, Tooltip("몬스터가 죽고 드롭이 시작 될 Transfrom -> tr 받고 position만 사용")]
    protected Transform _tr_start = null;
    [SerializeField, Tooltip("점프 최종 position.x")]
    protected float _pos_x = 0f;
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
    [SerializeField, Tooltip("초기 position")]
    protected Vector3 _vec3_original = new Vector3(100f, 0f, 0f);
    [SerializeField, Tooltip("점프 끝난 뒤 위아래 연출 range")]
    protected float _pos_y_range = 0.05f;

    #region Functions
    public void SettingBase(Transform tr_start)
    {
        _tr_start = tr_start;
        transform.position = _tr_start.position;

        // 추후 경험치 등 생기면 switch로 (아이템 크기에 따라 Y값 조절 위함)
        if (_item == DY.Define.DropItems.Gold)
        {
            transform.position = new Vector3(transform.position.x, 0.32f, transform.position.z);
        }

        _pos_y = transform.position.y;

        int num = Random.Range(1, 4);
        RandomSettingValue(num);

        _vec3_end = transform.position + new Vector3(_pos_x, 0, 0);
    }

    void RandomSettingValue(int num)
    {
        switch (num)
        {
            case 1:
                _pos_x = 0.5f;
                _jumpPower = 0.2f;
                _jumpCount = 1;
                _jumpDuration = 0.5f;
                break;
            case 2:
                _pos_x = 1f;
                _jumpPower = 0.6f;
                _jumpCount = 1;
                _jumpDuration = 1f;
                break;
            case 3:
                _pos_x = 1.5f;
                _jumpPower = 0.4f;
                _jumpCount = 2;
                _jumpDuration = 2f;
                break;
        }
    }
    #endregion

    public abstract void Clear();
}
