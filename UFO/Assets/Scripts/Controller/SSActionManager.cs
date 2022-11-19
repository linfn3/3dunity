using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<int> deleteList = new List<int>();
    private List<SSAction> actionList = new List<SSAction>();

    protected void Start()
    {

    }

    protected void Update()
    {

        foreach (SSAction action in actionList)
            actions[action.GetInstanceID()] = action;
        actionList.Clear();

        foreach (KeyValuePair<int, SSAction> keyPair in actions)
        {
            if (keyPair.Value.destroy)
            {
                deleteList.Add(keyPair.Value.GetInstanceID());
            }
            else if (keyPair.Value.enable)
            {
                keyPair.Value.Update();
            }
        }

        foreach (int key in deleteList)
        {
            SSAction action = actions[key];
            actions.Remove(key);
            Destroy(action);
        }
        deleteList.Clear();
    }

    public void RunAction(GameObject gameObject, SSAction action, ISSActionCallback manager)
    {
        action.transform = gameObject.transform;
        action.gameObject = gameObject;
        action.callback = manager;
        actionList.Add(action);
        action.Start();
    }
    

}
