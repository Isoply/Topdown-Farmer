using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public GameManager gameManager;

    [HideInInspector] public PlayerController playerControl;

    [HideInInspector] public int money = 0;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        playerControl = GetComponent<PlayerController>();
    }
}
