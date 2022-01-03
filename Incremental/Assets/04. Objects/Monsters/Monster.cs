#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonsterBase
{
    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    public override void Clear()
    {

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Monster))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("몬스터의 기본 설정 [ MonsterBase.cs ]", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}
