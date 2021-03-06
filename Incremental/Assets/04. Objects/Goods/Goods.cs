#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using DG.Tweening;

public class Goods : GoodsBase
{
    Sequence _effect = null;

    public void Init()
    {
        if (_effect == null)
        {
            _effect = DOTween.Sequence();
            _effect.Append(transform.DOJump(_vec3_end, _jumpPower, _jumpCount, _jumpDuration));
            _effect.Append(transform.DOMoveY(_pos_y + _pos_y_range, 0.5f)).OnComplete(() => transform.DOMoveY(_pos_y - _pos_y_range, 1f).SetLoops(-1, LoopType.Yoyo));
        }

        Managers.UpdateM._updateDropItems -= Move;
        Managers.UpdateM._updateDropItems += Move;
    }

    private void OnDisable()
    {
        Clear();
    }

    public override void Clear()
    {
        if (Managers.Instance != null)
        {
            Managers.UpdateM._updateDropItems -= Move;
        }

        if (_effect != null)
        {
            DOTween.Kill(transform);
            _effect = null;
        }
        
        transform.position = _vec3_original;
    }

    #region Functions
    public void Move()
    {
        transform.position = new UnityEngine.Vector3(transform.position.x - UnityEngine.Time.deltaTime * _speed, transform.position.y, transform.position.z);
    }

    public void GetItems()
    {
        switch (_item)
        {
            case DY.Define.DropItems.Gold:
                UnityEngine.Debug.Log("골드 획득");
                GetGold();
                break;
            case DY.Define.DropItems.EXP:
                UnityEngine.Debug.Log("경험치 획득");
                GetExp();
                break;
        }

        Managers.PoolM.PushToPool(gameObject);
    }

    void GetGold()
    {
        Managers.DataM._ply_gold += 100;
        Middle_Menu.Instance.StartInit();
        Managers.ServerM.SetData(new System.Collections.Generic.Dictionary<string, string> { { "Gold", Managers.DataM._ply_gold.ToString() } });
    }

    void GetExp()
    {
        Managers.DataM._ply_experience += UnityEngine.Random.Range(10, 20);
        Top_Menu.Instance.SetExp();
        Panel_characterStatus.Instance._panel_playerInfo.Init();
        Managers.ServerM.SetData(new System.Collections.Generic.Dictionary<string, string> { { "Experience", Managers.DataM._ply_experience.ToString() } });
    }
    #endregion

#if UNITY_EDITOR
    [CustomEditor(typeof(Goods))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("드롭되는 아이템 설정 [ GoodsBase.cs ]", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}
