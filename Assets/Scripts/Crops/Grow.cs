using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Grow : MonoBehaviour
{

    public GameObject plant;
    public float growSpeed;
    public float size;

    float timer;
    float lastTime;
    public bool isGrowing;
    // Start is called before the first frame update
    void Start()
    {
        growSpeed = 0.18f;
        size = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrowing) Growing();


        if (timer >= 0.75f)
        {
            isGrowing = false;
            Debug.Log("You gave too much water");
        }
        else if (timer > 0.15f) isGrowing = true;

        if (lastTime == timer) timer = 0;

    }
    public void Growing()
    {
        if (size <= 1 && isGrowing == true)
        {
            size += growSpeed * Time.deltaTime;
            plant.transform.localScale = new Vector2(size, size);
            Debug.Log("Plant is still growing!");
        }
        if (size >= 1)
        {
            Debug.Log("Plant is done growing!");
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
