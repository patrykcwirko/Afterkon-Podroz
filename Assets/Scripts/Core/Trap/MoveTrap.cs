using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap : MonoBehaviour
{
    [SerializeField] private AnimationClip clip;
    [SerializeField] private int tickBetweenChange = 30;
    [SerializeField] private int timeOff = 2;

    private int _lastTimeActivate;
    private int _timeBetweenChange;
    private Animation _animation;

    void Awake()
    {
        _animation = GetComponent<Animation>();
        _animation.AddClip(clip, clip.name);
        _timeBetweenChange = tickBetweenChange;
        TimeTickSystem.onTick += MoveOnTick;
    }

    private void MoveOnTick(object sender, TimeTickSystem.onTickEventArgs e)
    {
        if (e.tick >= _lastTimeActivate + _timeBetweenChange + timeOff)
        {
            _lastTimeActivate = e.tick;
            _animation.Play(clip.name, PlayMode.StopAll);
            _timeBetweenChange = tickBetweenChange;
        }
    }
}
