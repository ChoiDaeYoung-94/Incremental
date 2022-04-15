#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Top_Menu : MonoBehaviour
{
    static Top_Menu instance;
    public static Top_Menu Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [Header("--- 세팅 ---")]
    [SerializeField, Tooltip("TMP - Player Level")]
    TMP_Text _TMP_level = null;
    [SerializeField, Tooltip("Sprite - Player tier에 필요한 b,s,g")]
    Sprite[] _spr_tier = null;
    [SerializeField, Tooltip("Image - Player tier 표기")]
    Image _img_tier = null;
    [SerializeField, Tooltip("TMP - Player NickName")]
    TMP_Text _TMP_nickName = null;
    [SerializeField, Tooltip("TMP - Player Experience")]
    TMP_Text _TMP_experience = null;
    [SerializeField, Tooltip("TMP - Player Diamond amount")]
    TMP_Text _TMP_diaAmount = null;

    /// <summary>
    /// Initialize_Game.cs 에서 호출
    /// </summary>
    public void StartInit()
    {
        SetInformations();
    }

    #region Functions
    /// <summary>
    /// Player Data 표기
    /// </summary>
    void SetInformations()
    {
        SetLv();

        if (Managers.DataM._ply_level > 0)
            _img_tier.sprite = _spr_tier[0];
        if (Managers.DataM._ply_level > 100)
            _img_tier.sprite = _spr_tier[1];
        if (Managers.DataM._ply_level > 200)
            _img_tier.sprite = _spr_tier[2];

        _TMP_nickName.text = Managers.DataM._str_NickName;

        SetExp();

        _TMP_diaAmount.text = Managers.DataM._ply_diamond.ToString();
    }

    internal void SetLv()
    {
        _TMP_level.text = $"Lv. {Managers.DataM._ply_level}";
    }

    internal void SetExp()
    {
        _TMP_experience.text = Managers.DataM.ExpToPercentage();
    }
    #endregion

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
