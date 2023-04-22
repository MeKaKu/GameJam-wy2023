using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class UIEvent
    {
        /// <summary>
        /// 显示对话信息
        /// 参数：DialogMsg
        /// </summary>
        public const int SHOW_DIALOG = 1;

        /// <summary>
        /// 玩家状态改变时
        /// 参数：PlayerState | 改变之后的状态
        /// </summary>
        public const int PLAYER_STATE_CHANGED = 2;
    }
}
