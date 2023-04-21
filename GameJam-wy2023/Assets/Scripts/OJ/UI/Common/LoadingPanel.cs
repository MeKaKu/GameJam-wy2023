using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OJ
{
    public class LoadingPanel : PanelBase
    {
        static LoadingPanel loadingPanel;
        protected override void Awake()
        {
            if(loadingPanel!=null){
                Destroy(gameObject);
                return;
            }
            loadingPanel = this;
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }

        Image img_slot;
        Image img_bar;
        bool loading;
        IEnumerator fakeLoading;
        private void Start() {
            Hide();
            img_slot = GetCom<Image>("Img_Slot");
            img_bar = GetCom<Image>("Img_Slot/Img_Bar");
        }

        public override void Show()
        {
            if(null != fakeLoading){
                return;
            }
            fakeLoading = FakeLoading();
            StartCoroutine(fakeLoading);
        }
        public override void Hide()
        {
            loading = false;
            if(fakeLoading == null){
                base.Hide();
            }
        }

        IEnumerator FakeLoading(){
            loading = true;
            img_slot.DOFade(0, 0);
            img_bar.fillAmount = 0;
            group.DOFade(1, 0);
            //yield return group.DOFade(1, .5f).WaitForCompletion();
            base.Show();
            yield return img_slot.DOFade(1, .2f).WaitForCompletion();
            float tp = .05f;
            float t = 0f;
            float speed = .1f;
            while(loading){
                t = Mathf.Lerp(t, tp, speed * Time.deltaTime);
                img_bar.fillAmount = Mathf.Lerp(img_bar.fillAmount, .8f, t);
                yield return null;
            }
            yield return img_bar.DOFillAmount(1, .8f).WaitForCompletion();
            yield return group.DOFade(0, .2f).WaitForCompletion();
            fakeLoading = null;
            base.Hide();
        }
    }
}
