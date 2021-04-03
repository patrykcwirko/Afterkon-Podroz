using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChIIIBoss : MonoBehaviour
{
    [SerializeField] GameObject spawnAttack;
    [SerializeField] GameObject timeSlider;
    [SerializeField] GameObject projectilePref;
    [SerializeField] Activator activator;
    [SerializeField] float bossTime;
    [SerializeField] float spawnRate;
    [SerializeField] ActionEvent actionEvent;
    [SerializeField] GameObject actionObject;

    private HeartsHealthVisual healthVisual;
    private Slider slider;
    private Transform leftCorner;
    private Transform rightCorner;
    private float currentTime;
    private bool isActive;

    void Start()
    {
        healthVisual = FindObjectOfType<HeartsHealthVisual>();
        slider = timeSlider.GetComponent<Slider>();
        slider.maxValue = bossTime;
        leftCorner = spawnAttack.transform.Find("LeftCorner");
        rightCorner = spawnAttack.transform.Find("RightCorner");
        activator.onTriger += Activator_onTriger;
        healthVisual.onDeath += HealthVisual_onDeath;
    }

    private void HealthVisual_onDeath(object sender, System.EventArgs e)
    {
        isActive = false;
        timeSlider.SetActive(false);
        slider.value = 0;
        currentTime = 0;
    }

    private void Activator_onTriger(object sender, System.EventArgs e)
    {
        isActive = true;
        timeSlider.SetActive(true);
    }

    void Update()
    {
        if (!isActive) return;
        if (slider.value < bossTime)
        {
            slider.value += Time.deltaTime;
            currentTime += Time.deltaTime;
            if(currentTime >= spawnRate)
            {
                currentTime = 0;
                float randomPos = Random.Range(leftCorner.position.x, rightCorner.position.x);
                GameObject projectile = Instantiate(projectilePref, new Vector2(randomPos, leftCorner.position.y), Quaternion.identity);
                projectile.transform.parent = leftCorner.transform;
            }
        }
        else
        {
            foreach (Transform item in leftCorner.transform)
            {
                Destroy(item.gameObject);
            }
            timeSlider.SetActive(false);
            actionEvent.DoAction(actionObject);
        }
    }
}
