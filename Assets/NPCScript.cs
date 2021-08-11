using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    void Start()
    {
        transform.forward = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

        Destroy(this);
    }

    void Update()
    {
        
    }
}
