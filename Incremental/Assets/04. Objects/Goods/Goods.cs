#if UNITY_EDITOR
using UnityEditor;
#endif

using DG.Tweening;

public class Goods : GoodsBase
{
    public void Init()
    {
        Sequence _effect = DOTween.Sequence();
        _effect.Append(transform.DOJump(_vec3_end, _jumpPower, _jumpCount, _jumpDuration));
        _effect.Append(transform.DOMoveY(_pos_y + _pos_y_range, 1f)).OnComplete(() => transform.DOMoveY(_pos_y - _pos_y_range, 1f).SetLoops(-1, LoopType.Yoyo));
    }

    public override void Clear()
    {
        
    }

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
