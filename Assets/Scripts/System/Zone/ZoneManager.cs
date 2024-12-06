using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZoneManager : MonoBehaviour, Initializable
{
    public static ZoneManager instance;

    [Header("地牢")]
    public GameObject dungeon;

    [Header("区域列表")]
    public List<Zone> zoneList;

    [Header("当前区域")]
    public GameObject currentZone;

    [Header("当前区域序号")]
    public int zoneNo;

    [Header("区域数量")]
    public int zoneCount;


    public void Initialize()
    {
        if (instance == null)
            instance = this;
        if (instance != null && instance != this)
            gameObject.SetActive(false);

        dungeon = GameObject.Find("Dungeon");
        if (dungeon == null)
            dungeon = new GameObject("Dungeon");

        zoneCount = 0;
        AddZone();
    }

    public void AddZone()
    {
        zoneCount += 1;

        GameObject newZoneObject = new GameObject("Zone" + zoneCount);
        newZoneObject.AddComponent<Zone>();
        Zone newZone = newZoneObject.GetComponent<Zone>();
        newZoneObject.transform.SetParent(dungeon.transform);
        newZone.zoneNo = zoneCount;

        GameObject map = new GameObject("Map");
        GameObject entity = new GameObject("Entity");

        map.transform.SetParent(newZoneObject.transform);
        entity.transform.SetParent(newZoneObject.transform);

        newZone.map = map;
        newZone.entity = entity;

        newZone.size = 20;

        newZone.Initialize();

        zoneList.Add(newZone);

        currentZone = newZoneObject;
    }

    public void SwitchToLastZone()
    {
        if (zoneNo == 1)
            return;

        zoneNo--;
        currentZone = zoneList[zoneNo].gameObject;
    }

    public void SwitchToNextZone()
    {
        if (zoneNo == zoneCount)
            return;

        zoneNo++;
        currentZone = zoneList[zoneNo].gameObject;
    }
}
