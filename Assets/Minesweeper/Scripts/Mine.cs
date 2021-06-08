using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    GameObject gM = null;
    GameManager gameManager;
    //GameObject map = null;
    //Map mapS;
    //GameObject cloudy = null;
    GameObject flag = null;
    void Start()
    {
        gM = GameObject.Find("GameManager");
        gameManager = gM.GetComponent<GameManager>();
        //map = GameObject.Find("Map");
        //mapS = map.GetComponent<Map>();
        //cloudy = transform.Find("Cloudy").gameObject;
        flag = transform.Find("flag").gameObject;
        flag.SetActive(false);
    }
    public void FlagOn()
    {
        if (flag && !flag.activeSelf)
        {
            flag.SetActive(true);
        }
        else
        {
            flag.SetActive(false);
        }
    }
    public void ClickHit()
    {
        //game Over
        flag.SetActive(false);
        gameManager.GameOver();
    }
}
