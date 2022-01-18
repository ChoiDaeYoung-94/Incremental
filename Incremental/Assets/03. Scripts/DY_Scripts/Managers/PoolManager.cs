using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    class Pool
    {
        /// <summary>
        /// Pool에 생성할 GameObject
        /// </summary>
        public GameObject GO_poolTarget { get; private set; }

        /// <summary>
        /// Pool에 생성할 GameObject의 root Transform
        /// </summary>
        public Transform Root { get; set; }

        /// <summary>
        /// 생성된 PoolObject 관리, 메서드로 Push, Pop 관리
        /// </summary>
        Stack<PoolObject> _Stack_pool = new Stack<PoolObject>();

        /// <summary>
        /// Pool 생성 시 Init
        /// -> Pool아래에 생성할 오브젝트의 Root 생성 후 create
        /// </summary>
        /// <param name="go"></param>
        /// <param name="root"></param>
        /// <param name="count"></param>
        public void Init(GameObject go, string root, int count)
        {
            GO_poolTarget = go;

            // root가 이미 있으면 그 아래에
            for (int i = -1; ++i < Managers.PoolM._root.childCount;)
            {
                if (Managers.PoolM._root.GetChild(i).name == root)
                {
                    Root = Managers.PoolM._root.GetChild(i).transform;
                    break;
                }
            }

            // root가 없으면 생성
            if (Root == null)
            {
                Root = new GameObject().transform;
                Root.name = $"{root}";
            }

            // count만큼 pool로
            for (int i = -1; ++i < count;)
                PushToPool(Create());
        }

        /// <summary>
        /// GO_poolTarget 생성 후 PoolObject반환
        /// [ 혹시 PoolObject가 없을 수 있으니 없으면 Add ]
        /// </summary>
        /// <returns></returns>
        PoolObject Create()
        {
            GameObject go = Object.Instantiate(GO_poolTarget);
            go.name = GO_poolTarget.name;

            PoolObject poolObj = go.GetComponent_<PoolObject>();
            // 추후 다시 풀로 돌아갈 때 가야할 root를 알기 위해
            poolObj._str_inactiveRootName = Root.name;

            return poolObj;
        }

        /// <summary>
        /// 생성된 go의 root를 맞추고 비활성화 후 stack에 push
        /// </summary>
        /// <param name="poolObj"></param>
        public void PushToPool(PoolObject poolObj)
        {
            // 혹시 모를...
            if (poolObj == null)
                return;

            poolObj.transform.parent = Root;
            poolObj.gameObject.SetActive(false);

            _Stack_pool.Push(poolObj);
        }

        /// <summary>
        /// Stack에서 Pop을 할 때 Stack이 비워있을 경우 Create
        /// 사용할 것이니까 활성화 후 transform 정리
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public PoolObject PopFromPool(Transform parent)
        {
            PoolObject poolObj;

            if (_Stack_pool.Count > 0)
                poolObj = _Stack_pool.Pop();
            else
                poolObj = Create();

            poolObj.gameObject.SetActive(true);

            if (parent == null)
                poolObj.transform.parent = GameObject.Find("ActivePool").transform;
            else
                poolObj.transform.parent = parent;

            return poolObj;
        }
    }

    [Tooltip("Pool 관리 할 Dictionary - _root아래의 root, Pool로 관리")]
    Dictionary<string, Pool> _dic_pool = new Dictionary<string, Pool>();

    [Tooltip("Pool의 root Transform")]
    public Transform _root;

    /// <summary>
    /// Managers - Awake() -> Init()
    /// Pool에 둬야 할 것들 미리 생성
    /// </summary>
    public void Init()
    {
        // root 생성
        if (_root == null)
        {
            _root = new GameObject { name = "Pool" }.transform;
            Object.DontDestroyOnLoad(_root);
        }

        for (int i = -1; ++i < Managers.Instance._go_poolMonsters.Length;)
            CreatePool(Managers.Instance._go_poolMonsters[i], "Monsters");
    }

    /// <summary>
    /// Pool 생성 (기본 5개 씩)
    /// </summary>
    /// <param name="go"></param>
    /// <param name="root"></param>
    /// <param name="count"></param>
    public void CreatePool(GameObject go, string root, int count = 5)
    {
        if (_dic_pool.ContainsKey(root))
            _dic_pool[root].Init(go, root, count);
        else
        {
            Pool pool = new Pool();
            pool.Init(go, root, count);
            pool.Root.parent = _root;
            _dic_pool.Add(root, pool);
        }
    }

    /// <summary>
    /// 사용한 PoolObj를 Pool에 다시 Push
    /// </summary>
    /// <param name="poolObj"></param>
    public void PushToPool(PoolObject poolObj)
    {
        string rootName = poolObj._str_inactiveRootName;

        // 혹시 모를...
        if (!_dic_pool.ContainsKey(rootName))
        {
            GameObject.Destroy(poolObj.gameObject);
            return;
        }

        // Stack으로 push
        _dic_pool[rootName].PushToPool(poolObj);
    }

    /// <summary>
    /// _dic_pool에서 Pool에 있는 사용할 go를 Pop
    /// </summary>
    /// <param name="go"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public PoolObject PopFromPool(PoolObject poolObj, Transform parent = null)
    {
        // 혹시 모를...
        if (!_dic_pool.ContainsKey(poolObj._str_inactiveRootName))
            CreatePool(poolObj.gameObject, poolObj._str_inactiveRootName);

        return _dic_pool[poolObj._str_inactiveRootName].PopFromPool(parent);
    }

    /// <summary>
    /// Pool 날릴 때 사용
    /// 현재 Managers - Clear()에 주석 처리 중 -> 거진 사용 안할 듯
    /// </summary>
    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _dic_pool.Clear();
    }
}
