using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    GameObject body;
    Animator myAnimator;


    private Vector2 _move;

    // Start is called before the first frame update
    void Start()
    {
        body = transform.Find("Body").gameObject;
        myAnimator = body.GetComponent<Animator>();

    }

    private void Update()
    {
        ChangeAnimation();
        ChangeOrintation();

        transform.position += new Vector3(_move.x * speed * Time.deltaTime, 0f);
    }

    private void ChangeOrintation()
    {
        if (_move.x < 0) transform.localScale = new Vector3(-1f, 1f);
        if (_move.x > 0) transform.localScale = new Vector3( 1f, 1f);
    }

    private void ChangeAnimation()
    {
        if (_move.x == 0) myAnimator.SetBool("isRunning", false);
        else myAnimator.SetBool("isRunning", true);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }
    
}
