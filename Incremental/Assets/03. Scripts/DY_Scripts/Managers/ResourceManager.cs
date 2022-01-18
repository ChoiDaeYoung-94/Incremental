using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string where, string path) where T : Object
    {
        if (Resources.Load<T>(path) == null)
            DebugError.LoadWarning(where, path);

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