using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionEvent/EjectPlatform")]
public class EjectPlatformEvent : ActionEvent
{
    public override void DoAction(GameObject actionObject)
    {
        actionObject.transform.position = new Vector3(actionObject.transform.position.x + 2, actionObject.transform.position.y, actionObject.transform.position.z);
    }
}
