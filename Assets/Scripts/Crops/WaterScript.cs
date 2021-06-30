using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    GameManager gameManager;

    ParticleSystem waterParticle;
    [HideInInspector] public bool isGrowing;
    [HideInInspector] public bool isWatering = false;

    int particleAmount;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        waterParticle = GetComponentInChildren<ParticleSystem>();

        particleAmount = waterParticle.maxParticles;
        waterParticle.maxParticles = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !gameManager.isPaused)
        {
            waterParticle.maxParticles = particleAmount;
            isWatering = true;
        }
        else
        {
            waterParticle.maxParticles = 0;
            isWatering = false;
        }

    }
}
