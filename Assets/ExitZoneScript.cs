using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitZoneScript : MonoBehaviour
{
    public UIQueryManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerMoveScript>())
        {
            manager.NotifyPageDone();
        }
    }
}
