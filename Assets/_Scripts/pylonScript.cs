using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pylonScript : MonoBehaviour
{
    public float bounceForce = 10f;

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //bounce pylon away
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 horizontalBounceDirection = new Vector3(other.gameObject.transform.forward.x ,0.4f, other.gameObject.transform.right.x).normalized; 

            rb.AddForce(horizontalBounceDirection * bounceForce * 2, ForceMode.Impulse);
        }
    }

    //bounce pylon away
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 horizontalBounceDirection = new Vector3(other.gameObject.transform.forward.x, 0.4f, other.gameObject.transform.right.x).normalized;

            rb.AddForce(horizontalBounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
