using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CameraSwapScript : MonoBehaviour
{
    public TextMeshProUGUI cameraText;
    public TextMeshProUGUI fovText;

    Camera[] cameras;
    int activeCamera = 0;
    int currentFOV = 40;

    void Start()
    {
        cameras = GetComponentsInChildren<Camera>();

        foreach(Camera c in cameras)
        {
            c.gameObject.SetActive(false);
        }

        cameras[0].gameObject.SetActive(true);
        cameras[activeCamera].fieldOfView = currentFOV;
        UpdateCameraText();
        UpdateFOV();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            cameras[activeCamera].gameObject.SetActive(false);
            activeCamera = (activeCamera + 1) % cameras.Length;
            cameras[activeCamera].gameObject.SetActive(true);
            cameras[activeCamera].fieldOfView = currentFOV;
            UpdateCameraText();
            UpdateFOV();
        } else if(Input.GetKeyDown(KeyCode.X))
        {
            cameras[activeCamera].gameObject.SetActive(false);
            activeCamera = (cameras.Length + activeCamera - 1) % cameras.Length;
            cameras[activeCamera].gameObject.SetActive(true);
            cameras[activeCamera].fieldOfView = currentFOV;
            UpdateCameraText();
            UpdateFOV();
        } else if(Input.GetKeyDown(KeyCode.V))
        {
            currentFOV += 5;
            if(currentFOV > 60)
            {
                currentFOV = 40;
            }
            cameras[activeCamera].fieldOfView = currentFOV;
            UpdateFOV();
        }
    }

    private void UpdateFOV()
    {
        fovText.text = "FOV: " + cameras[activeCamera].fieldOfView;
    }

    private void UpdateCameraText()
    {
        cameraText.text = "Active Camera:\nHorizontal: " + cameras[activeCamera].transform.eulerAngles.x + "\nVertical: " + (270 - cameras[activeCamera].transform.parent.eulerAngles.y);
    }
}
