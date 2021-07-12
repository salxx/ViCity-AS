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

    public bool clickMovement = false;

    public Image upArrow;
    public Image leftArrow;
    public Image downArrow;
    public Image rightArrow;

    public GameObject upArrowGo;
    public GameObject leftArrowGo;
    public GameObject downArrowGo;
    public GameObject rightArrowGo;

    private void Awake()
    {
        transform.position = current.transform.position;
        destination = current.transform.position;
    }

    private void OnEnable()
    {
        HideUIArrows();
        destination = transform.position;
        playerToDestination = destination - transform.position;
        waypointParent.SetActive(true);
    }

    private void OnDisable()
    {
        HideUIArrows();
        waypointParent.SetActive(false);
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;
        if(Mathf.Abs(transform.position.z - destination.z) > 0.2f || Mathf.Abs(transform.position.x - destination.x) > 0.2f) {
            HideUIArrows();
            direction = playerToDestination.normalized;
            transform.forward = playerToDestination;
            GetComponent<Animator>().SetBool("Running", direction.magnitude > 0);
            transform.position = transform.position + (Time.deltaTime * direction * 6f);
        } else
        {
            transform.position = destination;
            VisualizeUIArrows();
            Vector3 next = GetNextDestination();
            if(next.magnitude > 0.01f) {
                destination = next;
                playerToDestination = destination - transform.position;
                playerToDestination.y = 0;
            }
            GetComponent<Animator>().SetBool("Running", false);
        }
    }

    private Vector3 GetNextDestination() {
        Vector3 movement = Vector3.zero;
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && current.up != null) {
            movement = current.up.transform.position;
            current = current.up;
        } else
        if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && current.left != null) {
            movement = current.left.transform.position;
            current = current.left;
        } else
        if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && current.down != null) {
            movement = current.down.transform.position;
            current = current.down;
        } else
        if((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && current.right != null) {
            movement = current.right.transform.position;
            current = current.right;
        }
        return movement;
    }

    private void VisualizeUIArrows() {
        if(clickMovement)
        {
            upArrow.enabled = current.up != null;
            leftArrow.enabled = current.left != null;
            downArrow.enabled = current.down != null;
            rightArrow.enabled = current.right != null;
        } else
        {
            upArrowGo.SetActive(current.up != null);
            leftArrowGo.SetActive(current.left != null);
            downArrowGo.SetActive(current.down != null);
            rightArrowGo.SetActive(current.right != null);
        }
    }

    private void HideUIArrows() {
        upArrow.enabled = false;
        leftArrow.enabled = false;
        downArrow.enabled = false;
        rightArrow.enabled = false;

        upArrowGo.SetActive(false);
        leftArrowGo.SetActive(false);
        downArrowGo.SetActive(false);
        rightArrowGo.SetActive(false);
    }

}
