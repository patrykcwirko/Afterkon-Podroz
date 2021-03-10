using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("CheckPoint");
        if (other.gameObject.tag == "Player")
        {
            var gameControler = FindObjectOfType<GameController>();
            gameControler.SetCheckPoint(transform.Find("SpawnPoint").transform);
        }
    }
}
