using UnityEngine;

public class hbController : MonoBehaviour
{
    //changeable Variables
    [SerializeField] ScriptableBoard boardSettings;

    [SerializeField] Transform[] anchors = new Transform[4];

    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] LayerMask pylonsLayerMask;

    //Extra variables
    private Rigidbody rb;
    private float defaultMultiplier;
    private float multiplier;
    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    // Max velocity and angular velocity
    [SerializeField] float maxVelocity = 10f;
    [SerializeField] float maxAngularVelocity = 5f;

    private void Awake()
    {
        // destroy hoverboard if there are no settings applied
        if (boardSettings == null)
        {
            Debug.Log("The hoverboard settings are empty. Please add them to: " + gameObject.name);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Set references
        rb = GetComponent<Rigidbody>();

        //Get the flight height
        multiplier = boardSettings.flightHeight;
        defaultMultiplier = multiplier;

        //Disable collisions between pylons and the hoverboard
        Physics.IgnoreLayerCollision(gameObject.layer, pylonsLayerMask, true);
    }

    private void Update()
    {
        //Get input while the player is on THIS hoverboard else don't move
        if (PlayerController.instance.onHoverboard && PlayerController.instance.hoverboard == gameObject)
        {
            movementInput = PlayerController.instance.movementInput;
            jumped = PlayerController.instance.Jumped;
        }
        else
        {
            movementInput = Vector2.zero;
        }

        if (multiplier < defaultMultiplier * boardSettings.bounceAmount)
        {
            multiplier += Time.deltaTime * boardSettings.bounceSpeed;
        }
        else
        {
            multiplier = defaultMultiplier;
        }
        if (jumped)
        {
            multiplier += boardSettings.jumpheight;
        }
    }

    private void FixedUpdate()
    {
        //functions to move the hoverboard
        ApplyMovement();
        ApplyRotation();
        ApplyForces();

        // Clamp velocity and angular velocity
        ClampVelocity();
        ClampAngularVelocity();
    }

    void ApplyMovement()
    {
        //apply movement
        Vector3 forwardForce = transform.forward * movementInput.y * boardSettings.speed * rb.mass;
        rb.AddForce(forwardForce);

        //apply torque to rotate the board
        float turnTorque = movementInput.x * boardSettings.turnTorque * rb.mass;
        rb.AddTorque(transform.up * turnTorque);
    }

    void ApplyRotation()
    {
        Vector3 averageNormal = Vector3.zero;
        int hitCount = 0;

        //throw raycasts down from the anchors in the corners of the hoverboard
        foreach (Transform anchor in anchors)
        {
            RaycastHit hit;
            if (Physics.Raycast(anchor.position, -anchor.up, out hit, 1, groundLayerMask))
            {
                averageNormal += hit.normal;
                hitCount++;
            }
        }

        //fancy calculation to rotate the hoverboard so its hovering the correct way
        if (hitCount > 0)
        {
            averageNormal /= hitCount;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, averageNormal) * transform.rotation;
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * boardSettings.levelingSpeed));
        }
    }

    void ApplyForces()
    {
        //keep the hoverboard flying
        foreach (Transform anchor in anchors)
        {
            RaycastHit hit;
            if (Physics.Raycast(anchor.position, -anchor.up, out hit))
            {
                float force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
                rb.AddForceAtPosition(transform.up * force * multiplier, anchor.position, ForceMode.Acceleration);
            }
        }
    }

    // Clamp velocity to prevent spazzing out
    void ClampVelocity()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    // Clamp angular velocity to prevent spazzing out
    void ClampAngularVelocity()
    {
        if (rb.angularVelocity.magnitude > maxAngularVelocity)
        {
            rb.angularVelocity = rb.angularVelocity.normalized * maxAngularVelocity;
        }
    }

}
