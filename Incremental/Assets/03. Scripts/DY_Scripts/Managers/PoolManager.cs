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
                Creat();
        }

        PoolObject Creat()
        {
            GameObject go = Object.Instantiate<GameObject>(GO);
            go.name = GO.name;
            return go.GetComponent_<PoolObject>();
        }
    }
    Dictionary<string, object> _pool = new Dictionary<string, object>();

    Transform _root;

    /// <summary>
    /// Managers - Awake() -> Init()
    /// </summary>
    public void Init()
    {
        if (_root = null)
        {
            _root = new GameObject { name = "Pool" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void PushToPool(PoolObject poolObj)
    {

    }

    public PoolObject PopFromPool(GameObject go, Transform parent = null)
    {
        return null;
    }

    public GameObject GetGO(string name)
    {
        return null;
    }
}
