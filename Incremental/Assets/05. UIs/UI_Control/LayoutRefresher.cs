using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LayoutRefresher : MonoBehaviour
{
    RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
    }
}
