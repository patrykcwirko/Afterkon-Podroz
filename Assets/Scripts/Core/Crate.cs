using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, Iinteract
{
    public bool beingPushed;

    private float xPos;
    private GameObject _crate;

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

    public void Interact(Transform player)
    {
            this.GetComponent<FixedJoint2D>().enabled = true;
            this.GetComponent<Crate>().beingPushed = true;
            this.GetComponent<FixedJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
    }

    public void Desactive(Transform player)
    {
        this.GetComponent<FixedJoint2D>().enabled = false;
        this.GetComponent<Crate>().beingPushed = false;
    }
}
