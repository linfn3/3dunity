using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    public ActionManager actionManager;                   
    public SaucerFactory saucerFactory;                       
    int[] myRound;           
    int mode;                   
    int scores;                 
    int round;                  
    int count;              
    float shootTime;             

    void Start()
    {
        LoadResources();
    }

    public void LoadResources()
    {
        SSDirector.GetInstance().CurrentScenceController = this;
        gameObject.AddComponent<SaucerFactory>();
        gameObject.AddComponent<ActionManager>();
        gameObject.AddComponent<UserGUI>();
        saucerFactory = Singleton<SaucerFactory>.Instance;
        scores = 0;
       
        myRound = new int[] { 1, 1, 2, 3, 6, 10, 13, 18, 33, 54 };
        round = 1;
        count = 0;
        shootTime = 0;
        mode = 1;
    }

    public void flyingSaucer(int mode)
    {
        GameObject flyingSaucer = saucerFactory.GetSaucer(round-1);
        flyingSaucer.transform.position = new Vector3(-flyingSaucer.GetComponent<SaucerData>().direction.x * 5, UnityEngine.Random.Range(0f, 8f), 0);
        flyingSaucer.SetActive(true);

        if (mode == 2) {
            actionManager.Fly(flyingSaucer, flyingSaucer.GetComponent<SaucerData>().speed*1.2f, flyingSaucer.GetComponent<SaucerData>().direction);
        }
        else {
            actionManager.Fly(flyingSaucer, flyingSaucer.GetComponent<SaucerData>().speed, flyingSaucer.GetComponent<SaucerData>().direction);
        }
        
    }

    public void Hit(Vector3 position)
    {
        Camera camera = Camera.main;
        Ray ray = camera.ScreenPointToRay(position);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            //如果用户点击到飞碟
            if (hit.collider.gameObject.GetComponent<SaucerData>() != null)
            {
                hit.collider.gameObject.transform.position = new Vector3(0, -8, 0);
                scores += hit.collider.gameObject.GetComponent<SaucerData>().points;
                gameObject.GetComponent<UserGUI>().points = scores;
            }
        }
    }
    public void Restart()
    {
        
        round = 1;
        count = 0;
        scores = 0;
        gameObject.GetComponent<UserGUI>().result = "";
        gameObject.GetComponent<UserGUI>().round = round;
        gameObject.GetComponent<UserGUI>().mode = mode;
        gameObject.GetComponent<UserGUI>().points = scores;

    }

    void Update()
    {
        shootTime += Time.deltaTime;
        if (shootTime > 1)
        {
            shootTime = 0;
            for (int i = 0; i < 5 && count < myRound[round-1]; i++)
            {
                count++;
                flyingSaucer(mode);
            }
            if (count == myRound[round-1] && round == myRound.Length)
            {
                    gameObject.GetComponent<UserGUI>().result = "Game Over!";
                
            }
            if (count == myRound[round-1] && round < myRound.Length)
            {
                count = 0;
                round++;
                gameObject.GetComponent<UserGUI>().round = round;
            }
        }
    }

    public void SetMode(int mode)
    {
        this.mode = mode;
    }
}
