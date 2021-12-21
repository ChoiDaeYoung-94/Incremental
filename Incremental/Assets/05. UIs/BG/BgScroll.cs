using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour
{
    [Header("--- 미리 가지고 있어야 할 data ---")]
    [SerializeField, Tooltip("MeshRenderer")]
    MeshRenderer _mesh = null;

    [Header("--- 세팅 ---")]
    [SerializeField, Tooltip("배경 스크롤 스피드")]
    float _speed = 0;

    [Header("--- 참고용 ---")]
    [SerializeField, Tooltip("현재 배경의 Offset")]
    float _offset = 0;

    /// <summary>
    /// Scroll - UpdateM에 등록
    /// </summary>
    private void Start()
    {
        Managers.UpdateM._update -= Scroll;
        Managers.UpdateM._update += Scroll;
    }

    /// <summary>
    /// 배경 scroll
    /// </summary>
    void Scroll()
    {
        _offset += _speed * Time.deltaTime;
        _mesh.material.mainTextureOffset = new Vector2(_offset, 0);
    }
}