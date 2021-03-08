using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Trap
{
    public class TrapControler : MonoBehaviour
    {
        [SerializeField] Laser[] lasers;
        [SerializeField] int numberOfLaserTurnOff = 2;
        private float _lastTimeActivate = 0;
        private float _timeBetweenChange = 1;
        private const int MAX_RANDOM_VALUE = 9999;

        private void Start()
        {
            lasers = FindObjectsOfType<Laser>();
        }

        private void Update() 
        {
            // if(Time.time >= _lastTimeActivate + _timeBetweenChange)
            // {
            //     _lastTimeActivate = Time.time;
            //     TurnAllLaserOn();
            //     int FirstRandomIndex = Random.Range(0, MAX_RANDOM_VALUE) % lasers.Length;
            //     int SecondRandomIndex = Random.Range(0, MAX_RANDOM_VALUE)% lasers.Length;
            //     lasers[FirstRandomIndex].TurnOff();
            //     lasers[SecondRandomIndex].TurnOff();
            // }
        }

        private void TurnAllLaserOn()
        {
            foreach (var item in lasers)
            {
                item.TurnOn();
            }
        }
    }
}
