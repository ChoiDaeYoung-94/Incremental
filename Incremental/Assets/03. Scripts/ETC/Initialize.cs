#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    /// <summary>
    /// 초기화 해야하는 스크립트들의 이름을 그대로 선언
    /// -> 먼저 적은 순으로 초기화 진행
    /// </summary>
    enum Scripts
    {

    }

    [Tooltip("초기화 해야 할 스크립트를 지닌 게임오브젝트")]
    [SerializeField] GameObject[] _go_initialze = null;

    private void Awake()
    {
        foreach (Scripts script in Enum.GetValues(typeof(Scripts)))
        {
            foreach (GameObject item in _go_initialze)
            {
                if (item.GetComponent(script.ToString()) != null)
                {
                    // 실행해야 할 메서드
                    item.GetComponent(script.ToString()).SendMessage("");
                    break;
                }
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Initialize))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("초기화 순서를 지정할 경우 사용", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}
