using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyNamespace;

namespace MyNamespace
{
    public class Director : System.Object
    {
        private static Director SSDirector;
        public ISceneController CurrentSecnController { get; set; }

        // get instance anytime anywhere!
        public static Director GetInstance()
        {
            if (SSDirector == null)
            {
                SSDirector = new Director();
            }
            return SSDirector;
        }

        public int getFPS()
        {
            return Application.targetFrameRate;
        }

        public void setFPS(int fps)
        {
            Application.targetFrameRate = fps;
        }
    }
}