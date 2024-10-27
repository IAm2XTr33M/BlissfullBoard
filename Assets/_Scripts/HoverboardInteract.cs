using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverboardInteract : MonoBehaviour, I_Interactable //interface
{
    //variables
    [SerializeField]
    private GameObject uiElement;

    private GameObject playerPrefab, skaterPrefab;

    public void Interact()
    {
        //Interact with the hoverboard to get on
        if (!PlayerController.instance.onHoverboard)
        {
            //check if hoverboard is flipped or not

            bool IsUpsideDown = Vector3.Dot(transform.up, Vector3.up) < 0;
            if (IsUpsideDown)
            {
                StartCoroutine(flipUp());
            }
            else
            {
                PlayerController.instance.SetPlayerState(false);
                PlayerController.instance.hoverboard = gameObject;
                PlayerController.instance.hoverboardCamera.GetComponent<FollowObject>().followObject = gameObject;
                toggleUI(false);
            }
        }
    }

    IEnumerator flipUp()
    {
        GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(1 / 30);
            transform.position += new Vector3(0, 1.5f/30, 0);
        }
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(2 / 10);
            transform.eulerAngles += new Vector3(0, 0, 180 / 10);
        }
        GetComponent<BoxCollider>().enabled = true;
    }

    public void toggleUI(bool state)
    {
        //toggle the UI of the hoverboard
        if (uiElement != null && uiElement.activeSelf != state)
        {
            uiElement.SetActive(state);
        }
    }
}
