using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZoneManager : MonoBehaviour, Initializable
{
    [Header("����")]
    public GameObject dungeon;

    [Header("�����б�")]
    public List<Zone> zoneList;

    [Header("��ǰ����")]
    public GameObject currentZone;

    [Header("��ǰ�������")]
    public int zoneNo;

    [Header("��������")]
    public int zoneCount;


    public void Initialize()
    {
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

        zoneList.Add(newZone);
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
