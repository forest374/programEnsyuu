using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LGGameManager : MonoBehaviour
{
    GameObject[] cells;
    int count = 0;
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("cell");
        Debug.Log(cells.Length);
    }

    private void FixedUpdate()
    {
        count++;
        if (count >= 10)
        {
            foreach (var item in cells)
            {
                LGCell cell = item.GetComponent<LGCell>();
                cell.GenerationUpdate();
            }
            count = 0;
        }
        
    }
}
