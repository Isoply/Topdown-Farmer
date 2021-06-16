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
        SetShop(GameObject.Find("Buying"), gameManager.crops.allCrops, 5);
        SetShop(GameObject.Find("Selling"), gameManager.crops.allCrops, -5);
    }

    public void StartAnimationBool(Animator newAnimator)
    {
        animator = newAnimator;

        isPlaying = !isPlaying;
        animator.SetBool("isAnimating", true);
        animator.SetBool("isPlaying", isPlaying);

        if (animator.name == "Shop") gameManager.ChangePauseState(GameManager.PauseStates.Change);
    }

    public void SetShop(GameObject parent, Item[] crops, int modifier)
    {
        GameObject prefab = parent.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        Transform newParent = parent.transform.GetChild(0).GetChild(0);

        int size = 0;

        foreach (var crop in crops)
        {
            GameObject newShopItem = Instantiate(prefab, newParent);
            newShopItem.transform.GetChild(0).GetComponent<Image>().sprite = crop.icon;
            newShopItem.GetComponentInChildren<HorizontalLayoutGroup>().transform.GetChild(0).GetComponent<Text>().text = crop.name;
            newShopItem.GetComponentInChildren<HorizontalLayoutGroup>().transform.GetChild(1).GetComponent<Text>().text = ((int)(crop.price + ((crop.price / 100f) * modifier))).ToString();
            newShopItem.SetActive(true);
            size += 120;
        }

        newParent.GetComponent<RectTransform>().sizeDelta = new Vector2(size, newParent.GetComponent<RectTransform>().sizeDelta.y);
    }
}
