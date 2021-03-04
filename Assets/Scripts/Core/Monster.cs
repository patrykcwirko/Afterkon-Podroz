using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName ="newMonster", menuName ="Enemy/Monster")]
public class Monster : ScriptableObject
{
    [SerializeField] string monsterName;
    [SerializeField] Stats stats;
    [SerializeField] Sprite sprite;
}
