using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/AI/Decision/ActiveState")]
public class ActiveStateDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool chaseTergetIsActive = controller.chaseTarget.gameObject.activeSelf;
        return chaseTergetIsActive;
    }
}
