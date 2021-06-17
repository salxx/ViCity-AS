using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchPrepScript : AbstractPreparationScript
{
    public CameraPlayerSwitchScript script;

    public override void DoPreparation()
    {
        script.gameObject.SetActive(true);
    }
}
