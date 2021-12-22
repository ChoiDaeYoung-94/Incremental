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
        // TODO : 추후 Data 정리할 때 맞는 Bg의 이름 받아 오고 DataM - BgName에서 보고 그에 맞는 Index의 Bg 실행
        _bgScroll[0].Init();
    }
}
