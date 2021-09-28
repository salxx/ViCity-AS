using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveCameraTarget : MonoBehaviour
{
    public static List<AdaptiveCameraTarget> cameraTargets = new List<AdaptiveCameraTarget>();

    public bool isExhibit = false;
    public float exhibitFocusDistance = 3f;

    private void OnEnable()
    {
        cameraTargets.Add(this);
    }

    private void OnDisable()
    {
        if(cameraTargets.Contains(this))
        {
            cameraTargets.Remove(this);
        }
    }

}
