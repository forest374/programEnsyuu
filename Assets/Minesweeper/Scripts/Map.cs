using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField] int mineNum = 3;
    [SerializeField] int cellNum = 50;
    [SerializeField] int columns = 15;
    [SerializeField] TCell cell = null;
    [SerializeField] GridLayoutGroup layout = null;
    TCell[] cells;

    void Start()
    {
        cells = new TCell[cellNum];
        layout.constraintCount = columns;
        CellCreate();
    }

    /// <summary>
    /// cellを並べる
    /// </summary>
    void CellCreate()
    {
        for (int i = 0; i < cellNum; i++)
        {
            cells[i] = Instantiate(this.cell, layout.transform);
            cells[i].name = "cell \"" + i + "\"";
        }
    }

    void MineSet()
    {
        for (int i = 0; i < mineNum; i++)
        {
            int mine = UnityEngine.Random.Range(0, cellNum);
            if (cells[mine].CellState == CellState.mine)
            {
                i--;
                continue;
            }
            else
            {
                cells[mine].CellState = CellState.mine;
            }
        }
    }

}
