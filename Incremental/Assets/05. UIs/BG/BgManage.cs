#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgManage : MonoBehaviour
{
    static BgManage instance;
    public static BgManage Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    [Header("--- 미리 가지고 있어야 할 data ---")]
    [SerializeField, Tooltip("BgScroll")]
    BgScroll[] _bgScroll = null;
    [SerializeField, Tooltip("Canvas_BG_Ground - Bg랑 맞추어 바닥 채우기 위함")]
    GameObject[] _go_bgGrounds = null;

    [Header("--- 참고용 ---")]
    [SerializeField, Tooltip("사용 가능 한 BG max index")]
    int _maxIndex = 0;

    /// <summary>
    /// Initialize_Game.cs 에서 호출
    /// </summary>
    public void StartInit()
    {
        _maxIndex = Enum.GetValues(typeof(DY.Define.BgName)).Length - 1;

        SetBg(Managers.DataM._ply_bgIndex);
    }

    internal void SetBg(int BgIndex)
    {
        int Index = BgIndex;

        if (Index > _maxIndex)
        {
            Index = _maxIndex;
            Managers.DataM._ply_bgIndex = _maxIndex;
        }

        _bgScroll[Index].Init();
        _bgScroll[Index].gameObject.SetActive(true);
        _go_bgGrounds[Index].SetActive(true);

        for (int i = -1; ++i < _bgScroll.Length;)
        {
            if (i != Index)
            {
                _bgScroll[i].gameObject.SetActive(false);
                _go_bgGrounds[i].SetActive(false);
            }
        }
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
