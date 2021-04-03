using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionSystemVisual : MonoBehaviour
{
    [SerializeField] private Sprite potionSprite;
    [SerializeField] private PlayerInfo info;

    List<GameObject> potionList;

    private const float OFFSET_POTION = 70f;

    private void Awake() {
        potionList = new List<GameObject>();
        for (int i = 0; i < info.potionCount; i++)
        {
            var offset = potionList.Count * OFFSET_POTION;
            CreatePotionImage(new Vector2(offset, 0));
        }
    }

    private GameObject CreatePotionImage(Vector2 anchoredPosition)
    {
        GameObject potionGameObject = new GameObject("Potion", typeof(Image));
        potionGameObject.transform.parent = transform;
        potionGameObject.GetComponent<Image>().sprite = potionSprite;
        potionGameObject.transform.localPosition = Vector3.zero;
        potionGameObject.transform.localScale = Vector3.one;

        potionGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        potionGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(80,80);

        potionList.Add(potionGameObject);
        return potionGameObject;
    }

    public void UsePotion()
    {
        GameObject potion = potionList[potionList.Count -1];
        info.potionCount--;
        potionList.Remove(potion);
        Destroy(potion);
    }

    public void AddPotion()
    {
        var offset = potionList.Count * OFFSET_POTION;
        info.potionCount++;
        CreatePotionImage(new Vector2(offset,0));
    }

    public void ClearPotionList()
    {
        foreach (var item in potionList)
        {
            Destroy(item);
        }
        potionList.Clear();
    }

}
