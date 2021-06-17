using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPrepScript : AbstractPreparationScript
{
    public GameObject cameraParent;
    public GameObject cameraToEnable;

    public override void DoPreparation()
    {
        for(int i = 0; i < cameraParent.transform.childCount; i++)
        {
            cameraParent.transform.GetChild(i).gameObject.SetActive(false);
        }
        cameraToEnable.SetActive(true);
    }
}
