using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public Crops crops = new Crops();

    [HideInInspector] public bool isPaused;

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
        if (UnityEditor.EditorApplication.isPlaying) UnityEditor.EditorApplication.isPlaying = false;
        else Application.Quit();
    }
}
