using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player Info")]
public class PlayerInfo : ScriptableObject
{
    public string name;
    public float health = 100;
    public int potionCount = 0;
}
