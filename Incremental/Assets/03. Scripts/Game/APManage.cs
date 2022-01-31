#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APManage : MonoBehaviour
{
    public void Init()
    {

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(APManage))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("몬스터 생성 관리", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}
