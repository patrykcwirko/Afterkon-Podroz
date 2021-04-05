using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPainter : MonoBehaviour
{
    [SerializeField] private bool isSprite;

    private GameObject hair;
    private GameObject eyes;
    private GameObject skin;
    private GameObject torso;
    private GameObject arms;
    private GameObject legs;
    private GameObject foots;

    void Start()
    {
        hair = transform.Find("Hair").gameObject;
        eyes = transform.Find("Eyes").gameObject;
        skin = transform.Find("Skin").gameObject;
        torso = transform.Find("Torso").gameObject;
        arms = transform.Find("Arms").gameObject;
        legs = transform.Find("Legs").gameObject;
        foots = transform.Find("Foots").gameObject;
    }

    public void SetColor(CharacterColor characterColor)
    {
        if(isSprite)
        {

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
