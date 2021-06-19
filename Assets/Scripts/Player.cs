using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public GameManager gameManager;

    [HideInInspector] public int money;

    private void Awake()
    {
        money = 10000;
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
}
