#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class TMP_Damage : MonoBehaviour
{
    [Header("--- 정해진 값 ---")]
    [SerializeField, Tooltip("RRT - Original posY - 연출 시작 및 종료 뒤를 위함")]
    float _posY_org = 0.35f;
    [SerializeField, Tooltip("RRT - Scale - 연출 시작 및 종료 뒤를 위함")]
    float _scale_org = 0.1f;

    public void Init(int damage)
    {
        // Tween 뒤 -> Managers.PoolM.PushToPool(gameObject);
    }

    private void OnDisable()
    {
        Clear();
    }

    public void Clear()
    {
        // 위치 초기화
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
