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

    public Button upArrow;
    public Button leftArrow;
    public Button downArrow;
    public Button rightArrow;

    public GameObject upArrowGo;
    public GameObject leftArrowGo;
    public GameObject downArrowGo;
    public GameObject rightArrowGo;

    int nextMoveDirection = -1;

    private void Awake()
    {
        transform.position = current.transform.position;
        destination = current.transform.position;
        upArrow.onClick.AddListener(() => nextMoveDirection = 0);
        leftArrow.onClick.AddListener(() => nextMoveDirection = 1);
        downArrow.onClick.AddListener(() => nextMoveDirection = 2);
        rightArrow.onClick.AddListener(() => nextMoveDirection = 3);
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
        if(clickMovement)
        {
            switch(nextMoveDirection)
            {
                case 0:
                    movement = current.up.transform.position;
                    current = current.up;
                    break;
                case 1:
                    movement = current.left.transform.position;
                    current = current.left;
                    break;
                case 2:
                    movement = current.down.transform.position;
                    current = current.down;
                    break;
                case 3:
                    movement = current.right.transform.position;
                    current = current.right;
                    break;
            }
            nextMoveDirection = -1;
        } else
        {
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
        }
        return movement;
    }

    private void VisualizeUIArrows() {
        if(clickMovement)
        {
            upArrow.gameObject.SetActive(current.up != null);
            leftArrow.gameObject.SetActive(current.left != null);
            downArrow.gameObject.SetActive(current.down != null);
            rightArrow.gameObject.SetActive(current.right != null);
        } else
        {
            upArrowGo.SetActive(current.up != null);
            leftArrowGo.SetActive(current.left != null);
            downArrowGo.SetActive(current.down != null);
            rightArrowGo.SetActive(current.right != null);
        }
    }

    public void HideUIArrows() {
        upArrow.gameObject.SetActive(false);
        leftArrow.gameObject.SetActive(false);
        downArrow.gameObject.SetActive(false);
        rightArrow.gameObject.SetActive(false);

        upArrowGo.SetActive(false);
        leftArrowGo.SetActive(false);
        downArrowGo.SetActive(false);
        rightArrowGo.SetActive(false);
    }

}
