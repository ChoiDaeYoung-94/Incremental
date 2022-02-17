#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using DG.Tweening;

public class TMP_Damage : MonoBehaviour
{
    [Header("--- 미리 가지고 있어야 할 data ---")]
    [SerializeField, Tooltip("RTR - 미리 받아 두기 위함")]
    RectTransform _rtr_this = null;
    [SerializeField, Tooltip("TMP - 미리 받아 두기 위함")]
    TMP_Text _TMP_this = null;

    [Header("--- 정해진 값 ---")]
    [SerializeField, Tooltip("RRT - Original posY - 연출 시작 값")]
    float _posY_start = 0.35f;
    [SerializeField, Tooltip("RRT - Original posY - 연출 + 값")]
    float _posY_plus = 0.15f;
    [SerializeField, Tooltip("RRT - Scale - 연출 시작 값")]
    Vector3 _vec3_scale_start = new Vector3(0.01f, 0.01f, 0.01f);
    [SerializeField, Tooltip("RRT - Scale - 연출 + 값")]
    Vector3 _vec3_scale_plus = new Vector3(0.15f, 0.15f, 0.15f);
    [SerializeField, Tooltip("RRT - Scale - 연출 종료 값")]
    Vector3 _vec3_scale_end = new Vector3(0.1f, 0.1f, 0.1f);

    Sequence _effect = null;

    public void Init(int damage)
    {
        _TMP_this.text = damage.ToString();

        if (_effect == null)
        {
            _rtr_this.anchoredPosition = new Vector3(0f, _posY_start, 0f);

            _effect = DOTween.Sequence();

            _effect.Append(transform.DOScale(_vec3_scale_plus, 0.2f));
            _effect.Append(transform.DOScale(_vec3_scale_end, 0.2f));
            _effect.Append(_rtr_this.DOAnchorPosY(_posY_start + _posY_plus, 0.3f));
            _effect.Join(_TMP_this.DOFade(0f, 0.3f)).OnComplete(() => Managers.PoolM.PushToPool(gameObject));
        }
    }

    private void OnDisable()
    {
        Clear();
    }

    public void Clear()
    {
        if (_effect != null)
        {
            DOTween.Kill(transform);
            _effect = null;
        }

        _rtr_this.anchoredPosition = new Vector3(0f, _posY_start, 0f);
        _rtr_this.localScale = _vec3_scale_start;
        _TMP_this.alpha = 1f;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Goods))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("오직 대미지 TMP의 연출을 위함", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}
