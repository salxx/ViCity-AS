using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerScriptNet : MonoBehaviour
{
    private Transform player;

    private void LateUpdate()
    {
        if(player == null)
        {
            if(FindObjectOfType<PlayerMoveScriptNet>() && FindObjectOfType<PlayerMoveScriptNet>().isLocalPlayer)
            {
                player = FindObjectOfType<PlayerMoveScriptNet>().transform;
            }
        } else
        {
            CalculateAdaptiveCamera();
        }
    }

    private void CalculateAdaptiveCamera()
    {
        string log = "";
        Vector3 newDestination;
        float MIN_DISTANCE = 2f;
        float MAX_DISTANCE = 7f;
        float DELTA_DISTANCE = MAX_DISTANCE - MIN_DISTANCE;
        List<AdaptiveCameraTarget> targetsInRange = new List<AdaptiveCameraTarget>();
        targetsInRange.AddRange(AdaptiveCameraTarget.cameraTargets);
        targetsInRange.RemoveAll(t => (t.transform.position - player.position).magnitude > MAX_DISTANCE);
        targetsInRange.Remove(player.GetComponent<AdaptiveCameraTarget>());
        if (targetsInRange.Count > 0)
        {
            //transform.position = Vector3.Lerp(targetsInRange[0].transform.position, player.position, (distance - MIN_DISTANCE) / DELTA_DISTANCE);
            Vector3 center = Vector3.zero;
            float maxWeight = 0f;
            float w;
            targetsInRange.ForEach(t => {
                float distance = (t.transform.position - player.position).magnitude;
                distance -= MIN_DISTANCE;
                w = (DELTA_DISTANCE - distance) / DELTA_DISTANCE;
                log += " w: " + w;
                maxWeight += w;
                center += t.transform.position * w;
                });
            log += " maxW: " + maxWeight;
            w = 1 / (float) targetsInRange.Count;
            center += player.position * w;
            maxWeight += w;
            newDestination = center / maxWeight;
        } else
        {
            newDestination = player.position;
        }
        if((transform.position - newDestination).magnitude > 0.05f) {
            transform.position = Vector3.Lerp(transform.position, newDestination, Time.deltaTime * 15f);
        }
        targetsInRange.ForEach(t => {
            float distance = (t.transform.position - player.position).magnitude;
            distance -= MIN_DISTANCE;
            float w = (DELTA_DISTANCE - distance) / DELTA_DISTANCE;
            Debug.DrawLine(t.transform.position + Vector3.up, transform.position + Vector3.up, Color.green * w);
        });
        Debug.DrawLine(player.position + Vector3.up, transform.position + Vector3.up, Color.blue);
        Debug.Log(log);
    }
}
