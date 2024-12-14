using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour, Initializable
{
    public static CameraManager instance;

    public float moveSpeed = 0.5f; // 缩放速度

    public float zoomSpeed = 0.5f; // 缩放速度
    public float currentZoom; // 当前缩放
    public float minZoom = 1.0f; // 最小缩放值
    public float maxZoom = 5.0f; // 最大缩放值

    public float scrollInput;

    public GameObject cameraObject;


    public void Initialize()
    {
        if (instance == null)
            instance = this;
        if (instance != null && instance != this)
            gameObject.SetActive(false);

        currentZoom = Camera.main.orthographicSize - scrollInput * zoomSpeed;

        PlayerInputManager.instance.ZoomEvent += Zoom;
        PlayerInputManager.instance.MoveEvent += Move;
    }

    private void Move()
    {
        Vector3 moveDirection = PlayerInputManager.instance.inputDirection;

        if ((cameraObject.GetComponent<CameraTrigger>().reachUpBorder && moveDirection.y > 0) ||
            (cameraObject.GetComponent<CameraTrigger>().reachDownBorder && moveDirection.y < 0))
        {
            moveDirection.y = 0;
        }

        if ((cameraObject.GetComponent<CameraTrigger>().reachLeftBorder && moveDirection.x < 0) || 
            (cameraObject.GetComponent<CameraTrigger>().reachRightBorder && moveDirection.x > 0))
        {
            moveDirection.x = 0;
        }

        cameraObject.transform.position += moveDirection * moveSpeed * currentZoom / maxZoom;
    }

    public void DirectMove(Vector2 position)
    {
        Vector3 trans = new Vector3(position.x, position.y, -10);
        cameraObject.transform.position = trans;
    }

    //AI生成
    private void Zoom()
    {
        // 根据滚轮滚动值调整相机距离
        scrollInput = PlayerInputManager.instance.scrollInput;

        float newZoom = Camera.main.orthographicSize - scrollInput * zoomSpeed;
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom); // 限制在最小/最大范围内
        Camera.main.orthographicSize = newZoom;


        cameraObject.GetComponent<BoxCollider2D>().size = new Vector2(newZoom * 16 / 4.5f, newZoom * 2);
        currentZoom = newZoom;
    }
}
