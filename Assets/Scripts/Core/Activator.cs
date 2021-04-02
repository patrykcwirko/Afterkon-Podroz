using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public event EventHandler onTriger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriger?.Invoke(this, EventArgs.Empty);
    }
}
