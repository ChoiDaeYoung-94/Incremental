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
        _TMP_level.text = $"Lv. {Managers.DataM._ply_level}";

        if (Managers.DataM._ply_level > 0)
            _img_tier.sprite = _spr_tier[0];
        if (Managers.DataM._ply_level > 100)
            _img_tier.sprite = _spr_tier[1];
        if (Managers.DataM._ply_level > 200)
            _img_tier.sprite = _spr_tier[2];

        _TMP_nickName.text = Managers.DataM._str_NickName;

        _TMP_experience.text = ExpToPercentage();

        _TMP_diaAmount.text = Managers.DataM._ply_diamond.ToString();
    }

    string ExpToPercentage()
    {
        // 따로 경험치 테이블 안 만듬 -> 임시 계산식...
        int totalExp = 0;

        for (int i = 0; ++i <= Managers.DataM._ply_level;)
            totalExp += i * 10 + (i * 5) * 10;

        Managers.DataM._totalExp = totalExp;

        float percentage = Managers.DataM._ply_experience / Managers.DataM._totalExp * 100f;

        return $"{string.Format("{0:0.00}", percentage)} %";
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
