using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWaypointMoveScript : MonoBehaviour
{
    Vector3 destination;
    Vector3 playerToDestination;

    public GameObject waypointParent;
    public Waypoint current;
    public Image upArrow;
    public Image leftArrow;
    public Image downArrow;
    public Image rightArrow;

    private void Awake()
    {
        transform.position = current.transform.position;
        destination = current.transform.position;
    }

    private void OnEnable()
    {
        destination = transform.position;
    }

    private void OnDisable()
    {
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;
        if(Mathf.Abs(transform.position.z - destination.z) > 0.05f || Mathf.Abs(transform.position.x - destination.x) > 0.05f) {
            HideUIArrows();
            direction = playerToDestination.normalized;
            transform.forward = playerToDestination;
        } else
        {
            VisualizeUIArrows();
            Vector3 next = GetNextDestination();
            if(next.magnitude > 0.01f) {
                destination = next;
                playerToDestination = destination - transform.position;
                playerToDestination.y = 0;
            }
        }
        GetComponent<Animator>().SetBool("Walking", !Input.GetKey(KeyCode.LeftShift) && direction.magnitude > 0);
        GetComponent<Animator>().SetBool("Running", Input.GetKey(KeyCode.LeftShift) && direction.magnitude > 0);
        transform.position = transform.position += Time.deltaTime * direction * (Input.GetKey(KeyCode.LeftShift) ? 4f : 2f);
    }

    private Vector3 GetNextDestination() {
        Vector3 movement = Vector3.zero;
        if(Input.GetKeyDown(KeyCode.W) && current.up != null) {
            movement = current.up.transform.position;
            current = current.up;
        } else
        if(Input.GetKeyDown(KeyCode.A) && current.left != null) {
            movement = current.left.transform.position;
            current = current.left;
        } else
        if(Input.GetKeyDown(KeyCode.S) && current.down != null) {
            movement = current.down.transform.position;
            current = current.down;
        } else
        if(Input.GetKeyDown(KeyCode.D) && current.right != null) {
            movement = current.right.transform.position;
            current = current.right;
        }
        return movement;
    }

    private void VisualizeUIArrows() {
        upArrow.enabled = current.up != null;
        leftArrow.enabled = current.left != null;
        downArrow.enabled = current.down != null;
        rightArrow.enabled = current.right != null;
    }

    private void HideUIArrows() {
        upArrow.enabled = false;
        leftArrow.enabled = false;
        downArrow.enabled = false;
        rightArrow.enabled = false;
    }

}
