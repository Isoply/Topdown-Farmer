using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    Text display;
    Image overlay;

    float time = 15;
    int modifier = 10;

    int[] daytime = { 5, 15 };
    int[] nighttime = { 50, 60};

    // Start is called before the first frame update
    void Start()
    {
        display = GameObject.Find("Time").GetComponent<Text>();
        overlay = GameObject.Find("Overlay").GetComponent<Image>();
        if (time >= 5 && time <= 50) overlay.color = new Color32(255, 100, 0, 255);
        if (time < 5) overlay.color = new Color32(0, 100, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        SetTime();
    }

    string SetTime()
    {
        time += Time.deltaTime * modifier;
        display.text = time.ToString("00");

        if (time >= 5 && time <= 15) overlay.color = new Color32((byte)(255 * ((time - daytime[0]) / (daytime[1] - daytime[0]))), 100, (byte)(255 - (255 * ((time - daytime[0]) / (daytime[1] - daytime[0])))), 255);
        if (time >= 50 && time <= 60) overlay.color = new Color32((byte)(255 - (255 * ((time - nighttime[0]) / (nighttime[1] - nighttime[0])))), 100, (byte)(255 * ((time - nighttime[0]) / (nighttime[1] - nighttime[0]))), 255);

        if (time >= 61) time = 0;
        return null;
    }
}
