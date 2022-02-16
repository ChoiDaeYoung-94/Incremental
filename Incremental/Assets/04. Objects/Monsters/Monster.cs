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

        _sld_hp.value = _hp = _org_hp;
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
        _sld_hp.value = _hp;

        if (_hp <= 0)
        {
            _hp = 0;
            _sld_hp.value = _hp;
            DropItem();
            Managers.PoolM.PushToPool(gameObject);
        }
    }

    void DropItem()
    {
        // Gold는 80% -> 추후 경험치는 100%
        if (UnityEngine.Random.Range(1, 11) > 2)
        {
            UnityEngine.GameObject go_gold = Managers.PoolM.PopFromPool(Define.DropItems.Gold.ToString());
            Goods gold = go_gold.GetComponent<Goods>();
            gold.SettingBase(transform);
            gold.Init();
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
