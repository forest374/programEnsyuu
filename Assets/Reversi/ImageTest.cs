using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTest : MonoBehaviour
{
    [SerializeField] Image image = null;
    float fill = 0;

    bool turn = false;
    bool omote = true;

    public void TurnTrue()
    {
        fill = image.fillAmount;
        if (fill > 0.5f)
        {
            omote = true;
        }
        else
        {
            omote = false;
        }
        turn = true;
    }

    void Update()
    {
        if (turn)
        {
            if (fill < 1 && !omote)
            {
                fill += 0.01f;
                image.fillAmount = fill;
            }
            else if (fill > 0 && omote)
            {
                fill -= 0.01f;
                image.fillAmount = fill;
            }
            else
            {
                turn = false;
            }
        }
    }
}
