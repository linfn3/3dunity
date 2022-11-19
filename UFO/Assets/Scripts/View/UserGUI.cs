using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction userAction;
    public string result;
    public int points;
    public int round;
    public int mode;
    void Start()
    {
        points = 0;
        round = 1;
        mode = 1;
        result = "";
        userAction = SSDirector.GetInstance().CurrentScenceController as IUserAction;
    }
    
    //打印和用户交互提示界面
    void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.blue;
        

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 30;

        GUIStyle resultStyle = new GUIStyle();
        resultStyle.fontSize = 50;
        resultStyle.normal.textColor = Color.green;
        
        GUI.Label(new Rect(20, 10, 100, 50), "scores: " + points, style);
        GUI.Label(new Rect(220, 10, 100, 50), "Round: " + round, style);
        GUI.Label(new Rect(350, 100, 50, 200), result, resultStyle);

        if (GUI.Button(new Rect(750, 50, 100, 50), "Restart"))
        {
            userAction.Restart();
        }




        if (Input.GetButtonDown("Fire1"))
        {
            userAction.Hit(Input.mousePosition);
        }
    }
}
