using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;

    bool isPlaying;
    Animator animator;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        SetShop(GameObject.Find("Buying"), gameManager.crops.allCrops);
        SetShop(GameObject.Find("Selling"), gameManager.crops.allCrops);
    }

    public void StartAnimationBool(Animator newAnimator)
    {
        animator = newAnimator;

        isPlaying = !isPlaying;
        animator.SetBool("isAnimating", true);
        animator.SetBool("isPlaying", isPlaying);

        if (animator.name == "Shop") gameManager.ChangePauseState(GameManager.PauseStates.Change);
    }

    public void SetShop(GameObject parent, Crop[] crop)
    {

    }
}
