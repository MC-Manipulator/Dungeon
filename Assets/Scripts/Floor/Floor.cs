using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : AbstractFloor
{
    public List<Sprite> spriteList;
    public SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        if (spriteList.Count != 0)
        {
            sr.sprite = spriteList[Random.Range(0, spriteList.Count)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
