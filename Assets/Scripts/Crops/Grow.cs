using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Grow : MonoBehaviour
{
    [HideInInspector] public Crop type;
    GameManager gameManager;
    public float growSpeed = 0.18f;
    public float endSize = 1;
    [HideInInspector] public float curSize = 0.1f;
    float overwatered = 0.60f;
    public Sprite[] cropSprites;
    float timer;
    float lastTime;
    bool isGrowing;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isGrowing) Growing();


        if (timer >= overwatered)
        {
            isGrowing = false;
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        }
        else if (timer > 0.15f) isGrowing = true;

        if (lastTime == timer) timer = 0;

    }
    public void Growing()
    {
        if (curSize <= endSize && isGrowing == true)
        {
            curSize += growSpeed * Time.deltaTime;
            transform.localScale = new Vector2(curSize, curSize);
        }
         if(curSize >= (endSize / 2))
         {
            if(type.item.name == "Wheat")
            GetComponent<SpriteRenderer>().sprite = cropSprites[0];
            if (type.item.name == "Potatoes")
            GetComponent<SpriteRenderer>().sprite = cropSprites[3];
            if (type.item.name == "Strawberry")
            GetComponent<SpriteRenderer>().sprite = cropSprites[6];


        }
         if (curSize >= (endSize / 1.50))
         {
            if (type.item.name == "Wheat")
                GetComponent<SpriteRenderer>().sprite = cropSprites[1];
            if (type.item.name == "Potatoes")
                GetComponent<SpriteRenderer>().sprite = cropSprites[4];
            if (type.item.name == "Strawberry")
                GetComponent<SpriteRenderer>().sprite = cropSprites[7];
        }
        if (curSize >= endSize)
        {
            timer = 0;
            isGrowing = false;
            if (type.item.name == "Wheat")
                GetComponent<SpriteRenderer>().sprite = cropSprites[2];
            if (type.item.name == "Potatoes")
                GetComponent<SpriteRenderer>().sprite = cropSprites[5];
            if (type.item.name == "Strawberry")
                GetComponent<SpriteRenderer>().sprite = cropSprites[8];
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        lastTime = timer;

        timer += Time.deltaTime;
    }

}
