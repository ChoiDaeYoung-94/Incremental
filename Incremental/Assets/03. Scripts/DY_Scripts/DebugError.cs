using UnityEngine;

public class DebugError
{
    public static void GetData(string where, string error)
    {
        Debug.LogWarning($"<color=red>Error</color> - {where} \nFailed to GetData : {error}");
    }

    public static void Parse(string where, string error)
    {
        Debug.LogWarning($"<color=red>Error</color> - {where} \nFailed to Parse : {error}");
    }

    public static void Contain(string where, string item)
    {
        Debug.LogWarning($"<color=red>Error</color> - {where} \nThere is no {item}");
    }

    public static void LoadWarning(string where, string pass)
    {
        Debug.LogWarning($"<color=red>Error</color> - {where} \nFailed to Load - path : {pass}");
    }

    public static void InstantiateWarning(string where, string pass)
    {
        Debug.LogWarning($"<color=red>Error</color> - {where} \nFailed to Instantiate - path : {pass}");
    }
}