using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public class onActiveEventArgs : EventArgs
    {
        public Collider2D collision;
    }

    public event EventHandler<onActiveEventArgs> onTriger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriger?.Invoke(this, new onActiveEventArgs { collision = collision});
    }
}
