using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointInputPrepScript : AbstractPreparationScript
{
    public bool allow = false;
    public Canvas arrowCanvas;

    public override void DoPreparation() {
        if(allow)
        {
            FindObjectOfType<PlayerMoveScript>().enabled = false;
            FindObjectOfType<PlayerWaypointMoveScript>().enabled = true;
            arrowCanvas.enabled = true;
        }
        else
        {
            FindObjectOfType<PlayerMoveScript>().enabled = true;
            FindObjectOfType<PlayerWaypointMoveScript>().enabled = false;
            arrowCanvas.enabled = false;
        }
    }
}
