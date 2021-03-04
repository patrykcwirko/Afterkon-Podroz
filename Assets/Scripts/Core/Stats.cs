using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStats", menuName = "Enemy/Stats")]
public class Stats : ScriptableObject
{
    [SerializeField] public float health;
    [SerializeField] public float attack;
    [SerializeField] public float defence;
}
