using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    [CreateAssetMenu(fileName ="newMonster", menuName ="Enemy/Monster")]
    public class MonsterData : ScriptableObject
    {
        [SerializeField] string monsterName;
        [SerializeField] Stats monsterStats;
        [SerializeField] Sprite monsterSprite;


        public string MonsterName() { return monsterName; }
        public Stats Stats() { return monsterStats; }
        public Sprite Sprite() { return monsterSprite; }
    }
}

