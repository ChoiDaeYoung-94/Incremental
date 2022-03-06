#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DY
{
    [ExecuteInEditMode]
    public class ScreenSize : MonoBehaviour
    {
        [Header("--- 세팅 ---")]
        [SerializeField, Tooltip("타이틀 Image RectTransform")]
        RectTransform _RTR_title;
        [SerializeField, Tooltip("원하는 해상도 X값")]
        float _width = 1080;
        [SerializeField, Tooltip("원하는 해상도 Y값")]
        float _height = 1920;

        private void Awake()
        {
            _RTR_title = GetComponent<RectTransform>();
        }

        private void Update()
        {
            SetSize();
        }

        void SetSize()
        {
            float ratio = _width / _height;
            float deviceRatio = _width / Screen.width;

            float x = 0, y = 0;

            // 디바이스의 가로 비율이 더 높을 경우
            if ((float)Screen.width / Screen.height >= ratio)
            {
                x = Screen.width * deviceRatio;
                y = _height * (x / _width);
            }
            // 디바이스의 세로 비율이 더 높을 경우
            else
            {
                y = Screen.height * deviceRatio;
                x = _width * (y / _height);
            }

            _RTR_title.sizeDelta = new Vector2(x, y);
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(ScreenSize))]
        public class customEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                EditorGUILayout.HelpBox("기기 해상도에 따른 타이틀 사이즈 대응", MessageType.Info);

                base.OnInspectorGUI();
            }
        }
#endif
    }
}