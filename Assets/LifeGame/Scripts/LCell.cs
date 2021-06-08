using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LCell : MonoBehaviour
{
    [SerializeField] Image image = null;
    bool alive = false;
    public bool Alive { 
        get => alive; 
        set { alive = value; 
            OnCellStateChange(); } }


    private void OnValidate()
    {
        OnCellStateChange();
    }
    public void DeadOrAlive(int num)
    {
        if (alive)
        {
            if (num > 3 || num < 2)
            {
                Alive = false;
            }
        }
        else
        {
            if (num == 3)
            {
                Alive = true;
            }
        }
    }

    void OnCellStateChange()
    {
        if (alive)
        {
            image.color = Color.black;
        }
        else
        {
            image.color = Color.white;
        }
    }
}
