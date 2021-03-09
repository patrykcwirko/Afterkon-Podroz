using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStat", menuName = "Stats")]
public class Stats :ScriptableObject
{
    public float health;
    public float attack;
    public float defence;
    public float speed;
}
