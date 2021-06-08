using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    [SerializeField] Minesweeper minesweeper = null;
    [SerializeField] Button nextButton = null;
    bool startClick = true;
    int clickNum = 0;
    public bool StartClick { get => startClick; set { startClick = value; } }
    PointerEventData pointer;
    void Start()
    {
        pointer = new PointerEventData(EventSystem.current);
        nextButton.interactable = false;
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
            foreach (RaycastResult target in results)
            {
                if (startClick)
                {
                    minesweeper.MineSet(target.gameObject.GetComponent<MSCell>());
                    startClick = false;
                }
                //Debug.Log(target.gameObject.name);
                MSCell cell = target.gameObject.GetComponent<MSCell>();
                minesweeper.CellOpen(cell);
            }

            if (clickNum > 5)
            {
                nextButton.interactable = true;
            }
            else
            {
                clickNum++;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            List<RaycastResult> results = new List<RaycastResult>();
            // マウスポインタの位置にレイ飛ばし、ヒットしたものを保存
            pointer.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointer, results);

            foreach (var target in results)
            {
                MSCell cell = target.gameObject.GetComponent<MSCell>();
                if (cell.Flag == true || cell.Open)
                {
                    return;
                }
                minesweeper.Flag(cell);
            }

            if (clickNum > 5)
            {
                nextButton.interactable = true;
            }
            else
            {
                clickNum++;
            }
        }
    }

    public void NextMinesweeper()
    {
        startClick = true;
        nextButton.interactable = false;
        clickNum = 0;
    }
}
