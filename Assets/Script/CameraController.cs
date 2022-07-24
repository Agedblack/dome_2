using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float horizontalspeed = 30.0f;
    public float verticalSpeed=20.0f;
    public Animator anim;

    private GameObject Player;
    private GameObject PlayCameras;
    private float tempEulerX;

    private void Awake()
    {
        PlayCameras = transform.parent.gameObject;
        Player = PlayCameras.transform.parent.gameObject;
        Cursor.visible = false;//隐藏指针
        Cursor.lockState = CursorLockMode.Locked; //将光标锁定到游戏窗口的中心
    }
    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        Vector3 tempModelEuler = anim.transform.eulerAngles;

        Player.transform.Rotate(Vector3.up, mouseX * horizontalspeed * Time.deltaTime);
        //用欧拉角限制y轴旋转（也可用四元数）
        tempEulerX -= mouseY * verticalSpeed * Time.deltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -15, 25);
        PlayCameras.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
        anim.transform.eulerAngles = tempModelEuler;
    }
}
