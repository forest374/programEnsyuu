using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BCell : MonoBehaviour
{
    [SerializeField] Text text = null;
    [SerializeField] Image image = null;

    int number = 0;
    public int Number { get => number; set { number = value; OnCellStateChange(); } }

    private void OnValidate()
    {
        OnCellStateChange();
    }

    void OnCellStateChange()
    {
        if (Number == -1)
        {
            text.text = "〇";
            text.color = Color.red;
        }
        else
        {
            text.text = number.ToString();
        }
    }
}
