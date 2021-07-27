using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraPlayerToggleReferenceScript : MonoBehaviour
{
    public GameObject cameraParent;
    public TextMeshProUGUI cameraNameText;
    public int currentCamera = 2;
    bool isReference = false;

    private void OnEnable()
    {
        CameraToggle0(currentCamera);
    }

    private void Update()
    {
        CameraToggle();
    }

    private void CameraToggle()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isReference)
            {
                CameraToggle0(currentCamera);
            } else
            {
                CameraToggle0(0);
            }
        }
    }

    public void CameraToggle0(int index)
    {
        switch (index)
        {
            case 0:
                isReference = true;
                cameraNameText.text = LanguageSelection.lang == 0 ? "Kamera 1 - Referenz" : "Camera 1 - Reference";
                break;
            case 1:
                isReference = false;
                cameraNameText.text = LanguageSelection.lang == 0 ? "Kamera 2 - Mehr Distanz" : "Camera 2 - More Distance";
                break;
            case 2:
                isReference = false;
                cameraNameText.text = LanguageSelection.lang == 0 ? "Kamera 3 - Mehr Meigung" : "Camera 3 - More Incline";
                break;
            case 3:
                isReference = false;
                cameraNameText.text = LanguageSelection.lang == 0 ? "Kamera 4 - Weniger Tiefe" : "Camera 4 - Less Depth";
                break;
            case 4:
                isReference = false;
                cameraNameText.text = LanguageSelection.lang == 0 ? "Kamera 5 - Weniger Neigung" : "Camera 5 - Less Incline";
                break;
        }

        for (int i = 0; i < cameraParent.transform.childCount; i++)
        {
            cameraParent.transform.GetChild(i).gameObject.SetActive(false);
        }
        cameraParent.transform.GetChild(index).gameObject.SetActive(true);
    }
}
