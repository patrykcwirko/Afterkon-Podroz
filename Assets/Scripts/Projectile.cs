using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float damage = 25f;

    public Vector2 direction;

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }
}
