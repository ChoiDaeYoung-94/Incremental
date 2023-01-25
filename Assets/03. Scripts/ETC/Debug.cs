namespace DY
{
    public class Debug
    {
        public static void GetData(string where, string contents)
        {
            UnityEngine.Debug.LogWarning($"<color=red>Check</color> - {where} \nFailed to GetData : {contents}");
        }

        public static void Parse(string where, string contents)
        {
            UnityEngine.Debug.LogWarning($"<color=red>Check</color> - {where} \nFailed to Parse : {contents}");
        }

        public static void Contain(string where, string contents)
        {
            UnityEngine.Debug.LogWarning($"<color=red>Check</color> - {where} \nThere is no \"{contents}\"");
        }

        public static void Load(string where, string path)
        {
            UnityEngine.Debug.LogWarning($"<color=red>Check</color> - {where} \nFailed to Load - path : {path}");
        }

        public static void Instantiate(string where, string path)
        {
            UnityEngine.Debug.LogWarning($"<color=red>Check</color> - {where} \nFailed to Instantiate - path : {path}");
        }
    }
}