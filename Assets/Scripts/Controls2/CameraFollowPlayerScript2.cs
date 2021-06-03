using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerScript2 : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        Vector3 newPos = player.transform.position;
        transform.position = newPos;
    }
}
