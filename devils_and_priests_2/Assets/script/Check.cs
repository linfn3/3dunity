using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNamespace
{
    public class Check : MonoBehaviour
    {
        public FirstController sceneController;

        protected void Start()
        {
            sceneController = (FirstController)Director.GetInstance().CurrentSecnController;
            sceneController.gameStatusManager = this;
        }

        public int CheckGame()
        {

            int leftPriests = (sceneController.leftStones.GetCharacterNum())[0];
            int leftDevils = (sceneController.leftStones.GetCharacterNum())[1];
            int rightPriests = (sceneController.rightStones.GetCharacterNum())[0];
            int rightDevils = (sceneController.rightStones.GetCharacterNum())[1];

            if (leftPriests + leftDevils == 6) return 2;
            if ((rightPriests < rightDevils && rightPriests > 0) || (leftPriests < leftDevils && leftPriests > 0))
            {
                return 1;
            }
            return 0; 
        }
    }
}
