using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlToggleScript : MonoBehaviour
{
    public PlayerButtonMoveScript buttonControl;
    public PlayerMouseMoveScript mouseControl;

    private void Start()
    {
        mouseControl.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            buttonControl.enabled = !buttonControl.enabled;
            mouseControl.enabled = !mouseControl.enabled;
        }
    }
}
