using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DyeFramework.Modules
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PanelBase : Base.MonoBase<UIManager>
    {
        //name - components
        Dictionary<string, List<Component>> dict = new Dictionary<string, List<Component>>();
        protected CanvasGroup group;
        protected virtual void Awake(){
            UIManager.RegisterPanel(this);
            GetUIComponents(transform);
            group = GetComponent<CanvasGroup>();
        }

        public T GetCom<T>(string name)where T:Component{
            if(dict.ContainsKey(name)){
                foreach(var com in dict[name]){
                    if(com is T){
                        return com as T;
                    }
                }
            }
            return null;
        }
        void GetUIComponents(Transform root, string pre = ""){
            for(int i=0;i<root.childCount;i++){
                Transform child = root.GetChild(i);
                Component[] components = child.GetComponents<Component>();
                string key = pre + child.name;
                if(!dict.ContainsKey(key)){
                    dict.Add(key, new List<Component>());
                }
                bool isPanel = false;
                foreach(var component in components){
                    if(component is UIBehaviour){
                        dict[key].Add(component);
                        if(component is Button){
                            (component as Button).onClick.AddListener(()=>{
                                OnClick(key);
                            });
                        }
                        else if(component is InputField){
                            (component as InputField).onValueChanged.AddListener((value)=>{
                                OnInput(key, value);
                            });
                        }
                        else if(component is Slider){
                            (component as Slider).onValueChanged.AddListener((value)=>{
                                OnSlid(key, value);
                            });
                        }
                    }
                    else if(component is PanelBase){
                        dict[key].Add(component);
                        isPanel = true;
                    }
                }
                if(!isPanel){
                    GetUIComponents(child, key + "/");
                }
            }
        }
        protected virtual void OnClick(string name){

        }
        protected virtual void OnInput(string name, string value){

        }
        protected virtual void OnSlid(string name, float value){

        }
        protected override void OnDestroy(){
            base.OnDestroy();
            UIManager.Unregister(this);
        }
        public virtual void Show(){
            //gameObject.SetActive(true);
            group.alpha = 1;
            group.interactable = true;
            group.blocksRaycasts = true;
        }
        public virtual void Hide(){
            //gameObject.SetActive(false);
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }
    }
}
