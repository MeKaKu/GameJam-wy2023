using System.Collections.Generic;
using UnityEngine;

namespace DyeFramework.Modules{
    public class UIManager : Base.ManagerBase<UIManager>
    {
        //GameObject uiRoot = null;
        Dictionary<string, PanelBase> dict = new Dictionary<string, PanelBase>();
        public UIManager(){
            //...
            
        }
        public static void Init(){
            var var = Instance;
        }
        public static void RegisterPanel(PanelBase panel){
            if(!Instance.dict.ContainsKey(panel.name)){
                Instance.dict.Add(panel.name, panel);
            }
        }
        public static void Unregister(PanelBase panel){
            if(Instance.dict.ContainsKey(panel.name)){
                Instance.dict.Remove(panel.name);
            }
        }
        public static void ShowPanel(string panelName){
            Instance.dict.TryGetValue(panelName, out PanelBase panel);
            panel?.Show();
        }
        public static void HidePanel(string panelName){
            Instance.dict.TryGetValue(panelName, out PanelBase panel);
            panel?.Hide();
        }
    }

    public enum UILayer {
        Background = -99,
        Low = 0,
        Mid = 1,
        Top = 2,
        System = 99,
    }
}
