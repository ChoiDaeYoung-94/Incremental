using UnityEngine;

public class DebugError
{
    public static void GetData(string where, string error)
    {
        Debug.LogError("<color=red>Error</color> - " + where + "\nFailed to GetData : " + error);
    }

    public static void Parse(string where, string error)
    {
        Debug.LogError("<color=red>Error</color> - " + where + "\nFailed to Parse : " + error);
    }

    public static void Contain(string where, string item)
    {
        Debug.LogError("<color=red>Error</color> - " + where + "\nThere is no " + item);
    }

    public static void LoadError(string where, string pass)
    {
        Debug.LogError("<color=red>Error</color> - " + where + "\nFailed to Load - path : " + pass);
    }

    public static void InstantiateError(string where, string pass)
    {
        Debug.LogError("<color=red>Error</color> - " + where + "\nFailed to Instantiate - path : " + pass);
    }
}