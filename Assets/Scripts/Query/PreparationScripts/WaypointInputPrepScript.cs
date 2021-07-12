using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointInputPrepScript : AbstractPreparationScript
{
    public bool allow = false;
    public bool clickMovement = false;
    public Canvas arrowCanvas;
    public GameObject arrowGameObject;

    public override void DoPreparation() {
        if(allow)
        {
            FindObjectOfType<PlayerMoveScript>().enabled = false;
            FindObjectOfType<PlayerWaypointMoveScript>().gameObject.transform.position = FindObjectOfType<PlayerWaypointMoveScript>().current.transform.position;
            FindObjectOfType<PlayerWaypointMoveScript>().HideUIArrows();
            FindObjectOfType<PlayerWaypointMoveScript>().enabled = true;
            FindObjectOfType<PlayerWaypointMoveScript>().GetComponent<CharacterController>().enabled = false;
            FindObjectOfType<PlayerWaypointMoveScript>().clickMovement = clickMovement;
            arrowCanvas.enabled = true;
            arrowGameObject.SetActive(true);
        }
        else
        {
            FindObjectOfType<PlayerMoveScript>().enabled = true;
            FindObjectOfType<PlayerWaypointMoveScript>().HideUIArrows();
            FindObjectOfType<PlayerWaypointMoveScript>().enabled = false;
            FindObjectOfType<PlayerWaypointMoveScript>().GetComponent<CharacterController>().enabled = true;
            arrowCanvas.enabled = false;
            arrowGameObject.SetActive(false);
        }
    }
}
