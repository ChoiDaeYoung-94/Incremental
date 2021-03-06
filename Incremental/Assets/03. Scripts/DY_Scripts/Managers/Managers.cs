#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class Managers : MonoBehaviour
{
    /// <summary>
    /// Singleton - 객체 오직 1
    /// Manager관련 script 모두 등록
    /// </summary>
    static Managers instance;
    public static Managers Instance { get { return instance; } }

    DataManager _dataM = new DataManager();
    public static DataManager DataM { get { return instance._dataM; } }

    // GM의 경우 바로 보면서 작업하는게 편할 듯 
    [SerializeField] GameManager _gameM = null;
    public static GameManager GameM { get { return instance._gameM; } }

    PoolManager _poolM = new PoolManager();
    public static PoolManager PoolM { get { return instance._poolM; } }

    PopupManager _popupM = new PopupManager();
    public static PopupManager PopupM { get { return instance._popupM; } }

    ResourceManager _resourceM = new ResourceManager();
    public static ResourceManager ResourceM { get { return instance._resourceM; } }

    ServerManager _serverM = new ServerManager();
    public static ServerManager ServerM { get { return instance._serverM; } }

    UpdateManager _updateM = new UpdateManager();
    public static UpdateManager UpdateM { get { return instance._updateM; } }

    [Header("--- 미리 가지고 있어야 할 data ---")]
    [Tooltip("Pool에 사용할 GameObject")]
    public GameObject[] _go_poolGOs = null;
    [Tooltip("Pool에 사용할 UI")]
    public GameObject[] _go_poolUIs = null;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("Manager");
            if (go == null)
            {
                go = new GameObject { name = "Manager" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();

            InitM();
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        _updateM.OnUpdate();
    }

    /// <summary>
    /// 추후 다른 씬 특히 QA 전용 씬을 만들던지 할 때 flow 대비
    /// </summary>
    public void InitM()
    {
        DataM.Init();
        PoolM.Init();
        PopupM.Init();
    }

    /// <summary>
    /// 씬 전환 시 클리어
    /// </summary>
    public void Clear()
    {
        UpdateM.Clear();
        //PoolM.Clear();
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Managers))]
    public class customEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("초기 메니저 세팅", MessageType.Info);

            base.OnInspectorGUI();
        }
    }
#endif
}
