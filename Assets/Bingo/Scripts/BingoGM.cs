using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BingoGM : MonoBehaviour
{
    int num = -1;
    int[] bingoNum = new int[25];
    [SerializeField] GameObject obj = null;
    [SerializeField] GameObject cell = null;
    Text text;
    void Start()
    {
        for (int i = 0; i < bingoNum.Length; i++)
        {
            GameObject game = Instantiate(cell, obj.transform);

            if (i == 12) //真ん中
            {
                bingoNum[i] = -1;
                text = game.GetComponent<Text>();
                text.text = "〇";
                text.fontSize = 24;
                text.color = Color.red;
            }
            else
            {
                //bingoNum[i] = Random.Range(1, 50); //重複する
                bingoNum[i] = i;
                text = game.GetComponent<Text>();
                text.text = bingoNum[i].ToString();
            }
        }
    }

    public void SameNum(int num)
    {
        for (int i = 0; i < bingoNum.Length; i++)
        {
            if (bingoNum[i] == num)
            {
                Debug.Log(bingoNum[i]);
                bingoNum[i] = -1;
            }
        }
    }

    public void Check()
    {
        for (int i = 0; i < 5; i++)
        {
            int count = 0;
            for (int k = 0; k < 5; k++)
            {
                if (bingoNum[i * 5] == -1)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void NumGet(int a)
    {
        num = a;
    }
}
