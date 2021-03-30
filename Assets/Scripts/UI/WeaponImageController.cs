using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponImageController : MonoBehaviour
{
    [SerializeField] private GameController controller;

    private Image sprite;

    private void Start()
    {
        sprite = transform.Find("WeaponImage").GetComponent<Image>();
    }

    private void Update()
    {
        sprite.sprite = controller.weapons[controller.weaponIndex].weaponSprite;
    }
}
