using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Brain : ScriptableObject
{
    public virtual void Initialize() {}
    public abstract Vector3 Think(Transform raycastPoint);
}
