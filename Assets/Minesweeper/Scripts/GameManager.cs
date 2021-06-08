using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        
    }

    public void ClearChack()
    {
        int cloudy = GameObject.FindGameObjectsWithTag("Cloudy").Length;
        if (cloudy == 1)// 最後の1個をクリックしたらクリア
        {
            // クリア　
            Clear();
        }
    }

    void Clear()
    {
        Debug.Log("CLEAR");
    }
    public void GameOver()
    {
        Debug.Log("GAME OVER");
    }
}
