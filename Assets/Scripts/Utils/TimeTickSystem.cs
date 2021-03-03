using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    public class onTickEventArgs : EventArgs
    {
        public int tick;
    }

    public static event EventHandler<onTickEventArgs> onTick;
    
    private const float TICK_TIME_MAX = .2f;
    private int tick;
    private float tickTimer;

    private void Awake()
    {
        tick = 0;
    }

    private void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIME_MAX)
        {
            tickTimer -= TICK_TIME_MAX;
            tick++;
            if (onTick != null) onTick(this, new onTickEventArgs { tick = tick });
        }
    }
}
