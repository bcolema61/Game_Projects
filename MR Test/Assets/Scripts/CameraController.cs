using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; //Camera will focus this object

    public Vector3 offset; //Camera position offset

    private float currentZoom = 10f; //How far the camera is away from the target

    public float zoomSpeed = 4f; //Zoom in/out speed
    public float minZoom = 0f; //Minimum zoom distance
    public float maxZoom = 150f; //Maximum zoom distance

    public float pitch = 2f; //Rate at which camera aims at target

    public float yawSpeed = 100f; //Speed for caculating yaw when zooming

    public float currentYaw = 0f; //Yaw position of the camera
    
    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom; //Keeps camera the correct distance from the target
        transform.LookAt(target.position + Vector3.up * pitch); //Keeps camera aimed at target

        transform.RotateAround(target.position, Vector3.up, currentYaw);  //Keeps the camera rotated around target (when currentYaw is changed)
    }

    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; //lowers and raises currentZoom based on mouse scroll wheel
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom); //keeps currentZoom locked between minZoom and maxZoom

        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime; //lowers and raises currentYaw when camera is rotated around target based on horizontal movement input
    }
}
