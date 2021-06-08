using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    //[SerializeField] public bool mine = false;
    GameObject map = null;
    Map mapS;
    GameObject cloudy = null;
    GameObject flag = null;

    GameObject mineNumText = null;
    Text text;

    Vector2[] aroundCells = new Vector2[]
    {
            new Vector2(-1,1), Vector2.up,Vector2.one, Vector2.left,new Vector2(1,-1), Vector2.down,new Vector2(-1,-1),Vector2.right
    };
    void Start()
    {
        map = GameObject.Find("Map");
        mapS = map.GetComponent<Map>();
        cloudy = transform.Find("Cloudy").gameObject;
        flag = transform.Find("flag").gameObject;
        flag.SetActive(false);
        mineNumText = transform.Find("MineNumText").gameObject;
        text = mineNumText.GetComponent<Text>();
    }

    public void FlagOn()
    {
        if (flag && !flag.activeSelf)
        {
            flag.SetActive(true);
        }
        else
        {
            flag.SetActive(false);
        }
    }

    public void ClickHit()
    {
        if (cloudy)
        {
            Destroy(cloudy);
            Destroy(flag);

            int count = Check();
            if (count <= 0)
            {
                text.text = "";
            }
            else
            {
                text.text = count.ToString();
            }
        }
    }

    int Check()
    {
        int count = 0;
        Vector2 myPoint = new Vector2(this.transform.position.x, this.transform.position.z);
        //Debug.Log(myPoint);
        for (int i = 0; i < aroundCells.Length; i++)
        {
            Vector2 point = myPoint + aroundCells[i];
            //if (mapS.MineSet(point))
            //{
            //    count++;
            //    //Debug.Log(count);
            //}
        }
        return count;
    }
}
