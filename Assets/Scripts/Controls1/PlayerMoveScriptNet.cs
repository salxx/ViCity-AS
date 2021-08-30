using System;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoveScriptNet : NetworkBehaviour
{
    Vector3 destination;
    Vector3 playerToDestination;
    public GameObject indicatorPrefab;
    GameObject indicator;
    public LayerMask mouseTarget;
    public bool allowKeys = true;
    public bool allowMouse = false;
    CharacterController characterController;

    Vector3 zeroPosition = new Vector3(20, 0, -18);

    [SyncVar(hook = nameof(SetAvatarName))]
    public string avatarName = "";

    float previousDestinationDistance;


    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        indicator = Instantiate<GameObject>(indicatorPrefab);
        indicator.gameObject.SetActive(false);
        characterController = GetComponent<CharacterController>();
    }

    public override void OnStartClient()
    {
        avatarName = FindObjectOfType<TMPro.TMP_InputField>().text;
        GetComponentInChildren<TMPro.TextMeshProUGUI>().text = avatarName;
    }


    void SetAvatarName(string oldName, string newName)
    {
        avatarName = newName;
        GetComponentInChildren<TMPro.TextMeshProUGUI>().text = avatarName;
    }


    private void OnEnable()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        destination = transform.position;
    }

    private void OnDisable()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        indicator.gameObject.SetActive(false);
    }

    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        Vector3 direction = Vector3.zero;
        if (allowKeys)
        {
            direction = KeyMovement();
        }
        if(direction.magnitude < 0.01f && allowMouse)
        {
            direction = MouseMovement();
        }
        else
        {
            destination = transform.position;
            indicator.gameObject.SetActive(false);
        }
        GetComponent<Animator>().SetBool("Running", direction.magnitude > 0);
        characterController.Move(Time.deltaTime * direction * 6f);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            Vector3 ppos = transform.position;
            ppos.y = hit.point.y;
            transform.position = ppos;
        }
    }

    int frames = 0;

    private Vector3 MouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, mouseTarget))
            {
                destination = hit.point;
                playerToDestination = destination - transform.position;
                previousDestinationDistance = playerToDestination.magnitude;
                playerToDestination.y = 0;
            }
        }

        Vector3 direction = Vector3.zero;
        if ((transform.position - destination).magnitude > 0.1f)
        {
            playerToDestination = destination - transform.position;
            playerToDestination.y = 0;
            direction = playerToDestination.normalized;
            transform.forward = playerToDestination;

            Vector3 indicatorPos = destination;
            indicatorPos.y = 0.001f;
            indicator.transform.position = indicatorPos;
            indicator.gameObject.SetActive(true);

            if(playerToDestination.magnitude >= previousDestinationDistance)
            {
                if(frames >= 10)
                {
                    destination = transform.position;
                } else
                {
                    frames++;
                }
            } else
            {
                previousDestinationDistance = playerToDestination.magnitude;
                frames = 0;
            }
        }
        else
        {
            indicator.gameObject.SetActive(false);
        }
        return direction;
    }

    private Vector3 KeyMovement()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector3.right;
        }
        float angle = 5 + Vector3.Angle(transform.forward, direction.magnitude > 0.01f ? direction : transform.forward) / 8f;
        transform.forward = Vector3.RotateTowards(transform.forward, direction.magnitude > 0.01f ? direction : transform.forward, Time.deltaTime * angle, 0);
        direction.y = 0;
        direction.Normalize();
        return direction;
    }

}
