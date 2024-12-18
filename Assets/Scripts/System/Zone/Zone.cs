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
        GameObject border = DungeonManager.instance.GetBlockByName("Border");
        GameObject cantBuildPlace = DungeonManager.instance.GetBlockByName("CantBuildPlace");
        GameObject cameraBorder = DungeonManager.instance.GetBlockByName("CameraBorder");

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
                    //GameObject newEmptyBlcok = Instantiate(emptyBlock, blockPosition, new Quaternion(0, 0, 0, 0));
                    //GameObject newBlcok = Instantiate(wall, blockPosition, new Quaternion(0, 0, 0, 0));

                    GameObject newEmptyBlcok = ReInstantiate.Instantiate(emptyBlock, blockPosition);
                    GameObject newBlcok = ReInstantiate.Instantiate(wall, blockPosition);
                    blanks.Add(newEmptyBlcok);
                    newEmptyBlcok.transform.parent = map.transform;
                    newBlcok.transform.parent = map.transform;
                }
            }

            //GameObject newEntrance = Instantiate(entrance, new Vector2(zoneCenter.x, zoneCenter.y + width * 0.5f * size - 1), new Quaternion(0, 0, 0, 0));
            GameObject newEntrance = ReInstantiate.Instantiate(entrance, new Vector2(zoneCenter.x, zoneCenter.y + width * 0.5f * size - 1));

            for (int i = 0;i < size + 3;i++)
            {
                Vector2 wallPosition = (Vector2)this.transform.position + new Vector2(newEntrance.transform.position.x - 2, newEntrance.transform.position.y + i - 1);
                GameObject newWall = ReInstantiate.Instantiate(wall, wallPosition);
                newWall.transform.parent = newEntrance.transform;

                for (int j = 0;j < 3;j++)
                {
                    Vector2 blockPosition = (Vector2)this.transform.position + new Vector2(newEntrance.transform.position.x + j - 1, newEntrance.transform.position.y + i - 1);
                    //GameObject newFloor = Instantiate(floor, blockPosition, new Quaternion(0, 0, 0, 0));
                    GameObject newFloor = ReInstantiate.Instantiate(floor, blockPosition);
                    newFloor.transform.parent = newEntrance.transform;
                }

                wallPosition = (Vector2)this.transform.position + new Vector2(newEntrance.transform.position.x + 2, newEntrance.transform.position.y + i - 1);
                newWall = ReInstantiate.Instantiate(wall, wallPosition);
                newWall.transform.parent = newEntrance.transform;
            }
            newEntrance.transform.parent = map.transform;

            //GameObject newCore = Instantiate(core, new Vector2(zoneCenter.x, zoneCenter.y - width * 0.5f * size + 2f), new Quaternion(0, 0, 0, 0));
            GameObject newCore = ReInstantiate.Instantiate(core, new Vector2(zoneCenter.x, zoneCenter.y - width * 0.5f * size + 2f));
            newCore.transform.parent = map.transform;

            GameObject newUpBorder = ReInstantiate.Instantiate(border, new Vector2(zoneCenter.x - width * 0.5f, zoneCenter.y + size * 0.5f));
            newUpBorder.GetComponent<BoxCollider2D>().size = new Vector2(size, 0.9f);
            newUpBorder.transform.parent = map.transform;

            GameObject newCameraUpBorder = ReInstantiate.Instantiate(cameraBorder, new Vector2(zoneCenter.x - width * 0.5f, zoneCenter.y + size * 1.5f));
            newCameraUpBorder.GetComponent<BoxCollider2D>().size = new Vector2(size * 3, 0.9f);
            newCameraUpBorder.gameObject.tag = "CameraUpBorder";
            newCameraUpBorder.transform.parent = map.transform;

            GameObject newDownBorder = ReInstantiate.Instantiate(border, new Vector2(zoneCenter.x - width * 0.5f, zoneCenter.y - size * 0.5f - width));
            newDownBorder.GetComponent<BoxCollider2D>().size = new Vector2(size, 0.9f);
            newDownBorder.transform.parent = map.transform;

            GameObject newCameraDownBorder = ReInstantiate.Instantiate(cameraBorder, new Vector2(zoneCenter.x - width * 0.5f, zoneCenter.y - size * 1.5f - width));
            newCameraDownBorder.GetComponent<BoxCollider2D>().size = new Vector2(size * 3, 0.9f);
            newCameraDownBorder.gameObject.tag = "CameraDownBorder";
            newCameraDownBorder.transform.parent = map.transform;

            GameObject newRightBorder = ReInstantiate.Instantiate(border, new Vector2(zoneCenter.x + size * 0.5f, zoneCenter.y - width * 0.5f));
            newRightBorder.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, size);
            newRightBorder.transform.parent = map.transform;

            GameObject newCameraRightBorder = ReInstantiate.Instantiate(cameraBorder, new Vector2(zoneCenter.x + size * 1.5f, zoneCenter.y - width * 0.5f));
            newCameraRightBorder.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, size * 3);
            newCameraRightBorder.gameObject.tag = "CameraRightBorder";
            newCameraRightBorder.transform.parent = map.transform;

            GameObject newLeftBorder = ReInstantiate.Instantiate(border, new Vector2(zoneCenter.x - size * 0.5f - width, zoneCenter.y - width * 0.5f));
            newLeftBorder.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, size);
            newLeftBorder.transform.parent = map.transform;

            GameObject newCameraLeftBorder = ReInstantiate.Instantiate(cameraBorder, new Vector2(zoneCenter.x - size * 1.5f - width, zoneCenter.y - width * 0.5f));
            newCameraLeftBorder.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, size * 3);
            newCameraLeftBorder.gameObject.tag = "CameraLeftBorder";
            newCameraLeftBorder.transform.parent = map.transform;

            GameObject newCantBuildPlace = ReInstantiate.Instantiate(cantBuildPlace, new Vector2(zoneCenter.x - width * 0.5f, zoneCenter.y - width * 0.5f));
            newCantBuildPlace.transform.localScale = new Vector2(size * 3 + 3, size * 3 + 3);
            newCantBuildPlace.transform.parent = map.transform;
        }
    }

    public bool ReadStoredMap()
    {
        return false;
    }
}
