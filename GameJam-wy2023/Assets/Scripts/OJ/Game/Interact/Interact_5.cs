using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;

namespace OJ
{
    public class Interact_5 : InteractBase
    {
        RiddleMsg riddleMsg;
        public override void Interact(){
            if(riddleMsg == null){
                    riddleMsg = new RiddleMsg(){
                    riddleId = 3,
                    onResult = RiddleResult
                };
            }
            //解谜游戏3
            UIManager.Handle(UIEvent.SHOW_RIDDLE_PANEL, riddleMsg);
        }
        void RiddleResult(bool result){
            if(result){
                Complete();
                //成功则获得乐章*1（背包内可查看详情：x乐章·其二，把它交给流浪机器人会解锁不同的音乐）
                Bagdata._listBag.Add(DataManager.configs.TbItem.Get(16));
            }
            else{
                Trigger();
            }
        }
    }
}
