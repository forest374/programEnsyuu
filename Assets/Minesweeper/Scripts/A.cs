using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class A : MonoBehaviour
{
    [SerializeField] Minesweeper minesweeper = null;
    bool startClick = true;
    public bool StartClick { get => startClick; set { startClick = value; } }
    PointerEventData pointer;
    void Start()
    {
        pointer = new PointerEventData(EventSystem.current);
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
                minesweeper.Flag(cell);
            }

        }
    }
}
