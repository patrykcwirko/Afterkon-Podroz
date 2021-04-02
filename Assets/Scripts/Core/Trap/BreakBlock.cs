using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    [SerializeField] Sprite[] breakSprite;
    [SerializeField] bool timeDisapire;
    [SerializeField] int time;
    [SerializeField] bool regenerate;
    [SerializeField] int regenTime;

    private bool startTime;
    private bool destroyed;
    private int currentTime;
    private int currentRegenTime;
    private int currentSpriteIndex = 0;

    private void Start() => TimeTickSystem.onTick += TimeTickSystem_onTick;

    private void TimeTickSystem_onTick(object sender, TimeTickSystem.onTickEventArgs e)
    {
        if (startTime)
        {
            currentTime++;
            if(currentTime > (currentTime/time)*currentSpriteIndex)
            {
                ChangeSprite(currentSpriteIndex++);
            }
        }
        if(destroyed)
        {
            currentRegenTime++;
            if(currentRegenTime >= regenTime)
            {
                currentRegenTime = 0;
                currentSpriteIndex = 0;
                currentTime = 0;
                destroyed = false;
                startTime = false;
                this.gameObject.SetActive(true);
                ChangeSprite(0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            if(timeDisapire)
            {
                startTime = true;
            }
            else
            {
                ChangeSprite(currentSpriteIndex++);
            }
        }    
    }

    private void ChangeSprite(int index)
    {
        if (currentSpriteIndex < breakSprite.Length)
            transform.Find("BreakImage").GetComponent<SpriteRenderer>().sprite = breakSprite[index];
        else
        {
            if (regenerate)
            {
                destroyed = true;
                gameObject.SetActive(false);
            }
            else Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        startTime = false;
    }
}
