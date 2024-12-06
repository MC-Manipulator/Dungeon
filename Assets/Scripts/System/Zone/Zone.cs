using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zone : MonoBehaviour, Initializable
{
    [Header("区域尺寸")]
    public int size;

    [Header("区域编号")]
    public int zoneNo;

    [Header("区域中心")]
    public Vector2 zoneCenter;


    [Header("区域地图")]
    public Dictionary<int, GameObject> mapDic;

    [Header("区域实体")]
    public Dictionary<int, GameObject> entityDic;


    public List<GameObject> blanks;

    public GameObject map;

    public GameObject entity;

    public void Initialize()
    {
        blanks = new List<GameObject>();
        GenerateMap();
    }

    public void GenerateMap()
    {
        Debug.Log("区域" + zoneNo + "开始生成地图");
        bool ifMapExisted = ReadStoredMap();

        GameObject emptyBlock = DungeonManager.instance.GetBlockByName("EmptyBlock");
        GameObject wall = DungeonManager.instance.GetBlockByName("Wall");
        GameObject floor = DungeonManager.instance.GetFloorByName("Floor");
        GameObject core = DungeonManager.instance.GetBlockByName("DungeonCore");
        GameObject entrance = DungeonManager.instance.GetBlockByName("Entrance");
        //GameObject testFloor = Resources.Load<GameObject>("TestFloor");

        float width = wall.transform.localScale.x;

        zoneCenter = (Vector2)this.transform.position + new Vector2(width * 0.5f * size, width * 0.5f * size);

        if (ifMapExisted)
        {

        }
        else
        {
            for (int i = 0;i < size;i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Vector2 blockPosition = (Vector2)this.transform.position + new Vector2(width * j, width * i);
                    GameObject newEmptyBlcok = Instantiate(emptyBlock, blockPosition, new Quaternion(0, 0, 0, 0));
                    GameObject newBlcok = Instantiate(wall, blockPosition, new Quaternion(0, 0, 0, 0));
                    blanks.Add(newEmptyBlcok);
                    newEmptyBlcok.transform.parent = map.transform;
                    newBlcok.transform.parent = map.transform;
                }
            }

            GameObject newEntrance = Instantiate(entrance, new Vector2(zoneCenter.x, zoneCenter.y + width * 0.5f * size - 1), new Quaternion(0, 0, 0, 0));

            for (int i = 0;i < 2;i++)
            {
                for (int j = 0;j < 3;j++)
                {
                    Vector2 blockPosition = (Vector2)this.transform.position + new Vector2(newEntrance.transform.position.x + j - 1, newEntrance.transform.position.y + i - 1);
                    GameObject newFloor = Instantiate(floor, blockPosition, new Quaternion(0, 0, 0, 0));
                    newFloor.transform.parent = newEntrance.transform;
                }
            }
            newEntrance.transform.parent = map.transform;

            GameObject newCore = Instantiate(core, new Vector2(zoneCenter.x, zoneCenter.y - width * 0.5f * size + 2f), new Quaternion(0, 0, 0, 0));
            newCore.transform.parent = map.transform;
        }
    }

    public bool ReadStoredMap()
    {
        return false;
    }
}
