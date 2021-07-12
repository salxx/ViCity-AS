using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerSwitchScript : MonoBehaviour
{
    public GameObject cameraParent;

    private void Update()
    {
        CameraSwitch();
    }

    private void CameraSwitch()
    {
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))
        {
            CameraSwitch0(0);
        }
        if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
        {
            CameraSwitch0(1);
        }
        if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
        {
            CameraSwitch0(2);
        }
        if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4))
        {
            CameraSwitch0(3);
        }
        if (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5))
        {
            CameraSwitch0(4);
        }
    }

    private void CameraSwitch0(int index)
    {
        for (int i = 0; i < cameraParent.transform.childCount; i++)
        {
            cameraParent.transform.GetChild(i).gameObject.SetActive(false);
        }
        cameraParent.transform.GetChild(index).gameObject.SetActive(true);
    }
}
