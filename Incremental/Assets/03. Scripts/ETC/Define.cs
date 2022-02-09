using UnityEngine;

public class Define : MonoBehaviour
{
    /// <summary>
    /// Pool에서 가져온 객체 기본으로 담아두는 go.name
    /// </summary>
    public static string _activePool = "ActivePool";

    /// <summary>
    /// 사용하는 배경 이름
    /// </summary>
    public enum BgName
    {
        Base,
        BG_01,
        BG_02,
        BG_03,
        BG_04
    }

    /// <summary>
    /// 사용하는 몬스터 이름 (Pool에 사용)
    /// </summary>
    public enum Monsters
    {
        Base,
        Monster_1,
        Monster_2,
        Monster_3
    }

    /// <summary>
    /// 몬스터에게 드롭되는 아이템
    /// </summary>
    public enum DropItems
    {
        Base,
        Gold
    }
}
