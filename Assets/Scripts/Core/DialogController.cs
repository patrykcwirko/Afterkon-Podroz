using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] public GameObject dialogWindow;
    [SerializeField] public Dialog dialog;
    [SerializeField] private float timeBetweenLethers = 0.1f;
    [SerializeField] private PlayerInfo info;

    private const string PLAYER_NAME = "<name>";

    public void Setup(int index)
    {
        if (dialog.dialog[index].sprite == null)
        {
            SetupA(dialogWindow.transform.Find("RBody").gameObject, Color.clear);
            SetupA(dialogWindow.transform.Find("LBody").gameObject, Color.clear);
            return;
        }
        if(dialog.dialog[index].side == Dialog.Side.Left)
        {
            dialog.dialog[index].sprite.SetupA(dialogWindow.transform.Find("RBody").gameObject, Color.clear);
            dialog.dialog[index].sprite.SetupSprite(dialogWindow.transform.Find("LBody").gameObject);
        }
        else
        {
            dialog.dialog[index].sprite.SetupA(dialogWindow.transform.Find("LBody").gameObject, Color.clear);
            dialog.dialog[index].sprite.SetupSprite(dialogWindow.transform.Find("RBody").gameObject);
        }
        dialogWindow.transform.Find("Name").GetComponent<Text>().text = dialog.dialog[index].name.Replace(PLAYER_NAME, info.name);
    }

    public IEnumerator WriteText(int index, Text textholder)
    {
        textholder.text = "";
        string text = dialog.dialog[index].text.Replace(PLAYER_NAME, info.name);
        Setup(index);
        for (int textIndex = 0; textIndex < text.Length; textIndex++)
        {
            textholder.text += text[textIndex];
            yield return new WaitForSeconds(timeBetweenLethers);
        }
    }

    [ContextMenu("WriteSampleText")]
    public void WriteSampleText()
    {
        WriteText(0, dialogWindow.transform.Find("Text").GetComponent<Text>());
    }

    public void SetupA(GameObject body, Color color)
    {
        body.transform.Find("Head").GetComponent<Image>().color = color;
        body.transform.Find("Torso").GetComponent<Image>().color = color;
        body.transform.Find("Arm_L_T").GetComponent<Image>().color = color;
        body.transform.Find("Arm_L_B").GetComponent<Image>().color = color;
        body.transform.Find("Arm_R_T").GetComponent<Image>().color = color;
        body.transform.Find("Arm_R_B").GetComponent<Image>().color = color;
        body.transform.Find("Legs").GetComponent<Image>().color = color;
    }
}
