using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGame : MonoBehaviour
{
    [SerializeField] int countNum = 60;
    [SerializeField] LCell cell = null;
    [SerializeField] int columns = 15;
    [SerializeField] int row = 10;
    [SerializeField] GridLayoutGroup layoutGroup = null;
    LCell[,] cells;

    int[,] aliveNum;
    int count = 0;
    void Start()
    {
        layoutGroup.constraintCount = columns;
        cells = new LCell[columns, row];
        aliveNum = new int[columns, row];
        Naraberu();
    }

    private void FixedUpdate()
    {
        count++;
        if (count == countNum)
        {
            count = 0;
        }
        else
        {
            return;
        }
        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                aliveNum[x, y] = AroundCells(x, y);
            }
        }

        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                cells[x, y].DeadOrAlive(aliveNum[x, y]);
            }
        }
    }
    void Naraberu()
    {
        for (int i = 0; i < row; i++)
        {
            for (int k = 0; k < columns; k++)
            {
                cells[k, i] = Instantiate(cell, layoutGroup.transform);

                // Randomに生き返す
                //int a = Random.Range(0, 2);
                //if (a == 0)
                //{
                //    cells[k, i].Alive = true;
                //}
            }
        }
    }

    /// <summary>
    /// 渡された位置のセルの周囲のセルの生存を調べてその数を返す
    /// </summary>
    /// <param name="x">セルのｘ</param>
    /// <param name="y">セルのｙ</param>
    /// <returns>生存数</returns>
    int AroundCells(int x, int y)
    {
        int count = 0;
        
        for (int i = -1; i < 2; i++)
        {
            for (int k = -1; k < 2; k++)
            {
                if (i == 0 && k == 0)
                {
                    continue;
                }
                int xx = x + k;
                int yy = y + i;

                if (xx >= 0 && xx < columns &&
                    yy >= 0 && yy < row)
                {
                    if (cells[xx, yy].Alive)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    public void Block()
    {
        int x = columns / 2;
        int y = row / 2;
        int xx;
        int yy;
        for (int i = -1; i < 1; i++)
        {
            for (int k = -1; k < 1; k++)
            {
                xx = x + k;
                yy = y + i;
                cells[xx, yy].Alive = true;
            }
        }
    }
    
    public void Graider()
    {
        int x = columns / 2;
        int y = row / 2;
        int xx;
        int yy;
        for (int i = -1; i < 2; i++)
        {
            for (int k = -1; k < 2; k++)
            {
                xx = x + k;
                yy = y + i;
                if (i == 1)
                {
                    cells[xx, yy].Alive = true;
                }
                else if(i == 0 && k == -1)
                {
                    cells[xx, yy].Alive = true;
                }
                else if(i == -1 && k == 0)
                {
                    cells[xx, yy].Alive = true;
                }
            }
        }
    }

    public void RandomSet()
    {
        for (int i = 0; i < row; i++)
        {
            for (int k = 0; k < columns; k++)
            {
                // Randomに生き返す
                int a = Random.Range(0, 2);
                if (a == 0)
                {
                    cells[k, i].Alive = true;
                }
            }
        }
    }
}
