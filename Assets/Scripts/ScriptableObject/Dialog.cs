using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog")]
public class Dialog : ScriptableObject
{
    [Serializable]
    public class DialogStr
    {
        [SerializeField] public string name;
        [SerializeField] public string text;

        public string GetName() { return name; }
        public string GetText() { return text; }
    }

    public DialogStr[] dialog;
}
