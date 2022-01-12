using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string where, string path) where T : Object
    {
        if (Resources.Load<T>(path) == null)
            DebugError.LoadWarning(where, path);

        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            // 경로의 마지막 /를 찾는
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.PoolM.GetGO(name);

            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate_(string where, string path, Transform parent = null)
    {
        GameObject go = Load<GameObject>(where, "Prefabs/" + path);
        if (go == null)
        {
            DebugError.InstantiateWarning(where, path);
            return null;
        }

        if (go.GetComponent<PoolObject>() != null)
            return Managers.PoolM.PopFromPool(go, parent).gameObject;

        return Object.Instantiate(go, parent);
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        PoolObject poolable = go.GetComponent<PoolObject>();
        if (poolable != null)
        {
            Managers.PoolM.PushToPool(poolable);
            return;
        }

        Object.Destroy(go);
    }
}