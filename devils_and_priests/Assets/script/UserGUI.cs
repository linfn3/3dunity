using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNamespace
{
    public class UserGUI : MonoBehaviour
    {
       


        private IUserAction action;
        private GUIStyle textStyle;
        public CharacterController contronller;
        public int flag;
        private GUIStyle hintStyle;
        private GUIStyle btnStyle;

        // 初始化获得当前场记 CurrentSecnController
        void Start()
        {
            flag = 0;
            action = Director.GetInstance().CurrentSecnController as IUserAction;
        }

        // Update is called once per frame
        void OnGUI()
        {
            textStyle = new GUIStyle
            {
                fontSize = 40,
                alignment = TextAnchor.MiddleCenter
            };
            hintStyle = new GUIStyle
            {
                fontSize = 15,
                fontStyle = FontStyle.Normal
            };
            btnStyle = new GUIStyle("button")
            {
                fontSize = 30
            };


            if (flag == 1)
            {
                // Lose
                GUI.Label(new Rect(Screen.width / 2 - 48, Screen.height / 2 - 85, 100, 50), "Lose!", textStyle);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", btnStyle))
                {
                    flag = 0;
                    action.Restart();
                }
            }
            else if (flag == 2)
            {
                // Win
                GUI.Label(new Rect(Screen.width / 2 - 48, Screen.height / 2 - 85, 100, 50), "Win!", textStyle);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", btnStyle))
                {
                    flag = 0;
                    action.Restart();
                }
            }
        }

        public void SetCharacterCtrl(CharacterController controller)
        {
            contronller = controller;
        }

        void OnMouseDown()
        {


            if (gameObject.name == "boat")
            {
                action.Boatmoved();
            }
            else
            {
                action.CharacterClicked(contronller);
            }
        }
    }
}

