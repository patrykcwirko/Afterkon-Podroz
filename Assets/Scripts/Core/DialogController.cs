using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private float timeBetweenLethers = 0.1f;

    public IEnumerator WriteText(string input, Text textholder)
    {
        textholder.text = "";
        for (int textIndex = 0; textIndex < input.Length; textIndex++)
        {
            textholder.text += input[textIndex];
            yield return new WaitForSeconds(timeBetweenLethers);
        }
    }
}
