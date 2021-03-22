﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public Transform player;
    public float timeToChange = 0.05f;

    private PlatformEffector2D effector2D;
    private float _currentTime;
    private Player.PlayerInput _playerInput;

    void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
        _playerInput = player.GetComponent<Player.PlayerInput>();
        _currentTime = timeToChange;
    }

    private void Update() 
    {
        if(_playerInput.states.isStompPushed) StartCoroutine(FallTime());
    }

    IEnumerator FallTime()
    {
        effector2D.rotationalOffset = 180f;
        yield return new WaitForSeconds(0.1f);
        effector2D.rotationalOffset = 0;
    }
}