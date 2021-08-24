using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraPlayerSwitchScript : MonoBehaviour
{
    public GameObject cameraParent;
    public TextMeshProUGUI cameraNameText;

    public void OnEnable()
    {
        cameraNameText.text = LanguageSelection.lang == 0 ? "Kamera 1 - Referenz" : "Camera 1 - Reference";
        CameraSwitch0(0);
    }

    private void Update()
    {
        CameraSwitch();
    }

    private void CameraSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            cameraNameText.text = LanguageSelection.lang == 0 ? "Kamera 1 - Referenz" : "Camera 1 - Reference";
            CameraSwitch0(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            cameraNameText.text = LanguageSelection.lang == 0 ? "Kamera 2 - Mehr Distanz" : "Camera 2 - More Distance";
            CameraSwitch0(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            cameraNameText.text = LanguageSelection.lang == 0 ? "Kamera 3 - Mehr Meigung" : "Camera 3 - More Incline";
            CameraSwitch0(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            cameraNameText.text = LanguageSelection.lang == 0 ? "Kamera 4 - Weniger Tiefe" : "Camera 4 - Less Depth";
            CameraSwitch0(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            cameraNameText.text = LanguageSelection.lang == 0 ? "Kamera 5 - Weniger Neigung" : "Camera 5 - Less Incline";
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
