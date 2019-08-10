using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewChange : MonoBehaviour
{
    //This is Main Camera in the Scene
    Camera mainCam;
    //This is the second Camera and is assigned in inspector
    public Camera stillCamOne;
    public Camera stillCamTwo;
    public Camera stillCamThree;

    int randomCam = 1;

    int cameraChange = 5;

    int temp = 0;

    void Start()
    {
        //This gets the Main Camera from the Scene
        mainCam = Camera.main;
        //This enables Main Camera
        mainCam.enabled = true;
        //Use this to disable secondary Camera
        stillCamOne.enabled = false;
        stillCamTwo.enabled = false;
        stillCamThree.enabled = false;

        InvokeRepeating("ranCam", 0, cameraChange);
    }

    void Update()
    {
       
    }

    public void ranCam()
    {
        randomCam = Random.Range(1, 4);

        if (randomCam == temp)
        {
            ranCam();
            return;
        }

        temp = randomCam;

        if (randomCam == 1)
        {
            Debug.Log("Cam 1");
            mainCam.enabled = true;
            stillCamOne.enabled = false;
            stillCamTwo.enabled = false;
            stillCamThree.enabled = false;
        }
        if (randomCam == 2)
        {
            Debug.Log("Cam 2");
            mainCam.enabled = false;
            stillCamOne.enabled = true;
            stillCamTwo.enabled = false;
            stillCamThree.enabled = false;
        }
        if (randomCam == 3)
        {
            Debug.Log("Cam 3");
            mainCam.enabled = false;
            stillCamOne.enabled = false;
            stillCamTwo.enabled = true;
            stillCamThree.enabled = false;
        }
        if (randomCam == 4)
        {
            Debug.Log("Cam 4");
            mainCam.enabled = false;
            stillCamOne.enabled = false;
            stillCamTwo.enabled = false;
            stillCamThree.enabled = true;
        }
    }
}
