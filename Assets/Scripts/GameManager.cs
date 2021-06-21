using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public ItemManager itemManager;
    [HideInInspector] public UIManager UIManager;
    [HideInInspector] public Player player;
    [HideInInspector] public Grow grow;
    [HideInInspector] public CreateCrop createCrop;

    [HideInInspector] public Crops crops = new Crops();

    [HideInInspector] public bool isPaused;

    private void Awake()
    {
        itemManager = GetComponent<ItemManager>();
        UIManager = GetComponent<UIManager>();
        player = GameObject.FindObjectOfType<Player>();
        createCrop = GameObject.FindObjectOfType<CreateCrop>();
        crops.Awake();
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
