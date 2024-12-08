using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
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
    public GameObject Border;

    [Header("召唤列表")]
    public List<GameObject> monsterList;

    [Header("操作栈")]
    public Stack<Operation> operationStack;

    [Header("当前持有的金币")]
    public int gold;

    public bool isBuilding;

    public bool isSummoning;

    public bool isBuildingFloor;
    public bool chosingStartPoint;
    public bool hasChosenStartPoint;

    public GameObject currBuildingObject;
    public GameObject endOfFloorSet;
    [Header("地板放置位置列表")]
    public Vector2 preEndOfFloorSetPosition;
    public List<GameObject> floorSetList;
    public List<GameObject> floorSetEmptyBlockList;
    public List<GameObject> prefloorSetEmptyBlockList;

    public GameObject currSummoningObject;


    public event Action StartPlacingEvent;
    public event Action EndPlacingEvent;

    [Header("冲突方块列表")]
    public List<GameObject> conflictBlockList;

    public void Initialize()
    {
        if (instance == null)
            instance = this;
        if (instance != null && instance != this)
            gameObject.SetActive(false);

        conflictBlockList = new List<GameObject>();
        PlayerInputManager.instance.CancelEvent += Cancel;
        PlayerInputManager.instance.PlaceEvent += Place;
        isBuildingFloor = false;
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

                Vector2 blockPosition = new Vector2(0, 0);

                foreach (Collider2D c in hit)
                {
                    if (c.gameObject.name == "EmptyBlock")
                    {
                        blockPosition = (Vector2)c.gameObject.transform.position;
                        //currBuildingObject.transform.position = c.gameObject.transform.position;
                        break;
                    }
                }


                if (isBuildingFloor)
                {
                    if (hasChosenStartPoint)
                    {
                        Vector3 direction = blockPosition - (Vector2)currBuildingObject.transform.position;

                        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                        {
                            blockPosition.y = currBuildingObject.transform.position.y;
                        }
                        else
                        {
                            blockPosition.x = currBuildingObject.transform.position.x;
                        }

                        endOfFloorSet.transform.position = blockPosition;
                        if (blockPosition != preEndOfFloorSetPosition)
                        {
                            preEndOfFloorSetPosition = blockPosition;
                            BuildRestOfFloor();
                        }
                    }
                    else
                    {
                        currBuildingObject.transform.position = blockPosition;
                    }
                }
                else
                {

                    currBuildingObject.transform.position = blockPosition;
                }

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

        if (name == "Floor")
        {
            currBuildingObject = BuildFloor();
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
        GameObject room = ReInstantiate.Instantiate(Room, new Vector2(0, 0));

        room.GetComponent<BoxCollider2D>().size = new Vector2(5f, 5f);

        for (int i = 0; i < 5; i++)
        {
            GameObject wall = ReInstantiate.Instantiate(Wall, new Vector2(0, 0));
            wall.transform.position = new Vector3(-2, i - 2, 0);
            wall.transform.SetParent(room.transform);

        }

        for (int i = 0; i < 5; i++)
        {
            GameObject wall = ReInstantiate.Instantiate(Wall, new Vector2(0, 0));
            wall.transform.position = new Vector3(2, i - 2, 0);
            wall.transform.SetParent(room.transform);

        }

        for (int i = 0; i < 3; i++)
        {
            GameObject wall = ReInstantiate.Instantiate(Wall, new Vector2(0, 0));
            wall.transform.position = new Vector3(i - 1, -2, 0);
            wall.transform.SetParent(room.transform);

        }

        for (int i = 0; i < 3; i++)
        {
            GameObject wall = ReInstantiate.Instantiate(Wall, new Vector2(0, 0));
            wall.transform.position = new Vector3(i - 1, 2, 0);
            wall.transform.SetParent(room.transform);

        }

        for (int i = 0;i < 3;i++)
        {
            for (int j = 0;j < 3;j++)
            {
                GameObject floor = ReInstantiate.Instantiate(Floor, new Vector2(0, 0));
                floor.transform.position = new Vector3(i - 1, j - 1, 0);
                floor.transform.SetParent(room.transform);
            }
        }

        return room;
    }

    public GameObject BuildFloor()
    {
        isBuildingFloor = true;
        chosingStartPoint = true;
        GameObject floorSet = new GameObject("FloorSet");
        GameObject floor = ReInstantiate.Instantiate(Floor, new Vector2(0, 0));
        floor.transform.SetParent(floorSet.transform);



        return floorSet;
    }

    public void BuildRestOfFloor()
    {
        Vector3 direction = endOfFloorSet.transform.position - currBuildingObject.transform.position;
        
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            direction.y = 0;
        }
        else
        {
            direction.x = 0;
        }

        RaycastHit2D[] hit = Physics2D.RaycastAll(currBuildingObject.transform.position, direction.normalized, direction.magnitude);
        /*
        if (prefloorSetEmptyBlockList == null)
        {
            prefloorSetEmptyBlockList = new List<GameObject>();
        }
        foreach (GameObject gb in floorSetEmptyBlockList)
        {
            prefloorSetEmptyBlockList.Add(gb);
        }*/

        floorSetEmptyBlockList.Clear();

        foreach (RaycastHit2D h in hit)
        {
            if (h.transform.gameObject.name == "EmptyBlock" && !floorSetEmptyBlockList.Contains(h.transform.gameObject))
            {
                if (h.transform.position == currBuildingObject.transform.position ||
                    h.transform.position == endOfFloorSet.transform.position)
                {
                    continue;
                }

                floorSetEmptyBlockList.Add(h.transform.gameObject);

            }
        }

        foreach (GameObject eb in floorSetEmptyBlockList)
        {
            bool exist = false;
            foreach (GameObject f in floorSetList)
            {
                if (f.transform.position == eb.transform.position)
                {
                    exist = true;
                }
            }
            if (!exist)
            {

                GameObject floor = ReInstantiate.Instantiate(Floor, eb.transform.position);
                floor.transform.SetParent(currBuildingObject.transform);
                floorSetList.Add(floor);
            }
        }

        List<GameObject> toBeRemoved = new List<GameObject>();

        foreach (GameObject f in floorSetList)
        {
            bool exist = false;
            foreach (GameObject eb in floorSetEmptyBlockList)
            {
                if (f.transform.position == eb.transform.position)
                {
                    exist = true;
                }
            }
            if (!exist)
            {
                toBeRemoved.Add(f);
            }
        }

        for (int i = 0;i < toBeRemoved.Count;i++)
        {
            floorSetList.Remove(toBeRemoved[i]);
            Destroy(toBeRemoved[i]);
        }
        toBeRemoved.Clear();
    }


    public void Summon(string name)
    {
        isSummoning = true;

    }

    public void Cancel()
    {
        EndPlacingEvent?.Invoke();


        conflictBlockList.Clear();

        if (!_preShowingMesh)
        {
            GameManager.instance.HideMesh();
        }

        if (isBuilding)
        {
            isBuilding = false;
            isBuildingFloor = false;
            chosingStartPoint = false;
            chosingStartPoint = false;
            hasChosenStartPoint = false;
            
            if (endOfFloorSet != null)
            {
                Destroy(endOfFloorSet);
            }
            Destroy(currBuildingObject);
            endOfFloorSet = null;
            currBuildingObject = null;

            foreach (GameObject gb in ZoneManager.instance.currentZone.GetComponent<Zone>().blanks)
            {
                gb.GetComponent<EmptyBlock>().ActiveTrigger();
            }
            foreach (GameObject gb in ZoneManager.instance.currentZone.GetComponent<Zone>().blanks)
            {
                gb.GetComponent<EmptyBlock>().RefreshBlock();
            }
        }

        if (isSummoning)
        {
            isSummoning = false;

        }
    }

    public void Place()
    {
        if (chosingStartPoint && !hasChosenStartPoint)
        {
            hasChosenStartPoint = true;
            Debug.Log("已经选择起点");
            endOfFloorSet = ReInstantiate.Instantiate(Floor, new Vector2(0, 0));
            endOfFloorSet.transform.SetParent(currBuildingObject.transform);
            return;


        }

        if (chosingStartPoint && hasChosenStartPoint)
        {
            isBuilding = false;
            foreach (GameObject gb in ZoneManager.instance.currentZone.GetComponent<Zone>().blanks)
            {
                gb.GetComponent<EmptyBlock>().ActiveTrigger();
            }
            foreach (GameObject gb in ZoneManager.instance.currentZone.GetComponent<Zone>().blanks)
            {
                gb.GetComponent<EmptyBlock>().RefreshBlock();
            }
            isBuilding = true;
            currBuildingObject = BuildFloor();
            currBuildingObject.transform.position = endOfFloorSet.transform.position;
            endOfFloorSet = ReInstantiate.Instantiate(Floor, new Vector2(0, 0));
            endOfFloorSet.transform.SetParent(currBuildingObject.transform);
            floorSetList.Clear();
            floorSetEmptyBlockList.Clear();
            return;
        }

        if (conflictBlockList.Count != 0)
        {
            Debug.Log("建筑物有冲突，不能放置");
            return;
        }

        EndPlacingEvent?.Invoke();

        if (!_preShowingMesh)
        {
            GameManager.instance.HideMesh();
        }

        if (isBuilding)
        {
            isBuilding = false;
            isBuildingFloor = false;
            chosingStartPoint = false;
            hasChosenStartPoint = false;

            currBuildingObject.transform.SetParent(ZoneManager.instance.currentZone.GetComponent<Zone>().map.transform);

            currBuildingObject = null;
            endOfFloorSet = null;

            foreach (GameObject gb in ZoneManager.instance.currentZone.GetComponent<Zone>().blanks)
            {
                gb.GetComponent<EmptyBlock>().ActiveTrigger();
            }
            foreach (GameObject gb in ZoneManager.instance.currentZone.GetComponent<Zone>().blanks)
            {
                gb.GetComponent<EmptyBlock>().RefreshBlock();
            }
        }

        if (isSummoning)
        {
            isSummoning = false;

        }
    }
}