using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 用于保存从 JSON 加载的武器数据结构
[System.Serializable]
public class Weapon
{
    public string weaponName;
    public int damage;
    public int cost;
    public string description;
}

public class WeaponDataLoader : MonoBehaviour
{
    public List<Weapon> weaponList; // 存储加载后的武器数据

    void Start()
    {
        LoadWeaponData();
    }

    void LoadWeaponData()
    {
        // 获取 JSON 文件路径
        string filePath = Path.Combine(Application.streamingAssetsPath, "weapons.json");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            // 从 JSON 中解析武器数据
            weaponList = JsonUtility.FromJson<WeaponListWrapper>(jsonData).weapons;
            Debug.Log("Weapon data loaded successfully!");
        }
        else
        {
            Debug.LogError($"Weapon data file not found at: {filePath}");
        }
    }

    [System.Serializable]
    private class WeaponListWrapper
    {
        public List<Weapon> weapons; // 包装 JSON 数据的根对象
    }
}
