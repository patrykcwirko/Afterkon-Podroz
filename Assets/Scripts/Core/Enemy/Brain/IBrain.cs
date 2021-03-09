using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBrain : ScriptableObject
{
    public virtual void Initialize() {}
    public abstract Vector3 Think(Transform raycastPoint);
}
