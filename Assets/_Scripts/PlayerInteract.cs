using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Transform playerCamera;
    [SerializeField]
    private float interactRange = 10;

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>().gameObject.transform;
    }

    private I_Interactable previousScript;
    private void Update()
    {
        RaycastHit HitInfo;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out HitInfo, interactRange))
        {
            I_Interactable script = HitInfo.collider.gameObject.GetComponent<I_Interactable>();
            if (script != null)
            {
                previousScript = script;
                script.toggleUI(true);
            }
            if(previousScript != script)
            {
                previousScript.toggleUI(false);
            }
        }
    }

    public void Interact()
    {
        RaycastHit HitInfo;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out HitInfo, 100.0f))
        {
            var script = HitInfo.collider.gameObject.GetComponent<I_Interactable>();
            if (script != null)
            {
                script.Interact();
            }
        }
    }
}
