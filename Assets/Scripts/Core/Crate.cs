using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public bool beingPushed;

    private float xPos;

    void Start()
    {
        xPos = transform.position.x;
    }

    void Update()
    {
        Push();
    }

    private void Push()
    {
        if (!beingPushed)
        {
            transform.GetComponent<FixedJoint2D>().enabled = false;
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }
        else xPos = transform.position.x;
    }
}
