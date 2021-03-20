using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject key;

    private SpriteRenderer _sprite;
    private Color _defaultColor;

    private void Start() 
    {
        _sprite = transform.GetComponent<SpriteRenderer>();
        _defaultColor = _sprite.color;
    }

    void Update()
    {
        Open();
    }

    private void Open()
    {
        if(key.GetComponent<IKey>().CanOpen())
        {
            _sprite.color = new Color(_defaultColor.r, _defaultColor.g, _defaultColor.b, 0.25f);
            transform.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            _sprite.color = _defaultColor;
            transform.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
