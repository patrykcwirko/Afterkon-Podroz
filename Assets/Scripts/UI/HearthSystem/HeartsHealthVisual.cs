using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
=======
//using CodeMonkey;
>>>>>>> master

public class HeartsHealthVisual : MonoBehaviour
{
    [SerializeField] GameObject heartObject;
    [SerializeField] AnimationClip heartFullAnimationClip;

    public event EventHandler onDeath;

    private const float OFFSET = 110;

    private List<HeartImage> heartImageList;
    private HeartsHealthSystem heartsHealthSystem;
    private bool isHealing;

    private void Awake() {
        heartImageList = new List<HeartImage>();
    }

    private void Start()
    {
        HeartsHealthSystem newHeartsHealthSystem = new HeartsHealthSystem(5);
        SetHeartsHealthSystem(newHeartsHealthSystem);

        heartsHealthSystem.Damage(40f);
        heartsHealthSystem.Heal(40f);
    }

    public HeartsHealthSystem GetHeartSystem()
    {
        return heartsHealthSystem;
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
            heartAnchorePosition += new Vector2(OFFSET,0);
        }
        heartsHealthSystem.onDamaged += RefreshAllHearts;
        heartsHealthSystem.onHealed += HeakthSystem_OnHealed;
        heartsHealthSystem.onDead += DeadMessage;
    }

    private void Update() {
        HealingAnimatedPeriodic();
    }


    public void DeadMessage(object sender, EventArgs e)
    {
        Debug.Log("Dead");
        onDeath?.Invoke(this, EventArgs.Empty);
    }

    public void HeakthSystem_OnHealed(object sender, EventArgs e)
    {
        isHealing = true;
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

    private void HealingAnimatedPeriodic()
    {
        if(!isHealing) return;
        bool fullyHealed = true;
        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            HeartsHealthSystem.Heart heart = heartsHealthSystem.GetHeartList()[i];
            if(Math.Round(heartImage.GetValue(), 3) != heart.GetValue())
            {
                heartImage.AddHeartVisualValue();
                if(Math.Round(heartImage.GetValue(), 2) == HeartsHealthSystem.MAX_HEARTH_VALUE)
                {
                    heartImage.PlayHeartFullAnimation();
                }
                fullyHealed = false;
                break;
            }
        }
        if (fullyHealed)
        {
            isHealing = false;
        }
    }

    private HeartImage CreateHeartImage(Vector2 anchoredPosition)
    {
        GameObject heartGameObject = Instantiate(heartObject);
        heartGameObject.transform.parent = transform;
        heartGameObject.transform.localPosition = Vector3.zero;
        heartGameObject.AddComponent<Animation>();
        heartGameObject.GetComponent<Animation>().AddClip(heartFullAnimationClip, "HeartFull");
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1,1); 
        HeartImage heartImage = new HeartImage(heartGameObject, heartGameObject.GetComponent<Animation>());
        heartImageList.Add(heartImage);

        return heartImage;
    }

    public class HeartImage
    { 
        private GameObject heartImage;
        private Animation animation;

        public HeartImage(GameObject hearth, Animation animation)
        {
            this.heartImage = hearth;
            this.animation = animation;
        }

        public void SetHeartValue(float value)
        {
            heartImage.transform.Find("Fill").GetComponent<Image>().fillAmount = value * 0.8f;
        }

        internal void AddHeartVisualValue()
        {
            heartImage.transform.Find("Fill").GetComponent<Image>().fillAmount += 0.01f;
        }

        internal float GetValue()
        {
            return heartImage.transform.Find("Fill").GetComponent<Image>().fillAmount;
        }

        public void PlayHeartFullAnimation()
        {
            animation.Play("HeartFull", PlayMode.StopAll);
        }
    }
}
