using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionEvent/Desapire")]
public class Desapire : ActionEvent
{
    public override void DoAction(GameObject actionObject)
    {
        actionObject.SetActive(false);
    }
}
