using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum CellState
{
    None = 0,
    One = 1,
    two = 2,
    three = 3,
    four = 4,
    five = 5,
    six = 6,
    seven = 7,
    eight = 8,
    mine = -1
}

public class MSCell : MonoBehaviour
{
    
    [SerializeField] Text _view = null;
    [SerializeField] Image panel = null;
    [SerializeField] private CellState cellState = CellState.None;
    bool open = false;
    bool flag = false;
    public bool Open { get => open; set { open = value; } }
    public bool Flag { get => flag; set { flag = value; } }
    public CellState CellState
    {
        get => cellState;
        set
        {
            cellState = value;
            OnCellStateChange();
        }
    }

    private void OnValidate()
    {
        OnCellStateChange();
    }
    private void OnCellStateChange()
    {
        if (_view == null) { return; }

        if (cellState == CellState.None)
        {
            _view.text = "";
        }
        else if (cellState == CellState.mine)
        {
            _view.text = "X";
        }
        else
        {
            _view.text = ((int)cellState).ToString();
        }
    }

    public void Hit()
    {
        Open = true;
        Destroy(panel);
    }

    public void PanelColorChange()
    {
        panel.color = Color.red;
    }
}
