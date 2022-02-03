#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APManage : MonoBehaviour
{
    [SerializeField, Header("--- 참고용 ---")]
    int _monsterCount = 0;

    public void Init()
    {
        StartCoroutine(MonsterSettings());
    }

    #region Functions
    IEnumerator MonsterSettings()
    {
        while(true)
        {
            if (_monsterCount <= 0)
            {
                CreateMonters();
            }
        }
    }

    void CreateMonters()
    {
        // TODO : 플레이어의 레벨에 따라 생성하는 규칙을 정해야 함 + 상황에 따라 몬스터 1~3까지 랜덤 돌리는 것 까지....

        Managers.PoolM.PopFromPool(Define.Monsters.Monster_1.ToString());
        _monsterCount++;
    }
    #endregion

#if UNITY_EDITOR
    [CustomEditor(typeof(APManage))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("몬스터 생성 관리", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}
