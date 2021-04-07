using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "NPC/Image")]
public class CharacterImage : ScriptableObject
{
    public Sprite head;
    public Sprite torso;
    public Sprite legs_L;
    public Sprite legs_R;
    public Sprite legs;
    public Sprite arm_L_T;
    public Sprite arm_L_B;
    public Sprite arm_R_T;
    public Sprite arm_R_B;

    public void SetupSprite(GameObject body)
    {
        body.transform.Find("Head").GetComponent<Image>().sprite = head;
        body.transform.Find("Torso").GetComponent<Image>().sprite = torso;
        body.transform.Find("Arm_L_T").GetComponent<Image>().sprite = arm_L_T;
        body.transform.Find("Arm_L_B").GetComponent<Image>().sprite = arm_L_B;
        body.transform.Find("Arm_R_T").GetComponent<Image>().sprite = arm_R_T;
        body.transform.Find("Arm_R_B").GetComponent<Image>().sprite = arm_R_B;
        if(legs_L == null || legs_R == null)
        {
            body.transform.Find("Legs").GetComponent<Image>().sprite = legs;
            SetupA(body, Color.white);
            return;
        }
        body.transform.Find("Legs_L").GetComponent<Image>().sprite = legs_L;
        body.transform.Find("Legs_R").GetComponent<Image>().sprite = legs_R;
        SetupA(body, Color.white);
        
    }

    public void SetupA(GameObject body, Color color)
    {
        body.transform.Find("Head").GetComponent<Image>().color = color;
        body.transform.Find("Torso").GetComponent<Image>().color = color;
        body.transform.Find("Arm_L_T").GetComponent<Image>().color = color;
        body.transform.Find("Arm_L_B").GetComponent<Image>().color = color;
        body.transform.Find("Arm_R_T").GetComponent<Image>().color = color;
        body.transform.Find("Arm_R_B").GetComponent<Image>().color = color;
        if (legs_L == null || legs_R == null)
        {
            body.transform.Find("Legs").GetComponent<Image>().color = color;
            return;
        }
        body.transform.Find("Legs_L").GetComponent<Image>().color = color;
        body.transform.Find("Legs_R").GetComponent<Image>().color = color;
    }
}
