#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top_Menu : MonoBehaviour
{
    public void StartInit()
    {

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Top_Menu))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Canvas_Game아래 Top_Menu - 관리", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}
