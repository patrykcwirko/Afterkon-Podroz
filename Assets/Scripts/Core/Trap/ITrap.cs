using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Trap
{
    public interface ITrap
    {
        float size {get; set;}
        float KnockbackPower { get; set; }
        float KnockbackDuration { get; set; }

        void ResizeTrap();
        void TurnOff();
        void TurnOn();
    }
}

