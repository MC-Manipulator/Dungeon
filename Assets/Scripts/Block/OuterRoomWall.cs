using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterRoomWall : AbstractBlock
{

    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    public Sprite upLeftSprite;
    public Sprite upRightSprite;
    public Sprite downLeftSprite;
    public Sprite downRightSprite;

    public SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

    }

    public bool initialize = false;

    void Update()
    {

        if (!initialize)
        {
            initialize = true;
            Refresh();
        }
    }

    public override void Refresh()
    {
        if (!DungeonManager.instance.isBuilding)
        {
            Dictionary<Nearby, GameObject> dic = GetNearbyBlock.GetAll(gameObject);
            if (dic.ContainsKey(Nearby.Up))
            {
                if (dic[Nearby.Up].gameObject.tag == "Floor")
                {
                    sr.sprite = upSprite;
                }
            }

            if (dic.ContainsKey(Nearby.Down))
            {
                if (dic[Nearby.Down].gameObject.tag == "Floor")
                {
                    sr.sprite = downSprite;

                }
            }

            if (dic.ContainsKey(Nearby.Left))
            {
                if (dic[Nearby.Left].gameObject.tag == "Floor")
                {
                    sr.sprite = leftSprite;

                }
            }

            if (dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Right].gameObject.tag == "Floor")
                {
                    sr.sprite = rightSprite;

                }
            }


            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Left))
            {
                if (dic[Nearby.Up].gameObject.tag == "Floor" &&
                    dic[Nearby.Left].gameObject.tag == "Floor")
                {

                    sr.sprite = upLeftSprite;
                }
            }
            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Up].gameObject.tag == "Floor" &&
                    dic[Nearby.Right].gameObject.tag == "Floor")
                {

                    sr.sprite = upRightSprite;
                }
            }
            if (dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left))
            {
                if (dic[Nearby.Down].gameObject.tag == "Floor" &&
                    dic[Nearby.Left].gameObject.tag == "Floor")
                {
                    sr.sprite = downLeftSprite;

                }
            }
            if (dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Down].gameObject.tag == "Floor" &&
                    dic[Nearby.Right].gameObject.tag == "Floor")
                {
                    sr.sprite = downRightSprite;

                }
            }
        }
    }
}
