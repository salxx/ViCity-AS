using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    void Update()
    {
        Vector3 direciton = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direciton += transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direciton -= transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direciton -= transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direciton += transform.right;
        }
        direciton.y = 0;
        direciton.Normalize();
        if (Input.GetKey(KeyCode.E))
        {
            direciton += Vector3.up;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            direciton += Vector3.down;
        }
        transform.position = transform.position += Time.deltaTime * direciton * (Input.GetKey(KeyCode.LeftShift) ? 6f : 3f);
    }
}
