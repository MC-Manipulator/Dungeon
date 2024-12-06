using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour, Initializable
{
    public static DungeonManager instance;

    [Header("�����б�")]
    public List<GameObject> blockList;

    [Header("�ذ��б�")]
    public List<GameObject> floorList;

    [Header("�����б�")]
    public List<GameObject> buildingList;

    public GameObject Room;
    public GameObject Wall;
    public GameObject Floor;

    [Header("�ٻ��б�")]
    public List<GameObject> monsterList;

    [Header("����ջ")]
    public Stack<Operation> operationStack;

    [Header("��ǰ���еĽ��")]
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

                // ��ȡ��ǰ���
                Camera camera = Camera.main;

                // �������Ļλ��ת��Ϊ����λ��
                Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, camera.nearClipPlane));

                float radius = 0.1f;
                // ������λ�ø�������ײ��
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