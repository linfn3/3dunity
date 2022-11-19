using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerFactory : MonoBehaviour
{
    public GameObject saucerPrefab;            
    private List<SaucerData> use;                
    private List<SaucerData> free;                
    public void Start()
    {
        use = new List<SaucerData>();
        free = new List<SaucerData>();
        saucerPrefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/saucer"), Vector3.zero, Quaternion.identity);
        saucerPrefab.SetActive(false);
    }

    //生产飞碟
    public GameObject GetSaucer(int round)
    {
        GameObject saucer;
        if (free.Count > 0)
        {
            saucer = free[0].gameObject;
            free.Remove(free[0]);
        }
        else
        {
            saucer = GameObject.Instantiate<GameObject>(saucerPrefab, Vector3.zero, Quaternion.identity);
            saucer.AddComponent<SaucerData>();
        }
        float level = UnityEngine.Random.Range(0, 2f) * (round + 1);
        if (level < 4)
        {
            saucer.GetComponent<SaucerData>().speed = 4.0f;
            saucer.GetComponent<SaucerData>().direction = new Vector3(UnityEngine.Random.Range(-1f, 1f) > 0 ? 2 : -2, 1, 0);
            saucer.GetComponent<SaucerData>().points = 1;
            saucer.GetComponent<Renderer>().material.color = Color.blue; 
        }
        else if (level >= 4 && level < 7)
        {
            saucer.GetComponent<SaucerData>().speed = 6.5f;
            saucer.GetComponent<SaucerData>().direction = new Vector3(UnityEngine.Random.Range(-1f, 1f) > 0 ? 2 : -2, 1, 0);
            saucer.GetComponent<SaucerData>().points = 2;
            saucer.GetComponent<Renderer>().material.color = Color.yellow; 
        }
        else
        {
            saucer.GetComponent<SaucerData>().speed = 8.0f;
            saucer.GetComponent<SaucerData>().direction = new Vector3(UnityEngine.Random.Range(-1f, 1f) > 0 ? 2 : -2, 1, 0);
            saucer.GetComponent<SaucerData>().points = 3;        
            saucer.GetComponent<Renderer>().material.color = Color.red; 
        }

        use.Add(saucer.GetComponent<SaucerData>());

        return saucer;
    }
  
    public void FreeDisk(GameObject saucer)
    {
        foreach (SaucerData diskData in use)
        {
            if (diskData.gameObject.GetInstanceID() == saucer.GetInstanceID())
            {
                saucer.SetActive(false);
                free.Add(diskData);
                use.Remove(diskData);
                break;
            }

        }
    }
}
