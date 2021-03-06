using UnityEngine;

namespace DY
{
    public class Define : MonoBehaviour
    {
        /// <summary>
        /// Pool에서 가져온 객체 기본으로 담아두는 go.name
        /// </summary>
        public static string _activePool = "ActivePool";

        /// <summary>
        /// 현재 플레이어가 사용하는 Status Data
        /// </summary>
        public enum Stat
        {
            Base,
            Power,
            AttackSpeed,
            STR
        }

        /// <summary>
        /// 플레이어의 stat은 무조건 3씩 증가
        /// </summary>
        public static int _plusStat = 3;

        /// <summary>
        /// 플레이어의 공격 속도의 경우만 0.01씩 증가
        /// </summary>
        public static float _plusAttackSpeedStat = 0.01f;

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
            Gold,
            EXP
        }

        /// <summary>
        /// 지금 당장 분류하기 어려운 부분
        /// 추후 분류
        /// * 현재는 Pool에서 TMP_Damage만을 위해 사용
        /// </summary>
        public enum ETC
        {
            Base,
            TMP_Damage
        }
    }
}