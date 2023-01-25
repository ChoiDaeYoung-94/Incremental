#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Panel_PlayerInfo : MonoBehaviour
{
    [Header("--- 세팅 ---")]
    [SerializeField, Tooltip("TMP - PlayerLevel")]
    TMP_Text _TMP_playerLevel = null;
    [SerializeField, Tooltip("TMP - PlayerExp")]
    TMP_Text _TMP_playerExp = null;
    [SerializeField, Tooltip("TMP - PlayerExpPercentage")]
    TMP_Text _TMP_playerExpPercentage = null;

    /// <summary>
    /// Panel_characterStatus.cs 에서 호출
    /// </summary>
    public void Init()
    {
        _TMP_playerLevel.text = $"Lv. {Managers.DataM._ply_level}";
        _TMP_playerExp.text = $"{Managers.DataM._ply_experience} / {Managers.DataM._totalExp}";
        _TMP_playerExpPercentage.text = $"{Managers.DataM.ExpToPercentage()}";
    }

    #region Functions

    #endregion

#if UNITY_EDITOR
    [CustomEditor(typeof(Panel_PlayerInfo))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Canvas_Game아래 Bottom_Content - Panel_characterStatus - Panel_PlayerInfo - 관리", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}
