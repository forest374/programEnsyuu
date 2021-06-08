using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RManager : MonoBehaviour
{
    [SerializeField] Reversi reversi = null;
    [SerializeField] Text winLose = null;
    TileState myColor = TileState.Black;
    PointerEventData pointer;

    int lastTurnPutNum = 0;
    void Start()
    {
        pointer = new PointerEventData(EventSystem.current);
        reversi.Highlighting(myColor);

    }


    void Update()
    {
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
