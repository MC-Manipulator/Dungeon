using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger: MonoBehaviour
{
    public bool reachUpBorder = false;
    public bool reachDownBorder = false;
    public bool reachLeftBorder = false;
    public bool reachRightBorder = false;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CameraUpBorder")
        {
            reachUpBorder = true;
        }
        else if (collision.gameObject.tag == "CameraDownBorder")
        {
            reachDownBorder = true;
        }
        else if (collision.gameObject.tag == "CameraLeftBorder")
        {
            reachLeftBorder = true;
        }
        else if (collision.gameObject.tag == "CameraRightBorder")
        {
            reachRightBorder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CameraUpBorder")
        {
            reachUpBorder = false;
        }
        else if (collision.gameObject.tag == "CameraDownBorder")
        {
            reachDownBorder = false;
        }
        else if (collision.gameObject.tag == "CameraLeftBorder")
        {
            reachLeftBorder = false;
        }
        else if (collision.gameObject.tag == "CameraRightBorder")
        {
            reachRightBorder = false;
        }
    }
}
