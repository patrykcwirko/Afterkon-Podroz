using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] public float damage = 25f;
    [SerializeField] LayerMask targetLayer;

    [HideInInspector] public Vector2 direction;

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) Destroy(gameObject);
        if (other.gameObject.layer == (int)Mathf.Log(targetLayer.value, 2)) 
        {
            IEntityController entity = other.gameObject.GetComponent<IEntityController>();
            entity.TakeDamge(damage);
            Destroy(gameObject);
        }
    }
}
