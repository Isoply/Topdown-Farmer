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
    float overwatered = 0.35f;
    public Sprite[] cropSprites;
    float timer;
    [HideInInspector] public bool isGrowing;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        transform.localScale = new Vector2(0.15f, 0.15f);
        UpdateSprite(); 
    }
    // Update is called once per frame
    void Update()
    {
        if (isGrowing) Growing();


        if (timer >= overwatered)
        {
            isGrowing = false;
            GetComponent<ParticleSystem>().maxParticles = 0;
            GetComponent<SpriteRenderer>().color = new Color(125, 0, 0, 255);
        }
        else if (timer > 0.15f)
        {
            GetComponent<ParticleSystem>().maxParticles = 1000;
            isGrowing = true;
        }
    }
    public void Growing()
    {
        if (curSize <= endSize && isGrowing == true)
        {
            curSize += growSpeed * Time.deltaTime;
            if (curSize >= endSize)
            {
                GetComponent<ParticleSystem>().startColor = new Color32(0, 255, 0, 255);
                isGrowing = false;
            }

            //transform.localScale = new Vector2(curSize, curSize);
        }
        UpdateSprite();
    }

    void UpdateSprite()
    {
        int startSize = 0;
        if (type.item.name == "Wheat") startSize = 0;
        if (type.item.name == "Strawberries") startSize = 6;
        if (type.item.name == "Corn") startSize = 9;
        if (type.item.name == "Potatoes") startSize = 3;
        if (type.item.name == "Cauliflower") startSize = 12;
        if (type.item.name == "Pumpkins") startSize = 15;

        if (curSize >= endSize) GetComponent<SpriteRenderer>().sprite = cropSprites[startSize + 2];
        else if (curSize >= (endSize / 1.50)) GetComponent<SpriteRenderer>().sprite = cropSprites[startSize + 1];
        else if (curSize >= (endSize / 2)) GetComponent<SpriteRenderer>().sprite = cropSprites[startSize];
        else GetComponent<SpriteRenderer>().sprite = cropSprites[startSize];
    }

    private void OnParticleCollision(GameObject other)
    {
        timer += Time.deltaTime;
    }

}
