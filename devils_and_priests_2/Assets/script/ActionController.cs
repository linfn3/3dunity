using System.Collections.Generic;
using UnityEngine;
using MyNamespace;


public class SSAction : ScriptableObject
{ 
    public bool enable = true; 
    public bool destroy = false; 
    public GameObject GameObject { get; set; }
    public Transform Transform { get; set; }
    public ActionCallback Callback { get; set; }

    public virtual void Start()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
}


public class Moveaction : SSAction
{

    public Vector3 target;
    public float speed;

    private Moveaction() { }

    public static Moveaction GetSSMoveToAction(Vector3 goal, float speed)
    {
        Moveaction action = CreateInstance<Moveaction>();
        action.target = goal;
        action.speed = speed;
        return action;
    }
    public override void Start() { }


    public override void Update()
    {
        Transform.position = Vector3.MoveTowards(Transform.position, target, speed * Time.deltaTime);
        if (Transform.position == target)
        {
            destroy = true;
            Callback.ActionDone(this);
        }
    }
}

public class ConstituteAction : SSAction, ActionCallback
{
    public int tmp = -1;
    public int ActionIndex = 0;
    public List<SSAction> sequence;

    public static ConstituteAction GetConstitueAction(int repeat, int currentActionIndex, List<SSAction> sequence)
    {
        ConstituteAction action = CreateInstance<ConstituteAction>();
        action.sequence = sequence;
        action.tmp = repeat;
        action.ActionIndex = currentActionIndex;
        return action;
    }
    public override void Update()
    {
        if (sequence.Count == 0) return;
        if (ActionIndex < sequence.Count)
        {
            sequence[ActionIndex].Update();
        }
    }


    public override void Start()
    {
        foreach (SSAction action in sequence)
        {
            action.GameObject = GameObject;
            action.Transform = Transform;
            action.Callback = this;
            action.Start();
        }
    }
    void OnDestroy()
    {
        foreach (SSAction action in sequence)
        {
            Destroy(action);
        }
    }

    public void ActionDone(SSAction source)
    {
        source.destroy = false;
        ActionIndex++;
        if (ActionIndex >= sequence.Count)
        {
            ActionIndex = 0;
            if (tmp > 0) tmp--;
            if (tmp == 0)
            {
                destroy = true;
                Callback.ActionDone(this);
            }
        }
    }
}

public class ActionManager : MonoBehaviour, ActionCallback
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();

    private List<int> deletelist = new List<int>();
    private List<SSAction> addlist = new List<SSAction>();

    protected void Update()
    {
        foreach (SSAction action in addlist)
        {
            actions[action.GetInstanceID()] = action;
        }
        addlist.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction action = kv.Value;
            if (action.destroy)
            {
                deletelist.Add(action.GetInstanceID());
            }
            else if (action.enable)
            {
                action.Update();
            }
        }

        foreach (int key in deletelist)
        {
            SSAction action = actions[key];
            actions.Remove(key);
            Destroy(action);
        }
        deletelist.Clear();
    }

    public void AddAction(GameObject gameObject, SSAction action, ActionCallback callback)
    {
        action.Transform = gameObject.transform;
        action.Callback = callback;
        action.GameObject = gameObject;
        addlist.Add(action);
        action.Start();
    }

    public void ActionDone(SSAction source) { }
}
