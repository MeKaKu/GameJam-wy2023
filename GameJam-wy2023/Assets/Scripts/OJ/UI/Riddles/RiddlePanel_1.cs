using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace OJ
{
    public class RiddlePanel_1 : RiddlePanelBase
    {
        private void Start() {
            riddleId = 1;
        }
        public override void Show()
        {
            GetCom<InputField>("Input_Answer").text = "";
            UpdateAnswerText("");
            base.Show();
        }

        protected override void OnInput(string name, string value)
        {
            UpdateAnswerText(value);
        }
        protected override void OnClick(string name)
        {
            Result(GetCom<InputField>("Input_Answer").text.Equals("2075"));
        }

        void UpdateAnswerText(string answer){
            Text text = GetCom<Text>("Text_Answer");
            text.text = "";
            for(int i=0;i<answer.Length;i++){
                text.text += answer[i] + "         ";
            }
        }
    }
}
