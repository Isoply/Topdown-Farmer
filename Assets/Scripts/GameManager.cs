using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public DayNightCycle cycle;
    [HideInInspector] public ItemManager itemManager;
    [HideInInspector] public UIManager UIManager;
    [HideInInspector] public Player player;
    [HideInInspector] public Grow grow;

    [HideInInspector] public Crops crops = new Crops();
    [HideInInspector] public Goals goals = new Goals();

    [HideInInspector] public bool isPaused;

    public delegate void Event();
    public Event UpdateShop = delegate { };
    public Event LateStart = delegate { };
    float timer = 0;

    private void Awake()
    {
        cycle = GameObject.FindObjectOfType<DayNightCycle>();
        itemManager = GetComponent<ItemManager>();
        UIManager = GameObject.FindObjectOfType<UIManager>();
        player = GameObject.FindObjectOfType<Player>();
    }

    public void CheckWinCondition(int currentDay)
    {
        foreach (var goal in goals.allGoals)
        {
            if (goal.number == currentDay)
            {
                if (goal.money >= player.money) print("lose");
                return;
            }
        }
    }

    private void Update()
    {
        if (timer <= 0.005f && timer != -1) timer += Time.deltaTime;
        if (timer >= 0.005f)
        {
            LateStart();
            timer = -1;
        }
    }

    public enum PauseStates { Paused, Unpaused, Change };

    public void ChangePauseState(PauseStates pauseState = PauseStates.Change)
    {
        if (pauseState == PauseStates.Change) isPaused = !isPaused;
        else if (pauseState == PauseStates.Paused) isPaused = true;
        else if (pauseState == PauseStates.Unpaused) isPaused = false;
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        //if (UnityEditor.EditorApplication.isPlaying) UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
