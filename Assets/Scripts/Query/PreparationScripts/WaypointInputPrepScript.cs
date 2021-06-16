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
            FindObjectOfType<PlayerWaypointMoveScript>().gameObject.transform.position = FindObjectOfType<PlayerWaypointMoveScript>().current.transform.position;
            FindObjectOfType<PlayerWaypointMoveScript>().enabled = true;
            FindObjectOfType<PlayerWaypointMoveScript>().GetComponent<CharacterController>().enabled = false;
            arrowCanvas.enabled = true;
        }
        else
        {
            FindObjectOfType<PlayerMoveScript>().enabled = true;
            FindObjectOfType<PlayerWaypointMoveScript>().enabled = false;
            FindObjectOfType<PlayerWaypointMoveScript>().GetComponent<CharacterController>().enabled = true;
            arrowCanvas.enabled = false;
        }
    }
}
