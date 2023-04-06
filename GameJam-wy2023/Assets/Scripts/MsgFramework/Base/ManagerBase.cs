using System.Collections.Generic;
using UnityEngine;

namespace DyeFramework.Base{
    /// <summary>
    /// 泛型单例
    /// </summary>
    /// <typeparam name="T">子类</typeparam>
    public class ManagerBase<T> where T : ManagerBase<T>, new()
    {
        //单例模式
        static T ins;
        public static T Instance{
            get{
                return ins == null ? ins = new T() : ins;
            }
        }
        protected ManagerBase(){}
        /// <summary>
        /// 处理事件，静态方法
        /// </summary>
        /// <param name="eventCode">事件码</param>
        /// <param name="arg">参数</param>
        public static void Handle(int eventCode, object arg = null){
            Instance?.Trigger(eventCode, arg);
        }
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="eventCode">事件码</param>
        /// <param name="arg">参数</param>
        public virtual void Trigger(int eventCode, object arg)
        {
            if(!dict.ContainsKey(eventCode)){
                Debug.LogWarning($"事件<{eventCode}>没有注册");
                return;
            }
            foreach(var mono in dict[eventCode]){
                mono.Execute(eventCode, arg);
            }
        }
        /// <summary>
        /// 事件码 与 绑定了该事件的mono脚本列表 的字典表
        /// </summary>
        Dictionary<int, List<MonoBase<T>>> dict = new Dictionary<int, List<MonoBase<T>>>();
        /// <summary>
        /// 绑定事件
        /// </summary>
        /// <param name="eventCode">事件码</param>
        /// <param name="mono">脚本</param>
        public virtual void Bind(int eventCode, MonoBase<T> mono){
            if(!dict.ContainsKey(eventCode)){
                dict.Add(eventCode, new List<MonoBase<T>>(){mono});
            }
            else if(!dict[eventCode].Contains(mono)){
                dict[eventCode].Add(mono);
            }
        }
        /// <summary>
        /// 解绑某脚本绑定的所有事件
        /// </summary>
        /// <param name="mono">脚本</param>
        public virtual void Unbind(MonoBase<T> mono){
            foreach(var pair in dict){
                if(pair.Value.Contains(mono)){
                    pair.Value.Remove(mono);
                }
            }
        }
    }
}
