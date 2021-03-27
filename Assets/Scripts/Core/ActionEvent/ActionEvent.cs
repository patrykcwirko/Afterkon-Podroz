using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionEvent : ScriptableObject
{
    public abstract void DoAction(GameObject actionObject);
}
