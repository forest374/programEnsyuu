using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TileState
{
    None, White, Black
}

public class Tile : MonoBehaviour
{
    [SerializeField] Image stone = null;
    [SerializeField] Image highlight = null;

    int count = 0;
    public Image Highlight { get => highlight; set {highlight = value; } }
    TileState tileState = TileState.None;
    public TileState TileState { get => tileState; set { tileState = value; OnStateChange(); } }
    public int Count { get => count; set { count = value; } }


    private void OnValidate()
    {
        OnStateChange();
    }

    void OnStateChange()
    {
        switch (TileState)
        {
            case TileState.None:
                break;
            case TileState.White:
                stone.gameObject.SetActive(true);
                stone.color = Color.white;
                break;
            case TileState.Black:
                stone.gameObject.SetActive(true);
                stone.color = Color.black;
                break;
            default:
                break;
        }
    }
}
