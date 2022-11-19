using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : SSActionManager, ISSActionCallback
{
    public FlyAction flyAction;
    public FirstController controller;

    protected new void Start()
    {
        controller = (FirstController)SSDirector.GetInstance().CurrentScenceController;
        controller.actionManager = this;
    }

    public void Fly(GameObject disk, float speed, Vector3 direction)
    {
        flyAction = FlyAction.GetSSAction(direction, speed);
        RunAction(disk, flyAction, this);
    }
    public void SSActionEvent(SSAction source,
                              SSActionEventType events = SSActionEventType.Competed,
                              int intParam = 0,
                              string strParam = null,
                              Object objectParam = null)
    {
        controller.saucerFactory.FreeDisk(source.gameObject);
    }
}
