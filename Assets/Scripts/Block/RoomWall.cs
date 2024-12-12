using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWall : AbstractBlock
{

    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    public Sprite upLeftSprite;
    public Sprite upRightSprite;
    public Sprite downLeftSprite;
    public Sprite downRightSprite;


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

            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Up] = "Floor"
                },
                upSprite
            );

            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Down] = "Floor"
                },
                downSprite
            );

            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Left] = "Floor"
                },
                leftSprite
            );

            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Right] = "Floor"
                },
                rightSprite
            );


            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Up] = "Floor",
                    [Nearby.Right] = "Floor"
                },
                upRightSprite
            );

            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Up] = "Floor",
                    [Nearby.Left] = "Floor"
                },
                upLeftSprite
            );

            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Down] = "Floor",
                    [Nearby.Left] = "Floor"
                },
                downLeftSprite
            );

            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Down] = "Floor",
                    [Nearby.Right] = "Floor"
                },
                downRightSprite
            );


            ResetBlock(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Up] = "Floor",
                    [Nearby.Down] = "Floor"
                },
                DungeonManager.instance.Floor
            );

            ResetBlock(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Left] = "Floor",
                    [Nearby.Right] = "Floor"
                },
                DungeonManager.instance.Floor
            );

            /*
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
            }*/
        }
    }
}
