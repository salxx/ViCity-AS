using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseWaypointMoveScript : MonoBehaviour
{
    Vector3 destination;
    Vector3 playerToDestination;
    public GameObject indicator;

    public GameObject waypointParent;

    List<(Waypoint, Waypoint)> paths = new List<(Waypoint, Waypoint)>();

    public Waypoint wp1;
    public Waypoint wp2;

    private void Awake()
    {
        indicator.gameObject.SetActive(false);
        foreach(Waypoint wp in waypointParent.GetComponentsInChildren<Waypoint>())
        {
            foreach (Waypoint wp2 in wp.connections)
            {
                if(!paths.Contains((wp2, wp)))
                {
                    paths.Add((wp, wp2));
                    Debug.Log("Added Path: " + wp.transform.name + " - " + wp2.transform.name);
                }
            }
        }
        transform.position = wp1.transform.position + (wp2.transform.position - wp1.transform.position) / 2f;
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
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                destination = GetDestinationOnPath(hit.point);
                playerToDestination = destination - transform.position;
                playerToDestination.y = 0;
            }
        }

        Vector3 direction = Vector3.zero;
        if(Mathf.Abs(transform.position.z - destination.z) > 0.1f) {
            direction = playerToDestination.normalized;
            transform.forward = playerToDestination;

            Vector3 indicatorPos = destination;
            indicatorPos.y = 0.001f;
            indicator.transform.position = indicatorPos;
            indicator.gameObject.SetActive(true);
        } else
        {
            indicator.gameObject.SetActive(false);
        }
        GetComponent<Animator>().SetBool("Walking", !Input.GetKey(KeyCode.LeftShift) && direction.magnitude > 0);
        GetComponent<Animator>().SetBool("Running", Input.GetKey(KeyCode.LeftShift) && direction.magnitude > 0);
        transform.position = transform.position += Time.deltaTime * direction * (Input.GetKey(KeyCode.LeftShift) ? 4f : 2f);
    }

    private Vector3 GetDestinationOnPath(Vector3 point)
    {
        point.y = 0;
        int indexShortest = 0;
        int index = 0;
        float shortest = 999999;
        Vector3 destinationPoint = transform.position;
        foreach((Waypoint, Waypoint) path in paths)
        {
            Vector3 v1 = path.Item1.transform.position;
            Vector3 v2 = path.Item2.transform.position;
            v1.y = 0;
            v2.y = 0;

            float angle = Vector3.Angle(v2-v1, point-v1);
            float angle2 = Vector3.Angle(v1-v2, point-v2);
            float distance = (point - v1).magnitude * Mathf.Sin(Mathf.Deg2Rad * angle);
            if(Mathf.Abs(distance) < shortest)
            {
                indexShortest = index;
                shortest = distance;
                float destinationPointOffset = Mathf.Cos(Mathf.Deg2Rad * angle) * (point - v1).magnitude;
                destinationPoint = v1 + (v2 - v1).normalized * destinationPointOffset;
            }
            index++;
        }
        return destinationPoint;
    }
}
