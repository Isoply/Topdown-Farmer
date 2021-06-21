using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Grow : MonoBehaviour
{
    [HideInInspector] public Crop type;
    public float growSpeed = 0.18f;
    public float endSize = 1;
    [HideInInspector] public float curSize = 0.1f;
    float overwatered = 0.75f;

    float timer;
    float lastTime;
    bool isGrowing;

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
        if (curSize >= endSize)
        {
            timer = 0;
            isGrowing = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        lastTime = timer;

        timer += Time.deltaTime;
    }

}
