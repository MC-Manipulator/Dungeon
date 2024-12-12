using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerRoomWall : AbstractBlock
{

    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    public Sprite upLeftSprite;
    public Sprite upRightSprite;
    public Sprite downLeftSprite;
    public Sprite downRightSprite;

    public Sprite upLeftFSprite;
    public Sprite upRightFSprite;
    public Sprite downLeftFSprite;
    public Sprite downRightFSprite;

    public Sprite blank;


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

            /*
            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Up] = "Wall",
                    [Nearby.Right] = "Floor",
                    [Nearby.Down] = "Wall"
                },
                leftSprite
            );
            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Up] = "Wall",
                    [Nearby.Left] = "Floor",
                    [Nearby.Down] = "Wall"
                },
                rightSprite
            );
            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Left] = "Wall",
                    [Nearby.Down] = "Floor",
                    [Nearby.Right] = "Wall"
                },
                upSprite
            );
            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Left] = "Wall",
                    [Nearby.Up] = "Floor",
                    [Nearby.Right] = "Wall"
                },
                downSprite
            );




            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Right] = "Wall",
                    [Nearby.Down] = "Wall",
                    [Nearby.DownRight] = "Floor"
                },
                upLeftSprite
            );
            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Left] = "Wall",
                    [Nearby.Down] = "Wall",
                    [Nearby.DownLeft] = "Floor"
                },
                upRightSprite
            );
            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Right] = "Wall",
                    [Nearby.Up] = "Wall",
                    [Nearby.UpRight] = "Floor"
                },
                downLeftSprite
            );
            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Left] = "Wall",
                    [Nearby.Up] = "Wall",
                    [Nearby.UpLeft] = "Floor"
                },
                downRightSprite
            );




            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Down] = "Wall",
                    [Nearby.Right] = "Wall",
                    [Nearby.Up] = "Floor",
                    [Nearby.Left] = "Floor"
                },
                upLeftFSprite
            );
            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Down] = "Wall",
                    [Nearby.Left] = "Wall",
                    [Nearby.Up] = "Floor",
                    [Nearby.Right] = "Wall"
                },
                upRightFSprite
            );
            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Up] = "Wall",
                    [Nearby.Right] = "Wall",
                    [Nearby.Down] = "Floor",
                    [Nearby.Left] = "Floor"
                },
                downLeftFSprite
            );
            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Up] = "Wall",
                    [Nearby.Left] = "Wall",
                    [Nearby.Down] = "Floor",
                    [Nearby.Right] = "Floor"
                },
                downRightFSprite
            );




            SetSprite(
                new Dictionary<Nearby, string>
                {
                    [Nearby.Up] = "Wall",
                    [Nearby.Down] = "Wall",
                    [Nearby.Left] = "Wall",
                    [Nearby.Right] = "Wall"
                },
                blank
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
            );*/

            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Up].gameObject.tag == "Floor" &&
                    dic[Nearby.Down].gameObject.tag == "Floor" &&
                    dic[Nearby.Left].gameObject.tag == "Floor" &&
                    dic[Nearby.Right].gameObject.tag == "Wall")
                {
                    GameObject floor = ReInstantiate.Instantiate(DungeonManager.instance.Floor, transform.position);
                    floor.transform.SetParent(ZoneManager.instance.currentZone.GetComponent<Zone>().map.transform);
                    Destroy(gameObject);
                    return;
                }
            }
            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Up].gameObject.tag == "Floor" &&
                    dic[Nearby.Down].gameObject.tag == "Floor" &&
                    dic[Nearby.Left].gameObject.tag == "Wall" &&
                    dic[Nearby.Right].gameObject.tag == "Floor")
                {
                    GameObject floor = ReInstantiate.Instantiate(DungeonManager.instance.Floor, transform.position);
                    floor.transform.SetParent(ZoneManager.instance.currentZone.GetComponent<Zone>().map.transform);
                    Destroy(gameObject);
                    return;
                }
            }
            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Up].gameObject.tag == "Floor" &&
                    dic[Nearby.Down].gameObject.tag == "Wall" &&
                    dic[Nearby.Left].gameObject.tag == "Floor" &&
                    dic[Nearby.Right].gameObject.tag == "Floor")
                {
                    GameObject floor = ReInstantiate.Instantiate(DungeonManager.instance.Floor, transform.position);
                    floor.transform.SetParent(ZoneManager.instance.currentZone.GetComponent<Zone>().map.transform);
                    Destroy(gameObject);
                    return;
                }
            }
            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Up].gameObject.tag == "Wall" &&
                    dic[Nearby.Down].gameObject.tag == "Floor" &&
                    dic[Nearby.Left].gameObject.tag == "Floor" &&
                    dic[Nearby.Right].gameObject.tag == "Floor")
                {
                    GameObject floor = ReInstantiate.Instantiate(DungeonManager.instance.Floor, transform.position);
                    floor.transform.SetParent(ZoneManager.instance.currentZone.GetComponent<Zone>().map.transform);
                    Destroy(gameObject);
                    return;
                }
            }



            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Up].gameObject.tag == "Floor" &&
                    dic[Nearby.Down].gameObject.tag == "Floor" &&
                    dic[Nearby.Left].gameObject.tag == "Wall" &&
                    dic[Nearby.Right].gameObject.tag == "Wall")
                {
                    GameObject floor = ReInstantiate.Instantiate(DungeonManager.instance.Floor, transform.position);
                    floor.transform.SetParent(ZoneManager.instance.currentZone.GetComponent<Zone>().map.transform);
                    Destroy(gameObject);
                    return;
                }
            }
            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Up].gameObject.tag == "Wall" &&
                    dic[Nearby.Down].gameObject.tag == "Wall" &&
                    dic[Nearby.Left].gameObject.tag == "Floor" &&
                    dic[Nearby.Right].gameObject.tag == "Floor")
                {
                    GameObject floor = ReInstantiate.Instantiate(DungeonManager.instance.Floor, transform.position);
                    floor.transform.SetParent(ZoneManager.instance.currentZone.GetComponent<Zone>().map.transform);
                    Destroy(gameObject);
                    return;
                }
            }

            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Right))
            {


                if (dic[Nearby.Up].gameObject.tag == "Wall" &&
                    dic[Nearby.Right].gameObject.tag == "Floor" &&
                    dic[Nearby.Down].gameObject.tag == "Wall")
                {
                    sr.sprite = leftSprite;
                    return;
                }
            }

            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left))
            {
                if (dic[Nearby.Up].gameObject.tag == "Wall" &&
                    dic[Nearby.Left].gameObject.tag == "Floor" &&
                    dic[Nearby.Down].gameObject.tag == "Wall")
                {
                    sr.sprite = rightSprite;
                    return;
                }
            }
            
            if (dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Left].gameObject.tag == "Wall" &&
                    dic[Nearby.Down].gameObject.tag == "Floor" &&
                    dic[Nearby.Right].gameObject.tag == "Wall")
                {
                    sr.sprite = upSprite;
                    return;
                }
            }

            if (dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Left].gameObject.tag == "Wall" &&
                    dic[Nearby.Up].gameObject.tag == "Floor" &&
                    dic[Nearby.Right].gameObject.tag == "Wall")
                {
                    sr.sprite = downSprite;
                    return;
                }
            }

            
            if (dic.ContainsKey(Nearby.Right) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.DownRight))
            {
                if (dic[Nearby.Right].gameObject.tag == "Wall" &&
                    dic[Nearby.Down].gameObject.tag == "Wall" &&
                    dic[Nearby.DownRight].gameObject.tag == "Floor")
                {
                    sr.sprite = upLeftSprite;
                    return;
                }
            }
            if (dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.DownLeft))
            {
                if (dic[Nearby.Left].gameObject.tag == "Wall" &&
                    dic[Nearby.Down].gameObject.tag == "Wall" &&
                    dic[Nearby.DownLeft].gameObject.tag == "Floor")
                {
                    sr.sprite = upRightSprite;
                    return;
                }
            }
            if (dic.ContainsKey(Nearby.Right) && dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.UpRight))
            {
                if (dic[Nearby.Right].gameObject.tag == "Wall" &&
                    dic[Nearby.Up].gameObject.tag == "Wall" &&
                    dic[Nearby.UpRight].gameObject.tag == "Floor")
                {
                    sr.sprite = downLeftSprite;
                    return;
                }
            }
            if (dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.UpLeft))
            {
                if (dic[Nearby.Left].gameObject.tag == "Wall" &&
                    dic[Nearby.Up].gameObject.tag == "Wall" &&
                    dic[Nearby.UpLeft].gameObject.tag == "Floor")
                {
                    sr.sprite = downRightSprite;
                    return;
                }
            }

            
            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Down].gameObject.tag == "Wall" &&
                    dic[Nearby.Right].gameObject.tag == "Wall" &&
                    dic[Nearby.Up].gameObject.tag == "Floor" &&
                    dic[Nearby.Left].gameObject.tag == "Floor")
                {
                    sr.sprite = upLeftFSprite;
                    return;
                }
            }

            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Down].gameObject.tag == "Wall" &&
                    dic[Nearby.Left].gameObject.tag == "Wall" &&
                    dic[Nearby.Up].gameObject.tag == "Floor" &&
                    dic[Nearby.Right].gameObject.tag == "Floor")
                {
                    sr.sprite = upRightFSprite;
                    return;
                }
            }

            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Up].gameObject.tag == "Wall" &&
                    dic[Nearby.Right].gameObject.tag == "Wall" &&
                    dic[Nearby.Down].gameObject.tag == "Floor" &&
                    dic[Nearby.Left].gameObject.tag == "Floor")
                {
                    sr.sprite = downLeftFSprite;
                    return;
                }
            }

            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Up].gameObject.tag == "Wall" &&
                    dic[Nearby.Left].gameObject.tag == "Wall" &&
                    dic[Nearby.Down].gameObject.tag == "Floor" &&
                    dic[Nearby.Right].gameObject.tag == "Floor")
                {
                    sr.sprite = downRightFSprite;
                    return;
                }
            }

            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Up].gameObject.tag == "Wall" &&
                    dic[Nearby.Down].gameObject.tag == "Wall" &&
                    dic[Nearby.Left].gameObject.tag == "Wall" &&
                    dic[Nearby.Right].gameObject.tag == "Wall")
                {
                    sr.sprite = blank;
                    return;
                }
            }


            if (dic.ContainsKey(Nearby.Up) && dic.ContainsKey(Nearby.Down) && dic.ContainsKey(Nearby.Left) && dic.ContainsKey(Nearby.Right))
            {
                if (dic[Nearby.Up].gameObject.tag == "Floor" &&
                    dic[Nearby.Down].gameObject.tag == "Floor" &&
                    dic[Nearby.Left].gameObject.tag == "Floor" &&
                    dic[Nearby.Right].gameObject.tag == "Floor")
                {
                    GameObject floor = ReInstantiate.Instantiate(DungeonManager.instance.Floor, transform.position);
                    floor.transform.SetParent(ZoneManager.instance.currentZone.GetComponent<Zone>().map.transform);
                    Destroy(gameObject);
                    return;
                }
            }


        }
    }
}
