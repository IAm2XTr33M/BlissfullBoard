using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //changeable variables
    [SerializeField]
    private Vector2 lookSens = Vector2.one;
    [SerializeField]
    private bool rotateCamNotPlayer = false;

    //private variables
    private Camera playerCamera;
    private PlayerControlls playerControlls;
    private float xRotation = 0;

    private void Awake()
    {
        //set reference
        playerControlls = new PlayerControlls();
    }

    //turn on / off player input controlls
    private void OnEnable()
    {
        playerControlls.Enable();
    }
    private void OnDisable()
    {
        playerControlls.Disable();
    }

    private void Start()
    {
        //set camera 
        playerCamera = gameObject.GetComponentInChildren<Camera>();

        //turn corsor off
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //get mouse input
        float mouseX = playerControlls.Default.LookX.ReadValue<float>() * lookSens.x * Time.deltaTime;
        float mouseY = playerControlls.Default.LookY.ReadValue<float>() * lookSens.y * Time.deltaTime;

        //camera math
        xRotation -= mouseY;
        xRotation = Math.Clamp(xRotation, -90f, 90f);

        //camera logic
        if (rotateCamNotPlayer)
        {
            Vector3 rot = playerCamera.transform.eulerAngles;
            playerCamera.transform.eulerAngles = new Vector3(xRotation,rot.y + mouseX, 0);
        }
        else
        {
            transform.Rotate(Vector3.up * mouseX);
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
    }
}
