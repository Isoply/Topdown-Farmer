using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tempTesting : MonoBehaviour
{
    Image display;
    Text text;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        display = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
        display.material.color = new Color32(0, 100, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        text.text = timer.ToString("00");

        if (timer >= 5 && timer <= 15) display.color = new Color32((byte)(255 * ((timer - 5) / 10)), 100, (byte)(255 - (255 * ((timer - 5) / 10))), 255);
        if (timer >= 50 && timer <= 60) display.color = new Color32((byte)(255 - (255 * ((timer - 50) / 10))), 100, (byte)(255 * ((timer - 50) / 10)), 255);
        
        if (timer >= 60) timer = 0;
    }
}
