using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey;

public class HeartsHealthVisual : MonoBehaviour
{
    [SerializeField] GameObject heartObject;

    private List<HeartImage> heartImageList;
    private HeartsHealthSystem heartsHealthSystem;

    private void Awake() {
        heartImageList = new List<HeartImage>();
    }

    private void Start()
    {
        HeartsHealthSystem newHeartsHealthSystem = new HeartsHealthSystem(4);
        SetHeartsHealthSystem(newHeartsHealthSystem);

        heartsHealthSystem.Damage(1f);
        heartsHealthSystem.Damage(1f);
        heartsHealthSystem.Damage(1f);
        heartsHealthSystem.Damage(1f);
    }

    public void SetHeartsHealthSystem(HeartsHealthSystem heartsHealthSystem)
    {
        this.heartsHealthSystem = heartsHealthSystem;

        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
        Vector2 heartAnchorePosition = new Vector2(0,0);
        for (int i = 0; i < heartList.Count; i++)
        {
            HeartsHealthSystem.Heart heart = heartList[i];
            CreateHeartImage(heartAnchorePosition).SetHeartValue(heart.GetValue());
            heartAnchorePosition += new Vector2(44,0);
        }
        heartsHealthSystem.onDamaged += RefreshAllHearts;
        heartsHealthSystem.onHealed += RefreshAllHearts;
        heartsHealthSystem.onDead += DeadMessage;
    }

    public void DeadMessage(object sender, EventArgs e)
    {
        Debug.Log("Dead");
    }

    public void RefreshAllHearts(object sender, EventArgs e)
    {
        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage =  heartImageList[i];
            HeartsHealthSystem.Heart heart = heartsHealthSystem.GetHeartList()[i];
            heartImage.SetHeartValue(heart.GetValue());
        }
    }

    private HeartImage CreateHeartImage(Vector2 anchoredPosition)
    {
        GameObject heartGameObject = Instantiate(heartObject);
        heartGameObject.transform.parent = transform;
        heartGameObject.transform.localPosition = Vector3.zero;
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(10,10); 
        HeartImage heartImage = new HeartImage(heartGameObject);
        heartImageList.Add(heartImage);

        return heartImage;
    }

    public class HeartImage
    { 
        private GameObject herthImage;

        public HeartImage(GameObject hearth)
        {
            this.herthImage = hearth;
        }

        public void SetHeartValue(float value)
        {
            herthImage.transform.Find("Fill").GetComponent<Image>().fillAmount = value * 0.8f;
        }
    }
}
