using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerScriptNet : MonoBehaviour
{
    private Transform player;
    static float CAMERA_SMOOTH = 5f;

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
        float MAX_DISTANCE = 7f;

        // first check if player is close to exhibit
        AdaptiveCameraTarget closeExhibit;
        if (IsInExhibitFocusRange(out closeExhibit))
        {
            Debug.Log("Focus on exhibit: " + closeExhibit.name);
            newDestination = closeExhibit.transform.position;
            newDestination.y = player.position.y;
            transform.position = Vector3.Lerp(transform.position, newDestination, Time.deltaTime * CAMERA_SMOOTH);
            return;
        }

        // second: find and interpolate between all close targets
        List<AdaptiveCameraTarget> targetsInRange = new List<AdaptiveCameraTarget>();
        targetsInRange.AddRange(AdaptiveCameraTarget.cameraTargets);
        targetsInRange.RemoveAll(t => CalculateTargetDistanceFromPlayer(t) > MAX_DISTANCE);
        targetsInRange.Remove(player.GetComponent<AdaptiveCameraTarget>());
        if (targetsInRange.Count > 0)
        {
            Vector3 center = Vector3.zero;
            float maxWeight = 0f;
            float w;
            targetsInRange.ForEach(t => {
                float distance = CalculateTargetDistanceFromPlayer(t);
                w = Mathf.Pow((MAX_DISTANCE - distance) / MAX_DISTANCE, 2f);
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

        newDestination.y = player.position.y;
        if ((transform.position - newDestination).magnitude > 0.05f) {
            transform.position = Vector3.Lerp(transform.position, newDestination, Time.deltaTime * CAMERA_SMOOTH);
        }

        targetsInRange.ForEach(t => {
            float distance = CalculateTargetDistanceFromPlayer(t);
            float w = Mathf.Pow((MAX_DISTANCE - distance) / MAX_DISTANCE, 2f);
            Debug.DrawLine(t.transform.position + Vector3.up, transform.position + Vector3.up, Color.green * w);
        });
        //Debug.DrawLine(player.position + Vector3.up, transform.position + Vector3.up, Color.blue);
        Debug.Log(log);
    }

    private float CalculateTargetDistanceFromPlayer(AdaptiveCameraTarget t)
    {
        Vector3 from = t.transform.position;
        from.y = player.position.y;
        return (from - player.position).magnitude;
    }

    private bool IsInExhibitFocusRange(out AdaptiveCameraTarget exhibit)
    {
        List<AdaptiveCameraTarget> targetsInRange = new List<AdaptiveCameraTarget>();
        targetsInRange.AddRange(AdaptiveCameraTarget.cameraTargets);
        targetsInRange.Remove(player.GetComponent<AdaptiveCameraTarget>());
        targetsInRange.RemoveAll(t => !t.isExhibit || CalculateTargetDistanceFromPlayer(t) > t.exhibitFocusDistance);
        if(targetsInRange.Count == 0)
        {
            exhibit = null;
            return false;
        } else if(targetsInRange.Count == 1)
        {
            exhibit = targetsInRange[0];
            return true;
        } else // find closest exhibit
        {
            exhibit = targetsInRange[0];
            foreach (AdaptiveCameraTarget t in targetsInRange)
            {
                if(t.exhibitFocusDistance < exhibit.exhibitFocusDistance)
                {
                    exhibit = t;
                }
            }
            return true;
        }
    }
}
