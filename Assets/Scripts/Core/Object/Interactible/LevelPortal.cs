using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour, Iinteract
{
    [SerializeField] int sceneNumber = 0;
    [SerializeField] PlayerInfo info;
    [SerializeField] Player.StrStates states;

    public void Desactive(Transform player) {}

    public void Interact(Transform player)
    {
        info.currentMapIndex = sceneNumber;
        SceneManager.LoadScene(sceneNumber);
        states.interactable = false;
    }


}
