#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Middle_Menu : MonoBehaviour
{
    static Middle_Menu instance;
    public static Middle_Menu Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [Header("--- 세팅 ---")]
    [SerializeField, Tooltip("TMP - Player Gold")]
    TMP_Text _TMP_gold = null;

    /// <summary>
    /// Initialize_Game.cs 에서 호출
    /// </summary>
    public void StartInit()
    {
        _TMP_gold.text = string.Format("{0:#,##0}", Managers.DataM._ply_gold);
    }

    #region Functions

    #endregion

#if UNITY_EDITOR
    [CustomEditor(typeof(Middle_Menu))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Canvas_Game아래 Middle_Menu - 관리", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}
