using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseMoveScript : MonoBehaviour
{
    Vector3 destination;
    Vector3 playerToDestination;
    public GameObject indicator;

    private void Awake()
    {
        indicator.gameObject.SetActive(false);
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
        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                destination = hit.point;
                playerToDestination = destination - transform.position;
                playerToDestination.x = 0;
                playerToDestination.y = 0;
            }
        }

        Vector3 direction = Vector3.zero;
        if(Mathf.Abs(transform.position.z - destination.z) > 0.1f) {
            direction = playerToDestination.normalized;
            transform.forward = playerToDestination;

            Vector3 indicatorPos = destination;
            indicatorPos.x = transform.position.x;
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
}
