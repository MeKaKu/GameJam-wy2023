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
        /// </summary>
        public const int PLAYER_STATE_CHANGED = 2;

        /// <summary>
        /// 显示人物提示
        /// 参数：string
        /// </summary>
        public const int SHOW_MISSION_TIP = 3;

        /// <summary>
        /// 解谜界面
        /// 参数：RiddleMsg
        /// </summary>
        public const int SHOW_RIDDLE_PANEL = 4;

        /// <summary>
        /// 显示交互提示
        /// 参数：InteractMsg
        /// </summary>
        public const int SHOW_INTERACT_TIP = 5;
    }
}
