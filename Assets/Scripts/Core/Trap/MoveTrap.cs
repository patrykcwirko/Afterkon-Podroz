using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap : MonoBehaviour
{
    [SerializeField] private AnimationClip clip;
    [SerializeField] private int tickBetweenChange = 20;
    [SerializeField] private int timeOff = 2;

    private int _lastTimeActivate;
    private Animation _animation;

    void Awake()
    {
        _animation = GetComponent<Animation>();
        _animation.AddClip(clip, "MoveTrap");
        _animation.AddClip(clip, "MoveTrap");
        TimeTickSystem.onTick += MoveOnTick;
    }

    private void MoveOnTick(object sender, TimeTickSystem.onTickEventArgs e)
    {
        if (e.tick >= _lastTimeActivate + tickBetweenChange + timeOff)
        {
            _lastTimeActivate = e.tick;
            _animation.Play("MoveTrap", PlayMode.StopAll);
        }
    }
}
