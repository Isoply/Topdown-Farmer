using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public Crops crops;

    [HideInInspector] public bool isPaused;

    private void Awake()
    {
        crops = GameObject.FindObjectOfType<Crops>();
    }

    public enum PauseStates { Paused, Unpaused, Change };

    public void ChangePauseState(PauseStates pauseState = PauseStates.Change)
    {
        if (pauseState == PauseStates.Change) isPaused = !isPaused;
        else if (pauseState == PauseStates.Paused) isPaused = true;
        else if (pauseState == PauseStates.Unpaused) isPaused = false;
    }
}
