using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelDrawer : MonoBehaviour
{

    /// <summary>
    /// Unused script that I wanted to create a level with
    /// </summary>

    public GameObject GroundObject;
    public GameObject parentObj;

    public float distanceSensitivity;

    Transform lastObj;

    public bool start = false;
    public float timer = 3;
    public bool ready = false;

    private void OnEnable()
    {
        start = false;timer = 3;ready = false;
    }

    void Update()
    {
        if (start)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                start = false;timer = 0; ready = true;
                lastObj = Instantiate(GroundObject, transform).transform;
                lastObj.parent = parentObj.transform;
            }
        }


        if (ready)
        {
            float distance = Vector3.Distance(lastObj.position, transform.position);

            if (distance > distanceSensitivity)
            {
                lastObj = Instantiate(GroundObject, transform).transform;
                lastObj.parent = parentObj.transform;
            }
        }
    }
}
