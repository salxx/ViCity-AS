using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonMoveScript : MonoBehaviour
{
    void Update()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.D))
        {
            transform.forward = Vector3.forward;
            direction += transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.forward = Vector3.back;
            direction += transform.forward;
        }
        direction.y = 0;
        direction.Normalize();

        GetComponent<Animator>().SetBool("Walking", !Input.GetKey(KeyCode.LeftShift) && direction.magnitude > 0);
        GetComponent<Animator>().SetBool("Running", Input.GetKey(KeyCode.LeftShift) && direction.magnitude > 0);

        transform.position = transform.position += Time.deltaTime * direction * (Input.GetKey(KeyCode.LeftShift) ? 4f : 2f);
    }
}
