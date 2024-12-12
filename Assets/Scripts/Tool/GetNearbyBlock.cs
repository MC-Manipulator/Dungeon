using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Nearby
{
    Up,
    Down,
    Left,
    Right,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}



public class GetNearbyBlock
{
    public static string GetTypeInPos(Vector2 position)
    {
        string blockType = "";
        float radius = 0.1f;
        Collider2D[] blocks = Physics2D.OverlapCircleAll(position, radius);

        foreach (Collider2D c in blocks)
        {
            if (c.gameObject.tag == "Wall" || c.gameObject.tag == "Floor")
            {
                blockType = c.gameObject.tag;
            }
        }

        return blockType;
    }

    public static GameObject GetBlockInPos(Vector2 position)
    {
        GameObject block = null;
        float radius = 0.1f;
        Collider2D[] blocks = Physics2D.OverlapCircleAll(position, radius);

        foreach (Collider2D c in blocks)
        {
            if (c.gameObject.tag == "Wall" || c.gameObject.tag == "Floor")
            {
                block = c.gameObject;
            }
        }

        return block;
    }

    public static Dictionary<Nearby, GameObject> GetAll(GameObject gb)
    {
        Dictionary<Nearby, GameObject> dic = new Dictionary<Nearby, GameObject>();

        int width = 1;

        float radius = 0.1f;
        Collider2D[] up = Physics2D.OverlapCircleAll(gb.transform.position + new Vector3(0, width, 0), radius);
        Collider2D[] down = Physics2D.OverlapCircleAll(gb.transform.position + new Vector3(0, -width, 0), radius);
        Collider2D[] left = Physics2D.OverlapCircleAll(gb.transform.position + new Vector3(-width, 0, 0), radius);
        Collider2D[] right = Physics2D.OverlapCircleAll(gb.transform.position + new Vector3(width, 0, 0), radius);

        Collider2D[] upLeft = Physics2D.OverlapCircleAll(gb.transform.position + new Vector3(-width, width, 0), radius);
        Collider2D[] upRight = Physics2D.OverlapCircleAll(gb.transform.position + new Vector3(width, width, 0), radius);
        Collider2D[] downLeft = Physics2D.OverlapCircleAll(gb.transform.position + new Vector3(-width, -width, 0), radius);
        Collider2D[] downRight = Physics2D.OverlapCircleAll(gb.transform.position + new Vector3(width, -width, 0), radius);

        foreach (Collider2D c in up)
        {
            if (c.gameObject.tag == "Wall" || c.gameObject.tag == "Floor")
            {
                dic.TryAdd(Nearby.Up, c.gameObject);
            }
        }
        foreach (Collider2D c in down)
        {
            if (c.gameObject.tag == "Wall" || c.gameObject.tag == "Floor")
            {
                dic.TryAdd(Nearby.Down, c.gameObject);
            }
        }
        foreach (Collider2D c in left)
        {
            if (c.gameObject.tag == "Wall" || c.gameObject.tag == "Floor")
            {
                dic.TryAdd(Nearby.Left, c.gameObject);
            }
        }
        foreach (Collider2D c in right)
        {
            if (c.gameObject.tag == "Wall" || c.gameObject.tag == "Floor")
            {
                dic.TryAdd(Nearby.Right, c.gameObject);
            }
        }

        foreach (Collider2D c in upLeft)
        {
            if (c.gameObject.tag == "Wall" || c.gameObject.tag == "Floor")
            {
                dic.TryAdd(Nearby.UpLeft, c.gameObject);
            }
        }
        foreach (Collider2D c in upRight)
        {
            if (c.gameObject.tag == "Wall" || c.gameObject.tag == "Floor")
            {
                
                dic.TryAdd(Nearby.UpRight, c.gameObject);
            }
        }
        foreach (Collider2D c in downLeft)
        {
            if (c.gameObject.tag == "Wall" || c.gameObject.tag == "Floor")
            {
                dic.TryAdd(Nearby.DownLeft, c.gameObject);
            }
        }
        foreach (Collider2D c in downRight)
        {
            if (c.gameObject.tag == "Wall" || c.gameObject.tag == "Floor")
            {
                dic.TryAdd(Nearby.DownRight, c.gameObject);
            }
        }
        return dic;
    }
}
