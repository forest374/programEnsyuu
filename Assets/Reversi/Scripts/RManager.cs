using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class RManager : MonoBehaviour
{
    [SerializeField] Reversi reversi = null;
    [SerializeField] Text winLose = null;
    //[SerializeField] Text whiteTurn = null;
    //[SerializeField] Text blackTurn = null;

    int stoneCount;
    int[,] setCount;
    float timer = 0;

    TileState myColor = TileState.Black;
    PointerEventData pointer;

    int lastTurnPutNum = 1;
    void Start()
    {
        pointer = new PointerEventData(EventSystem.current);
        reversi.Highlighting(myColor);
        setCount = new int[8, 8];
    }


    void Update()
    {

        switch (myColor)
        {
            case TileState.None:
                break;
            case TileState.White:

                if (timer > 1.75)
                {
                    timer = 0;
                    WhiteTurn();
                }
                else
                {
                    timer += Time.deltaTime;
                }

                break;
            case TileState.Black:
                // クリックしたら
                if (Input.GetMouseButtonDown(0))
                {
                    List<RaycastResult> results = new List<RaycastResult>();
                    // マウスポインタの位置にレイ飛ばし、ヒットしたものを保存
                    pointer.position = Input.mousePosition;
                    EventSystem.current.RaycastAll(pointer, results);
                    // ヒットしたUIの名前
                    foreach (RaycastResult item in results)
                    {
                        Tile tile = item.gameObject.GetComponent<Tile>();
                        if (tile && tile.TileState == TileState.None && tile.Highlight.gameObject.activeSelf)
                        {
                            reversi.PutStone(myColor, tile);
                            ColorChange();
                            reversi.Highlighting(myColor);
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    private void WhiteTurn()
    {
        int min = int.MaxValue;
        int max = int.MinValue;
        int minX = int.MaxValue;
        int minY = int.MaxValue;
        int maxX = int.MaxValue;
        int maxY = int.MaxValue;


        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (reversi.Tiles[x, y].TileState != TileState.None)
                {
                    stoneCount++;
                }
                int set = reversi.Tiles[x, y].Count;
                setCount[x, y] = set;
                if (setCount[x, y] > 0 && min > setCount[x, y])
                {
                    min = setCount[x, y];
                    minX = x;
                    minY = y;
                }
                else if (min == setCount[x, y])
                {
                    if (Random.Range(0, 3) == 2)
                    {
                        min = setCount[x, y];
                        minX = x;
                        minY = y;
                    }
                }
                if (setCount[x, y] > 0 && max < setCount[x, y])
                {
                    max = setCount[x, y];
                    maxX = x;
                    maxY = y;
                }
                else if (max == setCount[x, y])
                {
                    if (Random.Range(0, 3) == 2)
                    {
                        min = setCount[x, y];
                        minX = x;
                        minY = y;
                    }
                }
            }
        }
        if (minX == int.MaxValue || maxX == int.MaxValue)
        {
            return;
        }

        Debug.Log(stoneCount);
        if (stoneCount < 35)
        {
            reversi.PutStone(myColor, reversi.Tiles[minX, minY]);
        }
        else
        {
            reversi.PutStone(myColor, reversi.Tiles[maxX, maxY]);
        }
        stoneCount = 0;
        ColorChange();
        reversi.Highlighting(myColor);
    }

    void ColorChange()
    {
        if (myColor == TileState.Black)
        {
            myColor = TileState.White;
        }
        else if (myColor == TileState.White)
        {
            myColor = TileState.Black;
        }
    }

    public void PutNumCount(int putNum)
    {
        lastTurnPutNum = putNum;
    }

    public void TurnPass(int putNum)
    {
        if (lastTurnPutNum == 0 && putNum == 0)
        {
            End();
            return;
        }

        lastTurnPutNum = putNum;
        ColorChange();
        reversi.Highlighting(myColor);
    }

    void End()
    {
        int black = reversi.StoneCount(TileState.Black);
        int white = reversi.StoneCount(TileState.White);

        if (black > white)
        {
            winLose.gameObject.SetActive(true);
            Debug.Log("黒WIN");
            winLose.text = "黒WIN";
        }
        else
        {
            winLose.gameObject.SetActive(true);
            Debug.Log("白WIN");
            winLose.text = "白WIN";
        }
    }
}
