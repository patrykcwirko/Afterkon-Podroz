﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public float timeToChange = 0.05f;

    private Transform player;
    private PlatformEffector2D effector2D;
    private Player.PlayerInput _playerInput;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        effector2D = GetComponent<PlatformEffector2D>();
        _playerInput = player.GetComponent<Player.PlayerInput>();
    }

    private void Update() 
    {
        if(_playerInput.states.downPush) StartCoroutine(FallTime());
    }

    IEnumerator FallTime()
    {
        effector2D.rotationalOffset = 180f;
        yield return new WaitForSeconds(timeToChange);
        effector2D.rotationalOffset = 0;
        _playerInput.states.downPush = false;
    }
}
