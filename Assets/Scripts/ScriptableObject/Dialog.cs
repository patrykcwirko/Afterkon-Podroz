using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog")]
public class Dialog : ScriptableObject
{
    public enum Side
    {
        Left,
        Right
    }

    [Serializable]
    public class DialogStr
    {
        [SerializeField] public Side side;
        [SerializeField] public CharacterImage sprite;
        [SerializeField] public string name;
        [TextArea(2,10)][SerializeField] public string text;

        public CharacterImage GetCharacterSprite() { return sprite; }
        public Side GetSide() { return side; }
        public string GetName() { return name; }
        public string GetText() { return text; }
    }

    public List<DialogStr> dialog;
}
