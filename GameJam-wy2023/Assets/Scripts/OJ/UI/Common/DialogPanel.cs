using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OJ
{
    public class DialogPanel : PanelBase
    {
        protected override void Awake()
        {
            base.Awake();
            Bind(
                UIEvent.SHOW_DIALOG
            );
        }
        public override void Execute(int eventCode, object arg)
        {
            switch(eventCode){
                case UIEvent.SHOW_DIALOG:{
                    if(arg == null){
                        Debug.LogError("dialogMsg is null");
                        return;
                    }
                    Show(arg as DialogMsg);
                    break;
                }
            }
        }
        void Start(){
            Hide();
            GetCom<Image>("Img_Next").transform.DOMoveY(10, 1).SetRelative().SetLoops(-1, LoopType.Yoyo);
        }

        void Update(){
            
        }
        Tween textTween;
        DialogMsg dialog;
        public void Show(DialogMsg dialogMsg){
            dialog = dialogMsg;
            GetCom<Text>("Text_Speaker").text = dialogMsg.speaker;
            for(int i=0;i<3;i++){
                GetCom<Button>("Btn_Option_" + i).gameObject.SetActive(false);
            }
            GetCom<Image>("Img_Next").gameObject.SetActive(false);
            GetCom<Text>("Text_Content").text = "";
            textTween = GetCom<Text>("Text_Content")
            .DOText(dialogMsg.content, dialog.speed * dialog.content.Length)
            .SetEase(Ease.Linear)
            .OnComplete(ShowOptions);
            Show();
        }
        void ShowOptions(){
            if(dialog.options.Count <= 0){
                Image nxt = GetCom<Image>("Img_Next");
                nxt.DOFade(0,0);
                GetCom<Image>("Img_Next").gameObject.SetActive(true);
                GetCom<Image>("Img_Next").DOFade(1, .2f);
            }
            for(int i=0;i<dialog.options.Count;i++){
                Button opt = GetCom<Button>("Btn_Option_" + i);
                opt.GetComponentInChildren<Text>().text = dialog.options[i];
                opt.gameObject.SetActive(true);
            }
        }

        protected override void OnClick(string name)
        {
            if(name.Equals("Btn_FrameBg")){
                if(textTween.IsActive()){
                    textTween.timeScale = 10;
                }
                else{
                    if(dialog.options.Count <= 0){
                        Hide();
                        dialog.callback?.Invoke("");
                    }
                }
                return;
            }
            Button btn = GetCom<Button>(name);
            Debug.Log(btn.GetComponentInChildren<Text>().text);
            Hide();
            dialog.callback?.Invoke(btn.GetComponentInChildren<Text>().text);
        }
    }

    public class DialogMsg{
        public string speaker = "";
        public string content = "";
        public List<string> options = new List<string>();
        public System.Action<string> callback = null;
        public float speed = 0.1f;
    }
}
