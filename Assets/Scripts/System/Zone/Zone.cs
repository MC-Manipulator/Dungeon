using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour, Initializable
{
    [Header("����ߴ�")]
    public int size;

    [Header("������")]
    public int zoneNo;

    [Header("�����ͼ")]
    public int map;

    [Header("����ʵ��")]
    public int entity;

    public void Initialize()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        Debug.Log("����" + zoneNo + "��ʼ���ɵ�ͼ");
    }

}
