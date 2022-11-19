using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAction : SSAction
{
    float gravity;          
    float speed;            
    float time;             
    Vector3 direction;      

    public static FlyAction GetSSAction(Vector3 direction, float speed)
    {
        FlyAction action = ScriptableObject.CreateInstance<FlyAction>();
        action.gravity = 10;
        action.time = 0;
        action.speed = speed;
        action.direction = direction;
        return action;
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        time += Time.deltaTime;
        transform.Translate(Time.deltaTime * gravity * time * Vector3.down);
        transform.Translate(direction * speed * Time.deltaTime);
        if (this.transform.position.y < -7) 
        {
            this.destroy = true;
            this.enable = false;
            this.callback.SSActionEvent(this);
        }
        
    }
}
