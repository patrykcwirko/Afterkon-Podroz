using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour
{
    [SerializeField] private FlexibleColorPicker colorPicker;
    [SerializeField] private Color color;
    [SerializeField] private TMPro.TMP_InputField input;
    [SerializeField] private PlayerInfo currentInfo;
    [SerializeField] private PlayerInfo defounltInfo;

    public void RandomColor()
    {
        float RandomR = Random.Range(0f,1f);
        float RandomG = Random.Range(0f,1f);
        float RandomB = Random.Range(0f,1f);
        Debug.Log($"R: {RandomR}, G: {RandomG}, B: {RandomB}");
        color = new Color(RandomR, RandomG, RandomB);
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
}
