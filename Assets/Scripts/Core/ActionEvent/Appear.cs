using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionEvent/Appear")]
public class Appear : ActionEvent
{
    public override void DoAction(GameObject actionObject)
    {
        actionObject.SetActive(true);
    }
}
