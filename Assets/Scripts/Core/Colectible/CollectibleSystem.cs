using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleSystem : MonoBehaviour
{
    [SerializeField] private int maxColect = 10;

    private int currentColect;
    private Image cImage;
    private TMPro.TextMeshProUGUI text;

    void Awake()
    {
        cImage = transform.Find("CIcon").GetComponent<Image>();
        text = transform.Find("CNumber").GetComponent<TMPro.TextMeshProUGUI>();
        text.text = "0 / " + maxColect;
        gameObject.SetActive(false);
    }

    public void SetImage(Sprite image)
    {
        cImage.sprite = image;
    }

    private void UpdateText()
    {
        text.text = currentColect + " / " + maxColect;
    }

    [ContextMenu("CollectItem")]
    public void CollectItem()
    {
        currentColect++;
        if (currentColect != 0) gameObject.SetActive(true);
        UpdateText();
    }
}
