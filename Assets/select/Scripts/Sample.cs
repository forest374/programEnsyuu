using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    [SerializeField] int numX = 5;
    [SerializeField] int numY = 5;
    GameObject[,] cubes;
    int selectNum = 0;


    void Start()
    {
        cubes = new GameObject[numX, numY];
        for (int i = 0; i < numX; i++)
        {
            for (int k = 0; k < numY; k++)
            {
                cubes[i,k] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubes[i,k].transform.position = new Vector3(-4f + (i * 2), k * -2, 0);
                var color = cubes[i,k].GetComponent<Renderer>();
                color.material.color = (k * numX + i == selectNum ? Color.red : Color.white);
            }
        }
    }


    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Horizontal"))
        {
            ButtonDown(h);
        }
        if (Input.GetKeyDown("w"))
        {
            ButtonDownW();
        }
        if (Input.GetKeyDown("s"))
        {
            ButtonDownS();
        }

        //if (Input.GetButtonDown("Enter"))
        //{
        //    cubes[selectNum].SetActive(false);

        //    //selectNum = int.MaxValue;
        //    //ColorChange();
        //}
    }

    private void ButtonDown(float h)
    {
        if (h > 0 && selectNum < cubes.Length - 1)
        {
            selectNum++;
            // setActive を検知
            //for (; !cubes[selectNum].activeSelf;)
            //{
            //    selectNum++;
            //    if (selectNum > num - 1)
            //    {
            //        selectNum = i;
            //        break;
            //    }
            //}
        }
        else if (h < 0 && selectNum > 0)
        {
            selectNum--;
        }
        ColorChange();
    }

    void ButtonDownW()
    {
        if (selectNum >= numX)
        {
            selectNum -= numX;
            ColorChange();
        }
    }
    void ButtonDownS()
    {
        if (selectNum < numX * (numY - 1))
        {
            selectNum += numX;
            ColorChange();
        }
    }

    void ColorChange()
    {
        for (int i = 0; i < numX; i++)
        {
            for (int k = 0; k < numY; k++)
            {
                var color = cubes[i,k].GetComponent<Renderer>(); 
                color.material.color = (k * numX + i == selectNum ? Color.red : Color.white);
            }
        }
    }
}
