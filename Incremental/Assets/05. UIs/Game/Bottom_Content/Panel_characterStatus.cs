#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Panel_characterStatus : MonoBehaviour
{
    [Header("--- Bottom_Content - 미리 가지고 있어야 할 data ---")]
    [SerializeField, Tooltip("GO - 강화 패널 go")]
    GameObject[] _go_reinforcing = null;
    [SerializeField, Tooltip("GO - 성장 패널 go")]
    GameObject[] _go_growth = null;

    [Header("--- 참고용 ---")]
    Coroutine _co_status = null;

    /// <summary>
    /// Initialize_Game.cs 에서 호출
    /// </summary>
    public void StartInit()
    {
        
    }

    #region Functions
    /// <summary>
    /// subMenuPanel 컨트롤 - panel_subMenu 아래 Toggle에서 제어
    /// [ 메뉴에 따른 목록 활성화 ]
    /// </summary>
    /// <param name="isReinforcing"></param>
    public void SubMenuPanelControl(bool isReinforcing)
    {
        if (isReinforcing)
        {
            Reinforcing(isReinforcing);
            Growth(!isReinforcing);
        }
        else
        {
            Reinforcing(isReinforcing);
            Growth(!isReinforcing);
        }
    }

    void Reinforcing(bool isOn)
    {
        for (int i = -1; ++i < _go_reinforcing.Length;)
            _go_reinforcing[i].SetActive(isOn);
    }

    void Growth(bool isOn)
    {
        for (int i = -1; ++i < _go_growth.Length;)
            _go_growth[i].SetActive(isOn);
    }

    #region Status Button Click
    /// <summary>
    /// panel_characterStatus - panel_Player_Info - Button_LevelUp 클릭 시
    /// </summary>
    public void LvUpBtn(bool isOn)
    {
        BtnClicked(isOn, "Lv");
    }

    /// <summary>
    /// panel_characterStatus - panel_R_STR - Button_plus 클릭 시
    /// </summary>
    public void R_STR_Btn(bool isOn)
    {
        BtnClicked(isOn, "R_STR");
    }

    /// <summary>
    /// panel_characterStatus - panel_R_HP - Button_plus 클릭 시
    /// </summary>
    public void R_HP_Btn(bool isOn)
    {
        BtnClicked(isOn, "R_HP");
    }

    /// <summary>
    /// panel_characterStatus - panel_R_VIT - Button_plus 클릭 시
    /// </summary>
    public void R_VIT_Btn(bool isOn)
    {
        BtnClicked(isOn, "R_VIT");
    }

    /// <summary>
    /// panel_characterStatus - panel_G_STR - Button_plus 클릭 시
    /// </summary>
    public void G_STR_Btn(bool isOn)
    {
        BtnClicked(isOn, "G_STR");
    }

    /// <summary>
    /// panel_characterStatus - panel_G_HP - Button_plus 클릭 시
    /// </summary>
    public void G_HP_Btn(bool isOn)
    {
        BtnClicked(isOn, "G_HP");
    }

    /// <summary>
    /// panel_characterStatus - panel_G_VIT - Button_plus 클릭 시
    /// </summary>
    public void G_VIT_Btn(bool isOn)
    {
        BtnClicked(isOn, "G_VIT");
    }

    void BtnClicked(bool isOn, string who)
    {
        if (isOn)
        {
            StopStatusCoroutine();

            _co_status = StartCoroutine(StatusBtnClick(who));
        }
        else
            StopStatusCoroutine();
    }

    IEnumerator StatusBtnClick(string who)
    {
        while (true)
        {
            switch (who)
            {
                case "Lv":
                    Debug.Log("LvUpBtn Clicked");
                    break;
                case "R_STR":
                    Debug.Log("R_STR_Btn Clicked");
                    break;
                case "R_HP":
                    Debug.Log("R_HP_Btn Clicked");
                    break;
                case "R_VIT":
                    Debug.Log("R_VIT_Btn Clicked");
                    break;
                case "G_STR":
                    Debug.Log("G_STR_Btn Clicked");
                    break;
                case "G_HP":
                    Debug.Log("G_HP_Btn Clicked");
                    break;
                case "G_VIT":
                    Debug.Log("G_VIT_Btn Clicked");
                    break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    void StopStatusCoroutine()
    {
        if (_co_status != null)
        {
            StopCoroutine(_co_status);
            _co_status = null;
        }
    }
    #endregion

    #endregion

#if UNITY_EDITOR
    [CustomEditor(typeof(Panel_characterStatus))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Canvas_Game아래 Bottom_Content - Panel_characterStatus - 관리", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}
