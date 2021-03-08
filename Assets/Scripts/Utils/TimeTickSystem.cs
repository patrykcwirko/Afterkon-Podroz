using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeTickSystem
{
    public class onTickEventArgs : EventArgs
    {
        public int tick;
    }

    public static event EventHandler<onTickEventArgs> onTick;
    public static event EventHandler<onTickEventArgs> onTick_5;
    
    private const float TICK_TIME_MAX = .2f;

    private static GameObject timeTickSystemGameObject;
    private static int tick;

    public static void Create()
    {
        if(timeTickSystemGameObject == null)
        {
            timeTickSystemGameObject = new GameObject("TimeTickSystem");
            timeTickSystemGameObject.AddComponent<TimeTickSystemObject>();
        }
    }

    public static int GetTick()
    {
        return tick;
    }

    private class TimeTickSystemObject : MonoBehaviour
    {
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
                if(tick % 5 == 0)
                {
                    if (onTick_5 != null) onTick_5(this, new onTickEventArgs { tick = tick });
                }
            }
        }
    }
}
