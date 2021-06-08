using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour
{
    [SerializeField] int mineNum = 3;
    [SerializeField] int columns = 15;
    int cellNum;

    [SerializeField] MSCell cell = null;
    [SerializeField] ClickManager clickM = null;
    [SerializeField] GridLayoutGroup layout = null;
    [SerializeField] Text scoreText = null;
    [SerializeField] Text flagText = null;
    [SerializeField] Text panelText = null;
    [SerializeField] Text flagMissText = null;
    [SerializeField] Text mineMissText = null;
    MSCell[,] cells;

    int score = 0;
    int flag = 0;
    int panel = 0;
    int flagMiss = 0;
    int mineMiss = 0;
    public int Score { get => score; set {score = value; } }
    void Start()
    {
        cellNum = columns * columns;
        cells = new MSCell[columns, columns];
        layout.constraintCount = columns; 
        ScoreDisplay();
        CellCreate();
    }

    /// <summary>
    /// cellを並べる
    /// </summary>
    void CellCreate()
    {
        for (int y = 0; y < columns; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                cells[x, y] = Instantiate(this.cell, layout.transform);
                cells[x, y].name = "cell (" + x + "," + y + ")";
            }
        }
    }

    /// <summary>
    /// 地雷を設置する
    /// </summary>
    public void MineSet(MSCell startHit)
    {
        (int x, int y) hitPoint = CellsSearch(startHit);
        (int x, int y)[] aroundPoint = new (int x, int y)[9] 
        { (-1,-1), (-1, 0), (-1, 1), (0, -1), (0, 0), (0, 1), (1, -1), (1, 0), (1, 1)};
        for (int i = 0; i < mineNum; i++)
        {
            bool startPoint = false;
            int mine = UnityEngine.Random.Range(0, cellNum);
            (int x, int y) minePoint = (mine % columns, mine / columns);

            //最初に選んだ周囲のcellにもmineを設置させない
            for (int f = 0; f < aroundPoint.Length; f++)
            {
                (int x, int y) point = (hitPoint.x + aroundPoint[f].x, hitPoint.y + aroundPoint[f].y);
                if (minePoint == point)
                {
                    startPoint = true;
                }
            }

            if (cells[minePoint.x, minePoint.y].CellState == CellState.mine || 
                startPoint)
            {
                i--;
                continue;
            }
            else
            {
                cells[minePoint.x, minePoint.y].CellState = CellState.mine;
            }
        }
        CellsNumSet();
    }

    /// <summary>
    /// 渡されたCellの座標を返す
    /// </summary>
    /// <param name="startHit">Cell</param>
    /// <returns>座標</returns>
    (int x, int y) CellsSearch(MSCell startHit)
    {
        (int x, int y) hitPoint = (-1, -1);
        for (int y = 0; y < columns; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                if (cells[x, y] == startHit)
                {
                    hitPoint = (x, y);
                }
            }
        }
        return hitPoint;
    }

    void CellsNumSet()
    {
        for (int y = 0; y < columns; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                (int x, int y) point = (x, y);
                int num = SearchMineNum(point);
                cells[x, y].CellState = (CellState)num;
            }
        }
    }

    int SearchMineNum((int x, int y) point)
    {
        if (cells[point.x, point.y].CellState == CellState.mine )
        {
            return -1;
        }
        int count = 0;
        for (int y = -1; y < 2; y++)// -1から１まで
        {
            for (int x = -1; x < 2; x++)
            {
                int serachX = point.x + x;
                int serachY = point.y + y;

                if (x == 0 && y == 0)
                {
                    continue;
                }

                if (serachY >= 0 && serachY < columns && serachX >= 0 && serachX < columns)
                {
                    if (cells[serachX, serachY].CellState == CellState.mine)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    public void CellOpen(MSCell cell)
    {
        //Debug.Log("");
        for (int y = 0; y < columns; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                if (cell == cells[x, y])
                {
                    Open(x, y);
                }
            }
        }
    }

    void Open(int x, int y)
    {
        //Debug.Log(x + ", " + y);
        MSCell cell = cells[x, y];
        if (cell.Open)
        {
            return;
        }
        cell.Hit();
        if (cell.CellState == CellState.mine)
        {
            mineMiss++;
            ScoreDisplay();
            Debug.Log("GameOver");
            return;
        }

        panel += 1;
        ScoreDisplay();

        if (cell.CellState == CellState.None)
        {

            for (int v = -1; v < 2; v++)// -1から１まで
            {
                for (int h = -1; h < 2; h++)
                {
                    int xx = x + h;
                    int yy = y + v;

                    if (v == 0 && h == 0)
                    {
                        continue;
                    }

                    if (0 <= xx && xx < columns)
                    {
                        if (0 <= yy && yy < columns)
                        {
                            Open(xx, yy);
                        }
                    }
                }
            }
        }
    }

    public void Flag(MSCell cell)
    {
        if (cell.CellState == CellState.mine)
        {
            flag++;
            ScoreDisplay();
            cell.PanelColorChange();
            cell.Flag = true;
        }
        else
        {
            flagMiss++;
            ScoreDisplay();
        }
    }

    void ScoreDisplay()
    {
        int total = flag * 500 + panel * 3 - flagMiss * 100 - mineMiss * 1000;
        Score = total;
        scoreText.text = Score.ToString("D8");
        flagText.text = flag.ToString("D6");
        panelText.text = panel.ToString("D6");
        flagMissText.text = flagMiss.ToString("D6");
        mineMissText.text = mineMiss.ToString("D6");
    }

    public void NextMineSweeper()
    {
        foreach (var item in cells)
        {
            Destroy(item.gameObject);
        }
        clickM.NextMinesweeper();
        cellNum = columns * columns;
        cells = new MSCell[columns, columns];
        layout.constraintCount = columns;
        ScoreDisplay();
        CellCreate();
    }
}
