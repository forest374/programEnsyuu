using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingoCell : MonoBehaviour
{
    [SerializeField] GameObject obj = null;
    [SerializeField] GameObject text = null;

    void Start()
    {
        for (int i = 0; i < 25; i++)
        {
            Instantiate(text, obj.transform);
        }
    }

}
