using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySystem : MonoBehaviour
{
    [SerializeField] int numberToDisplay = 100;

    public static event EventHandler onSubstractToZero;

    Text myText;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
        UpdateText();
    }

    private void UpdateText()
    {
        myText.text = numberToDisplay.ToString();
    }

    public void SetNumber(int value)
    {
        numberToDisplay = value;
    }

    public int GetNumber()
    {
        return numberToDisplay;
    }

    public bool HaveEnoughNumber(int amount)
    {
        return numberToDisplay >= amount;
    }

    public void AddAmount(int amount)
    {
        numberToDisplay += amount;
        UpdateText();
    }

    public void SubtractNumberIfCan(int amount)
    {
        if (numberToDisplay >= amount)
        {
            numberToDisplay -= amount;
            UpdateText();
        }
    }
    public void SubtractNumber(int amount)
    {
        numberToDisplay -= amount;
        UpdateText();
        if (numberToDisplay <= 0)
        {
            if (onSubstractToZero != null) onSubstractToZero(this, EventArgs.Empty);
        }
    }

    public void ResetNumber()
    {
        UpdateText();
    }
}
