using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BingoGame : MonoBehaviour
{
    [SerializeField] GridLayoutGroup layoutGroup = null;
    [SerializeField] BCell cell = null;
    [SerializeField] int size = 5;
    [SerializeField] int max = 100;
    [SerializeField] GameObject panel = null;

    int[] num;
    int nokori;

    BCell[,] cells;
    void Start()
    {
        if (size % 2 == 0)
        {
            size++;
        }
        if (max < size * size)
        {
            max = size * size;
        }
        layoutGroup.constraintCount = size;

        nokori = max - 1;
        num = new int[max];
        for (int i = 0; i < num.Length; i++)
        {
            num[i] = i + 1;
        }
        cells = new BCell[size, size];
        Naraberu();
    }

    public void Retry()
    {
        foreach (var item in cells)
        {
            Destroy(item.gameObject);
        }
        nokori = max - 1;
        Naraberu();
    }

    void Naraberu()
    {
        int center = size / 2;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                int r = Random.Range(0, nokori);

                cells[x, y] = Instantiate(cell, layoutGroup.transform);
                if (y == center && x ==center)
                {
                    cells[x, y].Number = -1;
                }
                else
                {
                    cells[x, y].Number = num[r];
                }

                int a = num[nokori];
                num[nokori] = num[r];
                num[r] = a;
                nokori--;
            }
        }
        nokori = max - 1;
    }

    public void Mawasu()
    {
        int tamaNum = Random.Range(0, nokori);

        foreach (var item in cells)
        {
            if (item.Number == num[tamaNum])
            {
                item.Number = -1; // 当たった
            }
        }
        Debug.Log(num[tamaNum]);

        int a = num[nokori];
        num[nokori] = num[tamaNum];
        num[tamaNum] = a;
        nokori--;

        Hanntei();
    }

    void Hanntei()
    {
        int count;
        // 横の判定
        for (int y = 0; y < size; y++)
        {
            count = 0;
            for (int x = 0; x < size; x++)
            {
                if (cells[x, y].Number == -1)
                {
                    count++;
                }
            }
            if (count == size)
            {
                Debug.Log("Bingo");
                panel.SetActive(true);
            }
        }

        // 縦の判定
        for (int x = 0; x < size; x++)
        {
            count = 0;
            for (int y = 0; y < size; y++)
            {
                if (cells[x, y].Number == -1)
                {
                    count++;
                }
            }
            if (count == size)
            {
                Debug.Log("Bingo");
                panel.SetActive(true);
            }
        }

        // 斜めの判定 できてない
        count = 0; //　左上から右下
        int count2 = 0; // 右上から左下
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (y == x)
                {
                    if (cells[x, y].Number == -1)
                    {
                        count++;
                    }
                }
                if (y + x == size - 1)
                {
                    if (cells[x, y].Number == -1)
                    {
                        count2++;
                    }
                }
            }
        }

        if (count == size || count2 == size)
        {
            Debug.Log("Bingo");
            panel.SetActive(true);
        }
    }
}
