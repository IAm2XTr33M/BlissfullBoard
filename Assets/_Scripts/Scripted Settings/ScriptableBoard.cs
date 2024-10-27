using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "hoverboard settings")]
public class ScriptableBoard : ScriptableObject
{
    //scriptable object where you can change the settings for the hoverboards.

    public string name;
    public float speed;
    public float flightHeight;
    public float turnTorque;
    public float jumpheight;
    public float bounceAmount;
    public float bounceSpeed;
    public float levelingSpeed;
    public float maxFlightForce;
}
