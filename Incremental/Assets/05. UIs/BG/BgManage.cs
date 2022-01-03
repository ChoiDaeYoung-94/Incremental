#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgManage : MonoBehaviour
{
    [Header("--- 참고용 ---")]
    [SerializeField, Tooltip("BgScroll")]
    BgScroll[] _bgScroll = null;

    public void Init()
    {
        // TODO : 추후 Data 정리할 때 맞는 Bg의 이름 받아 오고 Define - BgName에서 보고 그에 맞는 Index의 Bg 실행
        _bgScroll[0].Init();
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BgManage))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("서버에 저장된 현재 BG 정보를 가져온 뒤 Init \n" +
                "BgScroll.cs -> UpdateM - _updateBgScroll에 Scroll 등록 후 scroll", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}
