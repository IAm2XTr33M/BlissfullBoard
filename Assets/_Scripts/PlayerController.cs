using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //singleton
    public static PlayerController instance;

    //variables
    public bool onHoverboard = false;
    public GameObject player; 
    public GameObject hoverboardCamera;
    [HideInInspector] public GameObject hoverboard;
    private bool ableToHopOff = false;

    public Vector2 movementInput = Vector2.zero;
    public bool Jumped = false;
    public float coinWinAmmount = 10;

    [SerializeField] int coins = 0;
    [SerializeField] TextMeshProUGUI coinText;



    //singleton logic
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //player input events
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        GetOff();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        Jumped = context.action.triggered;
    }


    private void Start()
    {
        //Set player true and hoverboard false
        player.SetActive(true);
        hoverboardCamera.SetActive(false);
    }

    //function to get off of hoverboard
    public void GetOff()
    {
        if (onHoverboard && ableToHopOff)
        {
            SetPlayerState(true);
            ableToHopOff = false;
        }
    }

    /// <summary>
    /// Swap state of player from on to off of the hoverboard
    /// </summary>
    /// <param name="state"></param>
    public void SetPlayerState(bool state)
    {
        onHoverboard = !state;

        if (!state)
        {
            hoverboardCamera.transform.position = player.transform.position;
            hoverboardCamera.transform.rotation = player.transform.rotation;
            hoverboardCamera.GetComponentInChildren<Camera>().transform.rotation = player.transform.GetComponentInChildren<Camera>().transform.rotation;
        }
        else
        {
            player.transform.position = hoverboardCamera.transform.position + new Vector3(0,1,0);
            player.transform.rotation = hoverboardCamera.transform.rotation;
            player.transform.GetComponentInChildren<Camera>().transform.rotation = hoverboardCamera.GetComponentInChildren<Camera>().transform.rotation;
           
            hoverboard = null;
        }

        StartCoroutine(EnableHopOff());

        player.SetActive(state);
        hoverboardCamera.SetActive(!state);
    }

    IEnumerator EnableHopOff()
    {
        yield return new WaitForSeconds(2);
        ableToHopOff = true;
    }

    //collect coin and update text
    public void collectCoin()
    {
        coins++;
        if(coins < coinWinAmmount)
        {
            coinText.text = coins.ToString() + "/10 coins";
        }
        else
        {
            WinscreenScript.instance.Win();
            WinscreenScript.instance.timeText.text = TimerController.instance.getTime();
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
