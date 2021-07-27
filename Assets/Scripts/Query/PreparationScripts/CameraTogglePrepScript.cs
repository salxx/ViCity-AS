using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTogglePrepScript : AbstractPreparationScript
{
    public CameraPlayerToggleReferenceScript script;
    public bool enableScript = true;
    public int cameraNr = 2;

    public override void DoPreparation()
    {
        script.currentCamera = cameraNr;
        script.enabled = enableScript;
        script.CameraToggle0(cameraNr);
    }
}
