using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerOnly : MonoBehaviour
{
    void Start()
    {
        if (!Application.isBatchMode && !Application.isEditor)
        {
            GetComponent<NetworkManager>().StartServer();
        }
    }
}
