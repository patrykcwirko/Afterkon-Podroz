using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public class OnDestroyObjectArgs : EventArgs
    {
        public Collision2D collision;
    }

    public static event EventHandler<OnDestroyObjectArgs> OnDestroyObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Debug.Log("Destroy collider");
        OnDestroyObject?.Invoke(this, new OnDestroyObjectArgs { collision = collision });
    }
}
