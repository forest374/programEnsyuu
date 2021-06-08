using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LGCell : MonoBehaviour
{
    GameObject map = null;
    LifeGameMap mapS = null;
    [SerializeField] GameObject imageObj = null;
    Image image;
    public bool life = false;
    Vector2[] around = new Vector2[] { Vector2.right, Vector2.left, Vector2.down, Vector2.up, Vector2.one, Vector2.one * -1, new Vector2(1, -1), new Vector2(-1, 1) };

    void Start()
    {
        map = GameObject.Find("Map");
        mapS = map.GetComponent<LifeGameMap>();
        image = imageObj.GetComponent<Image>();
    }

    public void GenerationUpdate()
    {
        int num = Check();

        if (life)
        {
            if (num < 2)//周囲の生きているcellの数が2つ未満のとき死滅する
            {
                Depopulation();
            }
            if (num > 3)//周囲の生きているcellの数が3より大きいとき死滅する
            {
                Depopulation();
            }
        }
        else if(num == 3)//周囲の生きているcellの数が3つのとき誕生する
        {
            Birth();
        }
         
    }

    /// <summary>
    /// 周囲の生きているcellの数を調べる
    /// </summary>
    public int Check()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            Vector2 point = (Vector2)this.transform.position + around[i];
            if (0 <= point.x && 0 <= point.y && mapS.mapRange.x > point.x && mapS.mapRange.y > point.y)
            {
                GameObject cellObj = mapS.Search(point);
                LGCell cell = cellObj.GetComponent<LGCell>();
                if (cell.life)
                {
                    count++;
                }
            }
        }

        return count;
    }

    /// <summary>
    /// 誕生
    /// </summary>
    public void Birth()
    {
        life = true;
        image.color = Color.black;
    }

    /// <summary>
    /// 死滅
    /// </summary>
    public void Depopulation()
    {
        life = false;
        image.color = Color.white;
    }
}
