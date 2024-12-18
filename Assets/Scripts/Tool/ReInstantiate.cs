using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReInstantiate
{
    public static GameObject Instantiate(GameObject prefab, Vector2 position)
    {
        GameObject gb = null;

        gb = GameObject.Instantiate(prefab, position, new Quaternion(0, 0, 0, 0));
        gb.name = gb.name.Replace("(Clone)", "");

        return gb;
    }
}
