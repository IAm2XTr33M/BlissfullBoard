using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    //changeable variables
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    //private variables
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput = Vector2.zero;
    private bool Jumped = false;

    
    private void Start()
    {
        //set reference
        controller = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        getInput();
        movePlayer();
    }
    void getInput()
    {
        //get the input
        movementInput = PlayerController.instance.movementInput;
        Jumped = PlayerController.instance.Jumped;
    }

    void movePlayer()
    {
        //Check if grounded and set velocity to 0
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        //Move player WSAD
        Vector3 move = transform.right * movementInput.x + transform.forward * movementInput.y;
        controller.Move(move * Time.deltaTime * playerSpeed);

        //Jumping calculation
        if (Jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        //Jump logic
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

}
