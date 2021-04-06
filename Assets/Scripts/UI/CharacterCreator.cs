using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour
{
    [SerializeField] private FlexibleColorPicker colorPicker;
    [SerializeField] private TMPro.TMP_InputField input;
    [SerializeField] private PlayerInfo currentInfo;
    [SerializeField] private PlayerInfo defounltInfo;
    [SerializeField] private CharacterColor startColor;
    [SerializeField] private CharacterColor currentColor;
    [SerializeField] private CharacterPainter painter;
    
    private CharacterColor.BodyPart partIndex;

    private void Start()
    {
        painter.SetColor(startColor);
        currentColor.SetColor(startColor);
    }

    private void Update()
    {
        if (partIndex != CharacterColor.BodyPart.nothing)
        {
            currentColor.SetColorPart(partIndex, colorPicker.color);
            painter.SetColor(currentColor);
        }
    }

    public void RandomColor()
    {
        float RandomR = Random.Range(0f,1f);
        float RandomG = Random.Range(0f,1f);
        float RandomB = Random.Range(0f,1f);
        Debug.Log($"R: {RandomR}, G: {RandomG}, B: {RandomB}");
        var color = new Color(RandomR, RandomG, RandomB);
        colorPicker.color = color;
    }

    public void ChangeToMale()
    {
        Debug.Log("To Male");
    }
    public void ChangeToFamale()
    {
        Debug.Log("To Famale");
    }

    public void StartGame()
    {
        currentInfo.health = defounltInfo.health;
        currentInfo.potionCount = defounltInfo.potionCount;
        currentInfo.name = input.text;
        SceneManager.LoadScene(1);
    }

    public void ChoosePart(int index)
    {
        partIndex = (CharacterColor.BodyPart)index;
        colorPicker.color = currentColor.GetColorPart(partIndex);
    }
}
