using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepUpright : MonoBehaviour
{ 
    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);       
    }
}
