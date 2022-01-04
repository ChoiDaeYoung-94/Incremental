using UnityEngine;

using MiniJSON;

public class Utils : MonoBehaviour
{
    #region MiniJson
    public static string ObjectToJson(object obj)
    {
        return Json.Serialize(obj);
    }

    public static object JsonToObject(string JsonData)
    {
        return Json.Deserialize(JsonData);
    }
    #endregion
}
