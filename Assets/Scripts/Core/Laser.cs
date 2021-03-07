using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] Transform laser;
    [SerializeField] float size;
    [SerializeField] float KnockbackPower = 1f;
    [SerializeField] float KnockbackDuration = 10f;

    private BoxCollider2D _laseCollider;

    void Start()
    {
        _laseCollider = GetComponent<BoxCollider2D>();
        ResizeLaser();
    }

    [ContextMenu("ResizeLaser")]
    private void ResizeLaser()
    {
        laser.localScale = new Vector2(laser.localScale.x, size);
        _laseCollider.offset = new Vector2(0, size / 2);
        _laseCollider.size = new Vector2(1, size);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine( other.gameObject.GetComponent<Player.PlayerMovement>().Knockback(KnockbackDuration, KnockbackPower, this.transform) );
        }
    }
}
