using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object
{
    public ISceneController CurrentScenceController { get; set; }
    private static SSDirector instance;

    public static SSDirector GetInstance(){
        if (instance == null){
            instance = new SSDirector();
        }
        return instance;
    }
}
