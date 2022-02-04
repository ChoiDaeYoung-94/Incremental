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
        SetCreate();
    }

    private void SetCreate()
    {
        Managers.UpdateM._updateMonsters -= Move;
        Managers.UpdateM._updateMonsters += Move;

        Managers.GameM.PlusMonster();
    }

    private void OnDisable()
    {
        Clear();
    }

    public override void Clear()
    {
        if (Managers.Instance != null)
        {
            Managers.UpdateM._updateMonsters -= Move;
            Managers.GameM.EndBattle();
            Managers.GameM.MinusMonster();
        }

        _hp = _org_hp;
        transform.position = _position;
    }

    #region Functions
    public void Move()
    {
        transform.Translate(UnityEngine.Vector2.left * UnityEngine.Time.deltaTime * _speed);
    }

    public void Hit(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            _hp = 0;
            Managers.PoolM.PushToPool(gameObject);
        }
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
