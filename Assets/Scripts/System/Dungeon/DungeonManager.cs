using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour, Initializable
{
    public static DungeonManager instance;

    [Header("方块列表")]
    public List<GameObject> blockList;

    [Header("地板列表")]
    public List<GameObject> floorList;

    [Header("建筑列表")]
    public List<GameObject> buildingList;

    public GameObject Room;
    public GameObject Wall;
    public GameObject Floor;

    [Header("召唤列表")]
    public List<GameObject> monsterList;

    [Header("操作栈")]
    public Stack<Operation> operationStack;

    [Header("当前持有的金币")]
    public int gold;

    public bool isBuilding;

    public bool isSummoning;

    public GameObject currBuildingObject;
    public GameObject currSummoningObject;

    public event Action StartPlacingEvent;
    public event Action EndPlacingEvent;

    public void Initialize()
    {
        if (instance == null)
            instance = this;
        if (instance != null && instance != this)
            gameObject.SetActive(false);

        PlayerInputManager.instance.CancelEvent += Cancel;
        PlayerInputManager.instance.PlaceEvent += Place;
        isBuilding = false;
        isSummoning = false;
    }

    private void Update()
    {
        if (!GameManager.instance.isPaused)
        {
            if (currBuildingObject != null)
            {
                Vector3 mouseScreenPosition = Input.mousePosition;

                // 获取当前相机
                Camera camera = Camera.main;

                // 将鼠标屏幕位置转换为世界位置
                Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, camera.nearClipPlane));

                float radius = 0.1f;
                // 检测鼠标位置附近的碰撞体
                Collider2D[] hit = Physics2D.OverlapCircleAll(mouseWorldPosition, radius);
                foreach (Collider2D c in hit)
                {
                    if (c.gameObject.name.Contains("EmptyBlock"))
                    {
                        currBuildingObject.transform.position = c.gameObject.transform.position;
                        return;
                    }
                }

                currBuildingObject.transform.position = mouseWorldPosition;

            }
        }

    }

    public GameObject GetBlockByName(string name)
    {
        GameObject targetBlock = null;

        foreach (GameObject gb in blockList)
        {
            if (gb.name == name)
            {
                targetBlock = gb;
                break;
            }
        }

        return targetBlock;
    }

    public GameObject GetFloorByName(string name)
    {
        GameObject targetFloor = null;

        foreach (GameObject gb in floorList)
        {
            if (gb.name == name)
            {
                targetFloor = gb;
                break;
            }
        }

        return targetFloor;
    }

    private bool _preShowingMesh;

    public void Build(string name)
    {
        isBuilding = true;

        StartPlacingEvent?.Invoke();

        _preShowingMesh = GameManager.instance.isShowingMesh;

        if (!_preShowingMesh)
        {
            GameManager.instance.ShowMesh();
        }
        

        if (name == "Room")
        {
            currBuildingObject = BuildRoom();
            return;
        }

        foreach (GameObject gb in buildingList)
        {
            if (gb.name == name)
            {
                currBuildingObject = Instantiate(gb);
            }
        }
    }

    public GameObject BuildRoom()
    {
        GameObject room = Instantiate(Room);

        room.GetComponent<BoxCollider2D>().size = new Vector2(5f, 5f);

        for (int i = 0; i < 5; i++)
        {
            GameObject wall = Instantiate(Wall);
            wall.transform.position = new Vector3(-2, i - 2, 0);
            wall.transform.SetParent(room.transform);

        }

        for (int i = 0; i < 5; i++)
        {
            GameObject wall = Instantiate(Wall);
            wall.transform.position = new Vector3(2, i - 2, 0);
            wall.transform.SetParent(room.transform);

        }

        for (int i = 0; i < 5; i++)
        {
            GameObject wall = Instantiate(Wall);
            wall.transform.position = new Vector3(i - 2, -2, 0);
            wall.transform.SetParent(room.transform);

        }

        for (int i = 0; i < 5; i++)
        {
            GameObject wall = Instantiate(Wall);
            wall.transform.position = new Vector3(i - 2, 2, 0);
            wall.transform.SetParent(room.transform);

        }

        for (int i = 0;i < 3;i++)
        {
            for (int j = 0;j < 3;j++)
            {
                GameObject floor = Instantiate(Floor);
                floor.transform.position = new Vector3(i - 1, j - 1, 0);
                floor.transform.SetParent(room.transform);
            }
        }

        return room;
    }


    public void Summon(string name)
    {
        isSummoning = true;

    }

    public void Cancel()
    {
        EndPlacingEvent?.Invoke();

        if (!_preShowingMesh)
        {
            GameManager.instance.HideMesh();
        }

        if (isBuilding)
        {
            isBuilding = false;
            Destroy(currBuildingObject);
        }

        if (isSummoning)
        {
            isSummoning = false;

        }
    }

    public void Place()
    {
        EndPlacingEvent?.Invoke();

        if (!_preShowingMesh)
        {
            GameManager.instance.HideMesh();
        }

        if (isBuilding)
        {
            isBuilding = false;

            currBuildingObject.transform.SetParent(ZoneManager.instance.currentZone.GetComponent<Zone>().map.transform);

            currBuildingObject = null;

            foreach (GameObject gb in ZoneManager.instance.currentZone.GetComponent<Zone>().blanks)
            {
                gb.GetComponent<EmptyBlock>().Refresh();
            }
            
        }

        if (isSummoning)
        {
            isSummoning = false;

        }
    }
}