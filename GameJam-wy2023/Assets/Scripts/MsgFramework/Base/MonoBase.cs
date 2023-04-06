using UnityEngine;

namespace DyeFramework.Base{
    /// <summary>
    /// 扩展MonoBehaviour
    /// </summary>
    /// <typeparam name="T">所属的Manager类</typeparam>
    public class MonoBase<T> : MonoBehaviour where T : ManagerBase<T>, new()
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="eventCode">事件码</param>
        /// <param name="arg">参数</param>
        public virtual void Execute(int eventCode, object arg){
            //根据事件码执行不同的动作
        }

        /// <summary>
        /// 绑定消息
        /// </summary>
        /// <param name="eventCodes">消息码</param>
        public void Bind(params int[] eventCodes){
            foreach(int eventCode in eventCodes){
                ManagerBase<T>.Instance?.Bind(eventCode, this);
            }
        }

        /// <summary>
        /// 摧毁时解绑所有事件
        /// </summary>
        protected virtual void OnDestroy() {
            ManagerBase<T>.Instance?.Unbind(this);
        }
    }
}
