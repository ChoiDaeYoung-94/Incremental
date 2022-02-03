#if UNITY_EDITOR
using UnityEditor;
#endif

public class Monster : MonsterBase
{
    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    private void OnEnable()
    {
        Managers.UpdateM._update -= Move;
        Managers.UpdateM._update += Move;
    }

    private void OnDisable()
    {
        Clear();
    }

    public override void Clear()
    {
        if (Managers.Instance != null)
            Managers.UpdateM._update -= Move;

        _hp = _org_hp;
    }

    #region Functions
    public void Move()
    {
        transform.Translate(UnityEngine.Vector2.left * UnityEngine.Time.deltaTime * _speed);
    }
    #endregion

#if UNITY_EDITOR
    [CustomEditor(typeof(Monster))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("몬스터의 기본 설정 [ MonsterBase.cs ]", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}
