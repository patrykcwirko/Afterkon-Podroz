using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : MonoBehaviour, IKey
{
    public bool hold;
    public int tickPress = 5;
    public float movePerFrame = 0.04f;

    private const float MAX_MOVE = 0.1f;
    private Vector3 _startPosition;
    private bool isPress = false;
    private bool moveUp = false;
    private bool moveDown = false;
    private int _currentTime;

    private void Start() 
    {  
        _startPosition = transform.position;
        TimeTickSystem.onTick += MoveTimer;    
    }

    private void MoveTimer(object sender, TimeTickSystem.onTickEventArgs e)
    {
        if(moveUp && _currentTime <= tickPress && transform.position.y > _startPosition.y - MAX_MOVE)
        {
            MovePlate(Vector2.down);
            _currentTime++;
        }
        if(moveDown && _currentTime <= tickPress && transform.position.y < _startPosition.y )
        {
            MovePlate(Vector2.up);
            _currentTime++;
        }
    }

    public bool CanOpen()
    {
        return isPress;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {

        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Movable")
        {
            isPress = true;
            moveUp = true;
            moveDown = false;
            _currentTime = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Movable")
        {
            if(hold) isPress = false;
            moveDown = true;
            moveUp = false;
            _currentTime = 0;
        }
    }

    private void MovePlate(Vector2 direction)
    {
        transform.Translate(direction*movePerFrame);
    }

}
