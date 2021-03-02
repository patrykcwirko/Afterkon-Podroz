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
        if(_move.x == 0) myAnimator.SetBool("isRunning", false);

        transform.position += new Vector3(_move.x * speed * Time.deltaTime, 0f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        myAnimator.SetBool("isRunning", true);
        _move = context.ReadValue<Vector2>();
        Debug.Log($"Right move: { _move }");
    }
    
}
