using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap : MonoBehaviour
{
    [SerializeField] private AnimationClip moveClip;
    [SerializeField] private int tickBetweenChange = 30;
    [SerializeField] private int timeOff = 2;
    [Header("Eject")]
    [SerializeField] private AnimationClip ejectClip;
    [SerializeField] private int timeEjected = 0;
    [SerializeField] private AnimationClip hideClip;

    private int _lastTimeActivate;
    private int _timeBetweenChange;
    private Animation _animation;
    private bool once = true;
    private int currentTime;

    void Awake()
    {
        _animation = GetComponent<Animation>();
        if (moveClip)
        {
            _animation.AddClip(moveClip, moveClip.name);
            _timeBetweenChange = tickBetweenChange;
            if (moveClip) TimeTickSystem.onTick += MoveOnTick;
        }
        if (ejectClip)  _animation.AddClip(ejectClip, ejectClip.name);
        if (hideClip)  _animation.AddClip(hideClip, hideClip.name);
        if (timeEjected > 0) TimeTickSystem.onTick += HidePlatform;
    }

    private void HidePlatform(object sender, TimeTickSystem.onTickEventArgs e)
    {
        if(currentTime < timeEjected)
        {
            currentTime++;
            return;
        }
        else
        {
            if(!once)
            {
                _animation.Play(hideClip.name, PlayMode.StopAll);
                once = true;
            }
        }
    }

    private void MoveOnTick(object sender, TimeTickSystem.onTickEventArgs e)
    {
        if (e.tick >= _lastTimeActivate + _timeBetweenChange + timeOff)
        {
            _lastTimeActivate = e.tick;
            _animation.Play(moveClip.name, PlayMode.StopAll);
            _timeBetweenChange = tickBetweenChange;
        }
    }

    public void EjectPlatform()
    {
        currentTime = 0;
        if(once)
        {
            _animation.Play(ejectClip.name, PlayMode.StopAll);
            once = false;
        }
    }
}
