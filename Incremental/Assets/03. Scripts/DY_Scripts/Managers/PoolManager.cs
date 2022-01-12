using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    class Pool
    {
        public GameObject GO { get; private set; }
        public Transform Root { get; set; }

        Stack<PoolObject> _poolStack = new Stack<PoolObject>();

        public void Init(GameObject go, int count = 5)
        {
            GO = go;
            Root = new GameObject().transform;
            Root.name = $"{go.name}";

            for (int i = -1; ++i < count;)
                PushToPool(Creat());
        }

        PoolObject Creat()
        {
            GameObject go = Object.Instantiate<GameObject>(GO);
            go.name = GO.name;
            return go.GetComponent_<PoolObject>();
        }

        public void PushToPool(PoolObject poolObj)
        {
            if (poolObj == null)
                return;

            poolObj.transform.parent = Root;
            poolObj.gameObject.SetActive(false);

            _poolStack.Push(poolObj);
        }

        public PoolObject PopFromPool(Transform parent)
        {
            PoolObject poolObj;

            if (_poolStack.Count > 0)
                poolObj = _poolStack.Pop();
            else
                poolObj = Creat();

            poolObj.gameObject.SetActive(true);

            if (parent == null)
                poolObj.transform.parent = Managers.Instance.gameObject.transform;

            poolObj.transform.parent = parent;

            return poolObj;
        }
    }
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();

    /// <summary>
    /// Pool의 root Transform
    /// </summary>
    Transform _root;

    /// <summary>
    /// Managers - Awake() -> Init()
    /// </summary>
    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "Pool" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreatePool(GameObject go, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(go, count);
        pool.Root.parent = _root;

        _pool.Add(go.name, pool);
    }

    public void PushToPool(PoolObject poolObj)
    {
        string name = poolObj.gameObject.name;

        if (!_pool.ContainsKey(name))
        {
            GameObject.Destroy(poolObj.gameObject);
            return;
        }

        _pool[name].PushToPool(poolObj);
    }

    public PoolObject PopFromPool(GameObject go, Transform parent = null)
    {
        if (!_pool.ContainsKey(go.name))
            CreatePool(go);

        return _pool[go.name].PopFromPool(parent);
    }

    public GameObject GetGO(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;

        return _pool[name].GO;
    }

    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}
