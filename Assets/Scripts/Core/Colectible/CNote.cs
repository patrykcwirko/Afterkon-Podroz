using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNote : MonoBehaviour
{
    [SerializeField] private CollectibleSystem collectibleSystem;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var sprite = GetComponent<SpriteRenderer>().sprite;
            collectibleSystem.CollectItem();
            collectibleSystem.SetImage(sprite);
            Destroy(gameObject);
        }
    }
}
