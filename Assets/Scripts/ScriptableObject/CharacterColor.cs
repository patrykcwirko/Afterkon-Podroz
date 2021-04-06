using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Pllayer Color")]
public class CharacterColor : ScriptableObject
{
    [Serializable]
    public enum BodyPart
    {
        hair,
        eyes,
        skin,
        torso,
        arms,
        legs,
        foots, 
        nothing
    }

    public Color hair;
    public Color eyes;
    public Color skin;
    public Color torso;
    public Color arms;
    public Color legs;
    public Color foots;

    public void SetColor(CharacterColor characterColor)
    {
        hair = characterColor.hair;
        eyes = characterColor.eyes;
        skin = characterColor.skin;
        torso = characterColor.torso;
        arms = characterColor.arms;
        legs = characterColor.legs;
        foots = characterColor.foots;
    }

    public void SetColorPart(BodyPart part, Color color)
    {
        switch (part)
        {
            case BodyPart.hair:
                hair = color;
                break;
            case BodyPart.eyes:
                eyes = color;
                break;
            case BodyPart.skin:
                skin = color;
                break;
            case BodyPart.torso:
                torso = color;
                break;
            case BodyPart.arms:
                arms = color;
                break;
            case BodyPart.legs:
                legs = color;
                break;
            case BodyPart.foots:
                foots = color;
                break;
            default:
                break;
        }
    }
    public Color GetColorPart(BodyPart part)
    {
        switch (part)
        {
            case BodyPart.hair:
                return hair;
            case BodyPart.eyes:
                return eyes;
            case BodyPart.skin:
                return skin;
            case BodyPart.torso:
                return torso;
            case BodyPart.arms:
                return arms;
            case BodyPart.legs:
                return legs;
            case BodyPart.foots:
                return foots;
            default:
                return new Color(0,0,0);
        }
    }

}
