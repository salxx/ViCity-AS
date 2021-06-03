using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMouseNavMeshMoveScript : MonoBehaviour
{
    Vector3 destination;
    Vector3 playerToDestination;
    public GameObject indicator;
    public NavMeshAgent agent;

    float speed;

    private void Awake()
    {
        indicator.gameObject.SetActive(false);
        speed = agent.speed;
    }

    private void OnEnable()
    {
        destination = transform.position;
    }

    private void OnDisable()
    {
        indicator.gameObject.SetActive(false);
    }

    void Update()
    {
        bool moving = true;
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                destination = hit.point;
                destination.y = 0;
                moving = true;
                agent.SetDestination(destination);
            }
        }

        if((transform.position - destination).magnitude < 0.1f)
        {
            moving = false;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            agent.speed = speed * 2f;
        } else
        {
            agent.speed = speed;
        }

        GetComponent<Animator>().SetBool("Walking", !Input.GetKey(KeyCode.LeftShift) && moving);
        GetComponent<Animator>().SetBool("Running", Input.GetKey(KeyCode.LeftShift) && moving);
    }

}
