using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LifeGameMap : MonoBehaviour
{
    [SerializeField] public Vector2 mapRange = Vector2.zero;
    [SerializeField] GameObject cell = null;
    [SerializeField] GameObject deadCell = null;
    [SerializeField] GameObject canvas = null;
    GameObject[] cells;

    void Awake()
    {
        cells = new GameObject[(int)mapRange.x * (int)mapRange.y];
        CellPlacement();
    }

    /// <summary>
    /// mapを埋める
    /// </summary>
    void CellPlacement()
    {
        for (int x = 0; x < (int)mapRange.x; x++)
        {
            for (int y = 0; y < (int)mapRange.y; y++)
            {
                int num = (int)mapRange.y * x + y;
                Vector3 point = new Vector3(x, y, 10);
                if (0 == UnityEngine.Random.Range(0, 3))
                {
                    cells[num] = Instantiate(cell, point, Quaternion.identity);
                    cells[num].transform.parent = canvas.transform;
                }
                else
                {
                    cells[num] = Instantiate(deadCell, point, Quaternion.identity);
                    cells[num].transform.parent = canvas.transform;
                }
            }
        }
    }

    /// <summary>
    /// 座標を受け取りその位置のオブジェクトを返す
    /// </summary>
    /// <param name="point">座標</param>
    /// <returns>オブジェクト</returns>
    public GameObject Search(Vector2 point)
    {
        int num = (int)mapRange.y * (int)point.x + (int)point.y;
        return cells[num];
    }
}
