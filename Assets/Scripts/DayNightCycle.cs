using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    GameManager gameManager;

    Text display;
    Image overlay;

    float time = 5;
    int modifier = 1;
    int day = 0;

    int opacity = 35;

    int[] daytime = { 5, 15 };
    int[] nighttime = { 50, 60};

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        display = GameObject.Find("Time").GetComponentInChildren<Text>();
        overlay = GameObject.Find("Overlay").GetComponent<Image>();
        if (time >= 5 && time <= 50) overlay.color = new Color32(255, 100, 0, (byte)opacity);
        if (time < 5) overlay.color = new Color32(0, 100, 255, (byte)opacity);
    }

    // Update is called once per frame
    void Update()
    {
        display.text = SetTime();
    }

    string SetTime()
    {
        time += Time.deltaTime * modifier;

        if (time >= 5 && time <= 15) overlay.color = new Color32((byte)(255 * ((time - daytime[0]) / (daytime[1] - daytime[0]))), 100, (byte)(255 - (255 * ((time - daytime[0]) / (daytime[1] - daytime[0])))), (byte)opacity);
        if (time >= 50 && time <= 60) overlay.color = new Color32((byte)(255 - (255 * ((time - nighttime[0]) / (nighttime[1] - nighttime[0])))), 100, (byte)(255 * ((time - nighttime[0]) / (nighttime[1] - nighttime[0]))), (byte)opacity);

        if (time >= 60)
        {
            time = 0;
            day++;
            gameManager.CheckWinCondition(day);
        }
        return $"{time.ToString("00")} | Day {day}";
    }
}
