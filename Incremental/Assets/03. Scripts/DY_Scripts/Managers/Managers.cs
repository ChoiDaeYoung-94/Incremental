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

    ResourceManager _resourceM = new ResourceManager();
    public static ResourceManager ResourceM { get { return instance._resourceM; } }

    UpdateManager _updateM = new UpdateManager();
    public static UpdateManager UpdateM { get { return instance._updateM; } }

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
        }
        else
            Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        instance = null;
    }

    private void Update()
    {
        _updateM.OnUpdate();
    }

    /// <summary>
    /// 씬 전환 시 클리어
    /// </summary>
    public void Clear()
    {
        UpdateM.Clear();
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
