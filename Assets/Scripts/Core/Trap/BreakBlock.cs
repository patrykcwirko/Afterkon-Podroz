using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    [SerializeField] Sprite[] breakSprite;

    private int currentSpriteIndex = 0;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            if(currentSpriteIndex < breakSprite.Length)
            {
                transform.Find("BreakImage").GetComponent<SpriteRenderer>().sprite = breakSprite[currentSpriteIndex++];
            }
            else
            {
                Destroy(gameObject);
            }
        }    
    }
}
