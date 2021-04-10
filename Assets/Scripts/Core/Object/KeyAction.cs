using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAction : MonoBehaviour
{
    [SerializeField] private ActionEvent action;
    [SerializeField] private GameObject key;

    private bool once = true;

    void Update()
    {
        if (!key.GetComponent<IKey>().CanOpen()) return;
        if (!once) return;
        action.DoAction(gameObject);
        once = false;
    }
}
