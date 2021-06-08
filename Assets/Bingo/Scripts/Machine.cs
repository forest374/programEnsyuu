using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour
{
    [SerializeField] GameObject textObj = null;
    [SerializeField] GameObject GM = null;
    BingoGM bingoGM;
    Text text;
    void Start()
    {
        bingoGM = GM.GetComponent<BingoGM>();
        text = textObj.GetComponent<Text>();
        text.text = "";
    }


    public void Select()
    {
        int num = Random.Range(1, 51);
        text.text = num.ToString();
        bingoGM.SameNum(num);
    }
}
