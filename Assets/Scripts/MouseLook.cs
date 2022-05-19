using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;
    public Rigidbody rB;

    [Header("Settings")]
    
    public float walkSpeed = 100f;

    public float mouseSens = 100f;

    private float xRot = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSens * Time.deltaTime;
        Vector2 walk = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * walkSpeed * Time.deltaTime;

        xRot = Mathf.Clamp(xRot - mouse.y*25, -90f, 90f);
        
        camera.transform.localRotation = Quaternion.Euler(xRot, 0,0);
        player.transform.RotateAround(Vector3.up, mouse.x);

        rB.AddForce(player.transform.forward * walk.y * walkSpeed * Time.deltaTime +
                    player.transform.right * walk.x * walkSpeed * Time.deltaTime,
                    ForceMode.Force);
    }
}
