using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPainter : MonoBehaviour
{
    [SerializeField] private bool isSprite;

    [SerializeField] private Transform hair;
    [SerializeField] private Transform eyes;
    [SerializeField] private Transform skin;
    [SerializeField] private Transform torso;
    [SerializeField] private Transform arms;
    [SerializeField] private Transform legs;
    [SerializeField] private Transform foots;

    void Start()
    {
        hair = transform.Find("Hair");
        eyes = transform.Find("Eyes");
        skin = transform.Find("Skin");
        torso = transform.Find("Torso");
        arms = transform.Find("Arms");
        legs = transform.Find("Legs");
        foots = transform.Find("Foots");
    }

    public void SetColor(CharacterColor characterColor)
    {
        if(isSprite)
        {
            hair.GetComponent<SpriteRenderer>().color = characterColor.hair;
            eyes.GetComponent<SpriteRenderer>().color = characterColor.eyes;
            skin.GetComponent<SpriteRenderer>().color = characterColor.skin;
            torso.GetComponent<SpriteRenderer>().color = characterColor.torso;
            arms.GetComponent<SpriteRenderer>().color = characterColor.arms;
            legs.GetComponent<SpriteRenderer>().color = characterColor.legs;
            foots.GetComponent<SpriteRenderer>().color = characterColor.foots;
        }
        else
        {
            hair.GetComponent<Image>().color = characterColor.hair;
            eyes.GetComponent<Image>().color = characterColor.eyes;
            skin.GetComponent<Image>().color = characterColor.skin;
            torso.GetComponent<Image>().color = characterColor.torso;
            arms.GetComponent<Image>().color = characterColor.arms;
            legs.GetComponent<Image>().color = characterColor.legs;
            foots.GetComponent<Image>().color = characterColor.foots;
        }
    }
}
