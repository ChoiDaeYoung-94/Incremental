using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetHeight : MonoBehaviour
{
    [SerializeField, Tooltip("하단 메뉴 부모 safeArea")]
    RectTransform _rtr_safeArea = null;

    [SerializeField, Tooltip("하단 메뉴 RTR -> Height 대응")]
    RectTransform _rtr_btmMenu = null;

    private void Start()
    {
        Refresh();
    }

    private void Update()
    {
        Refresh();
    }

    void Refresh()
    {
        float height_canvas = GetComponent<RectTransform>().rect.height;

        _rtr_btmMenu.sizeDelta = new Vector2(0, height_canvas * 0.45f - height_canvas * _rtr_safeArea.anchorMin.y);
    }
}
