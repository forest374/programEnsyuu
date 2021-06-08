using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reversi : MonoBehaviour
{
    [SerializeField] Tile tile = null;
    [SerializeField] LayoutGroup layoutGroup = null;
    [SerializeField] RManager manager = null;
    Tile[,] tiles;
    int size = 8;
    TileState enemyState = TileState.Black;

    private void Awake()
    {
        tiles = new Tile[size, size];
        Naraberu();
    }

    /// <summary>
    /// tileを並べ石の初期配置を行う
    /// </summary>
    void Naraberu()
    {
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                tiles[x, y] = Instantiate(tile, layoutGroup.transform);

                // 石の初期位置
                if ((y == 3 && x == 3) || (y == 4 && x == 4))
                {
                    tiles[x, y].TileState = TileState.White;
                }
                if ((y == 3 && x == 4) || (y == 4 && x == 3))
                {
                    tiles[x, y].TileState = TileState.Black;
                }
            }
        }
    }

    /// <summary>
    /// 置く場所のタイルと自分の石の色を受け取り自分の石を置く
    /// </summary>
    /// <param name="turnState">自分の石の色</param>
    /// <param name="tile">置く場所のタイル</param>
    public void PutStone(TileState turnState, Tile tile)
    {
        if (turnState == TileState.Black)
        {
            enemyState = TileState.White;
        }
        else if (turnState == TileState.White)
        {
            enemyState = TileState.Black;
        }
        else
        {
            return;
        }

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (tiles[x, y] == tile)
                {
                    if (tiles[x, y].TileState == TileState.None)
                    {
                        tiles[x, y].TileState = turnState;
                        ReverseCheck(enemyState, x, y);
                    }
                }
            }
        }
    }
    /// <summary>
    /// ひっくり返せるかを調べる
    /// </summary>
    /// <param name="state">敵の色</param>
    /// <param name="tilesX">石を置くタイルの位置X</param>
    /// <param name="tilesY">石を置くタイルの位置Y</param>
    void ReverseCheck(TileState state, int tilesX, int tilesY)
    {
        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                int CheckX = tilesX + x;
                int CheckY = tilesY + y;
                // eria外
                if (CheckX < 0 || CheckX >= size ||
                    CheckY < 0 || CheckY >= size)
                {
                }
                else if (tiles[CheckX, CheckY].TileState == state)
                {
                    bool check = Check(state, CheckX, CheckY, x, y);
                    if (check)
                    {
                        Reverse(state, CheckX, CheckY, x, y);
                    }
                }
            }
        }
    }

    /// <summary>
    /// ひっくり返す
    /// </summary>
    /// <param name="state">敵の色</param>
    /// <param name="lastX">前回のタイルX</param>
    /// <param name="lastY">前回のタイルY</param>
    /// <param name="x">移動してきた向きX</param>
    /// <param name="y">移動してきた向きY</param>
    void Reverse(TileState state, int lastX, int lastY, int x, int y)
    {
        int CheckX = lastX;
        int CheckY = lastY;
        if (state == TileState.White)
        {
            tiles[CheckX, CheckY].TileState = TileState.Black;
        }
        else if (state == TileState.Black)
        {
            tiles[CheckX, CheckY].TileState = TileState.White;
        }

        //　次もできるか調べて、できるならひっくり返す
        CheckX = lastX + x;
        CheckY = lastY + y;
        if (tiles[CheckX, CheckY].TileState == state)
        {
            Reverse(state, CheckX, CheckY, x, y);
        }

    }

    /// <summary>
    /// 自分のstoneの色を受け取り置けるところを強調する
    /// </summary>
    /// <param name="turnState">自分のstone</param>
    public void Highlighting(TileState turnState)
    {
        foreach (var item in tiles)
        {
            item.Highlight.gameObject.SetActive(false);
        }

        if (turnState == TileState.Black)
        {
            enemyState = TileState.White;
        }
        else if(turnState == TileState.White)
        {
            enemyState = TileState.Black;
        }
        else
        {
            return;
        }

        int count = 0;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (tiles[x, y].TileState == TileState.None)
                {
                    bool put = Check(enemyState, x, y);
                    //　置けるところを強調表示
                    if (put)
                    {
                        count++;
                        tiles[x, y].Highlight.gameObject.SetActive(true);
                    }
                }
            }
        }
        //　置けるところがなかったらturnをパスする
        if (count == 0)
        {
            Debug.Log("pass");
            manager.TurnPass(count);
        }
    }


    /// <summary>
    /// 置けるかどうか調べてboolで返す
    /// </summary>
    /// <param name="state">敵のstate</param>
    /// <param name="tilesX">調べるタイルのX</param>
    /// <param name="tilesY">調べるタイルのY</param>
    /// <returns>bool</returns>
    bool Check(TileState state, int tilesX, int tilesY)
    {
        bool check = false;
        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                int CheckX = tilesX + x;
                int CheckY = tilesY + y;
                if (CheckX < 0 || CheckX >= size || 
                    CheckY < 0 || CheckY >= size)
                {
                }
                else if (tiles[CheckX, CheckY].TileState == state)// 敵の色と同じなら次も調べる
                {
                    check = Check(state, CheckX, CheckY, x, y);
                    if (check)
                    {
                        return check;
                    }
                }
            }
        }
        return check;
    }

    /// <summary>
    /// 敵の色と前回調べたタイルのXYと移動してきた向きを受け取り次の石の色を調べてひっくり返せるかを返す
    /// </summary>
    /// <param name="state">敵の色</param>
    /// <param name="lastX">前回調べたタイルのX</param>
    /// <param name="lastY">前回調べたタイルのY</param>
    /// <param name="x">移動してきた向きX</param>
    /// <param name="y">移動してきた向きY</param>
    /// <returns>ひっくり返せるか</returns>
    bool Check(TileState state, int lastX, int lastY, int x, int y)
    {
        int CheckX = lastX + x;
        int CheckY = lastY + y;
        //　エリア外
        if (CheckX < 0 || CheckX >= size ||
            CheckY < 0 || CheckY >= size)
        {
            return false;
        }
        else if (tiles[CheckX, CheckY].TileState == state)// 敵の色と同じなら次も調べる
        {
            bool check = Check(state, CheckX, CheckY, x, y);
            return check;
        }
        else if (tiles[CheckX, CheckY].TileState == TileState.None)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 渡された色の石の数を返す
    /// </summary>
    /// <param name="state">石の色</param>
    /// <returns>石の数</returns>
    public int StoneCount(TileState state)
    {
        int count = 0;
        foreach (var item in tiles)
        {
            if (item.TileState == state)
            {
                count++;
            }
        }
        return count;
    }
}
