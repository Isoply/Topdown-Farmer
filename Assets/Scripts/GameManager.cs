using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;

    [HideInInspector] public DayNightCycle cycle;
    [HideInInspector] public ItemManager itemManager;
    [HideInInspector] public UIManager UIManager;
    [HideInInspector] public Player player;
    [HideInInspector] public Grow grow;

    [HideInInspector] public Crops crops = new Crops();
    [HideInInspector] public Goals goals = new Goals();

    Text goalText;

    [HideInInspector] public bool isPaused;

    public delegate void Event();
    public Event UpdateShop = delegate { };
    public Event LateStart = delegate { };
    float timer = 0;

    private void Awake()
    {
        if(GameObject.Find("Goal")) goalText = GameObject.Find("Goal").GetComponentInChildren<Text>();
        if(goalText != null) goalText.text = FindNextGoal(0);
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
                if (goal.money >= player.money && loseScreen != null)
                {
                    loseScreen.SetActive(true);
                    ChangePauseState(PauseStates.Paused);
                }
                else
                {
                    if (FindNextGoal(currentDay) != null) goalText.text = FindNextGoal(currentDay);
                    else if (winScreen != null)
                    {
                        winScreen.SetActive(true);
                        ChangePauseState(PauseStates.Paused);
                    }
                }
                return;
            }
        }
    }

    string FindNextGoal(int currentDay)
    {
        foreach (var goal in goals.allGoals)
        {
            if (goal.number > currentDay) return $"Goal: Day {goal.number} - {goal.money}$";
        }
        return null;
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
