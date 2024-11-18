using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour, Initializable
{
    [Header("区域尺寸")]
    public int size;

    [Header("区域编号")]
    public int zoneNo;

    [Header("区域地图")]
    public int map;

    [Header("区域实体")]
    public int entity;

    public void Initialize()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        Debug.Log("区域" + zoneNo + "开始生成地图");
    }

}
