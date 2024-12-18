using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBlock : MonoBehaviour
{
    public SpriteRenderer sr;

    public abstract void Refresh();

    public bool ComfirmBlock(Dictionary<Nearby, string> targetDic)
    {
        Dictionary<Nearby, GameObject> dic = GetNearbyBlock.GetAll(gameObject);
        bool meet = true;
        foreach (Nearby p in targetDic.Keys)
        {
            if (dic.ContainsKey(p))
            {
                if (dic[p].tag != targetDic[p])
                {
                    meet = false;
                    break;
                }
            }
            else
            {
                meet = false;
                break;
            }
        }


        return meet;
    }

    public void SetSprite(Dictionary<Nearby, string> targetDic, Sprite sprite)
    {
        Dictionary<Nearby, GameObject> dic = GetNearbyBlock.GetAll(gameObject);
        bool meet = true;
        foreach (Nearby p in targetDic.Keys)
        {
            if (dic.ContainsKey(p))
            {
                if (dic[p].tag != targetDic[p])
                {
                    meet = false;
                    break;
                }
            }
            else
            {
                meet = false;
                break;
            }
        }

        if (meet)
        {
            sr.sprite = sprite;
        }
    }

    public void ResetBlock(Dictionary<Nearby, string> targetDic, GameObject gb)
    {
        Dictionary<Nearby, GameObject> dic = GetNearbyBlock.GetAll(gameObject);
        bool meet = true;
        foreach (Nearby p in targetDic.Keys)
        {
            if (dic.ContainsKey(p))
            {
                if (dic[p].tag != targetDic[p])
                {
                    meet = false;
                    break;
                }
            }
            else
            {
                meet = false;
                break;
            }
        }

        if (meet)
        {
            GameObject floor = ReInstantiate.Instantiate(gb, transform.position);
            floor.transform.SetParent(ZoneManager.instance.currentZone.GetComponent<Zone>().map.transform);
            Destroy(gameObject);
            /*
            GameObject floor = ReInstantiate.Instantiate(DungeonManager.instance.Floor, transform.position);
            floor.transform.SetParent(ZoneManager.instance.currentZone.GetComponent<Zone>().map.transform);
            Destroy(gameObject);*/
        }
    }
}
