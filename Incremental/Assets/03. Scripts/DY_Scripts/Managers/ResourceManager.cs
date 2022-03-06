using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string where, string path) where T : Object
    {
        if (Resources.Load<T>(path) == null)
            DY.Debug.Load(where, path);

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate_(string where, string path, Transform parent = null)
    {
        GameObject go = Load<GameObject>(where, "Prefabs/" + path);
        if (go == null)
        {
            DY.Debug.Instantiate(where, path);
            return null;
        }

        return Object.Instantiate(go, parent);
    }
}