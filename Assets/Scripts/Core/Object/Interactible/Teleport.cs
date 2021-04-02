using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour, Iinteract
{
    public GameObject targetTeleport;

    public void Desactive(Transform player)
    {
        return;
    }

    public void Interact(Transform player)
    {
        player.position = targetTeleport.transform.position;
        player.GetComponent<Player.PlayerInput>().states.interactable = false;
    }

}
