using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lewel : MonoBehaviour, Iinteract, IKey
{
    private bool isActive;
    private Transform lewelImage;

    void Start()
    {
       lewelImage = transform.Find("lewelmage");
    }

    public bool CanOpen()
    {
        return isActive;
    }

    public void Desactive(Transform player) {}

    public void Interact(Transform player)
    {
        isActive = !isActive;
        lewelImage.localScale = new Vector2(-lewelImage.localScale.x, lewelImage.localScale.y);
        lewelImage.localPosition = new Vector2(-lewelImage.localPosition.x, lewelImage.localPosition.y);
        player.GetComponent<Player.PlayerInput>().states.interactable = false;
    }
}
