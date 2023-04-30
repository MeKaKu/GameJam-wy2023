using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class GameEvent
    {
        public const int TRY_FLOW = 1;
        public const int FLOW_STARED = 2;
        public const int FLOW_ENDED = 3;
        /// <summary>
        /// 玩家生命值改变
        /// 参数：int
        /// </summary>
        public const int CHANGE_HP = 4;
    }
}
