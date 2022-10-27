using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("global").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), 30 * Time.deltaTime);
        GameObject.Find("global").transform.Rotate(Vector3.up * Time.deltaTime*10000);
        GameObject.Find("Mercury").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), 20 * Time.deltaTime);
        GameObject.Find("Mercury").transform.Rotate(Vector3.up * Time.deltaTime * 10000);
        GameObject.Find("gold").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), 45 * Time.deltaTime);
        GameObject.Find("gold").transform.Rotate(Vector3.up * Time.deltaTime * 10000);
        GameObject.Find("juiter").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), 35 * Time.deltaTime);
        GameObject.Find("juiter").transform.Rotate(Vector3.up * Time.deltaTime * 10000);
        GameObject.Find("Saturn").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), 50 * Time.deltaTime);
        GameObject.Find("Saturn").transform.Rotate(Vector3.up * Time.deltaTime * 10000);
        GameObject.Find("spark").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), 15 * Time.deltaTime);
        GameObject.Find("spark").transform.Rotate(Vector3.up * Time.deltaTime * 10000);
        GameObject.Find("sky").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), 25 * Time.deltaTime);
        GameObject.Find("sky").transform.Rotate(Vector3.up * Time.deltaTime * 10000);
        GameObject.Find("sea").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), 15 * Time.deltaTime);
        GameObject.Find("sea").transform.Rotate(Vector3.up * Time.deltaTime * 10000);
        
      
    }
}
