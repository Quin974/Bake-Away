using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public static Order instance;
    public Sprite[] spriteArray;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        instance = this;  
    }

    void Start()
    {
        spriteRenderer = this.transform.Find("Order").GetComponent<SpriteRenderer>();
        changeSprite();
    }

    private void changeSprite()
    {
        int spriteIndex = Random.Range(0, spriteArray.Length);
        spriteRenderer.sprite = spriteArray[spriteIndex];
    }

    public string getSpriteName()
    {
        return spriteRenderer.sprite.name;
    }
}
