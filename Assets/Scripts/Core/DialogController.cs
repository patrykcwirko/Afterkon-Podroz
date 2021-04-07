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
        if (dialog.dialog[index].sprite == null) return;
        if(dialog.dialog[index].side == Dialog.Side.Left)
        {
            dialog.dialog[index].sprite.SetupSprite(dialogWindow.transform.Find("LBody").gameObject);
            //dialog.dialog[index].sprite.SetupA(dialogWindow.transform.Find("RBody").gameObject, Color.clear);
        }
        else
        {
            dialog.dialog[index].sprite.SetupSprite(dialogWindow.transform.Find("RBody").gameObject);
            dialog.dialog[index].sprite.SetupA(dialogWindow.transform.Find("LBody").gameObject, Color.clear);
        }
        dialogWindow.transform.Find("Name").GetComponent<Text>().text = dialog.dialog[index].name;
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
}
