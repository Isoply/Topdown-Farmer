using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;

    bool isPlaying;
    Animator animator;
    List<Item> buyItems = new List<Item>();
    List<Item> sellItems = new List<Item>();

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

    public void SetShop(GameObject parent, Item[] items, int modifier)
    {
        GameObject prefab = parent.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        Transform newParent = parent.transform.GetChild(0).GetChild(0);

        int size = 0;

        Item[] usedArray = null;
        bool skips;
        if (modifier >= 0) usedArray = buyItems.ToArray();
        else if (modifier < 0) usedArray = sellItems.ToArray();
        foreach (var item in items)
        {
            skips = false;
            foreach (var curItem in usedArray) if (curItem.name == item.name) skips = true;
            if (skips) continue;
            GameObject newShopItem = Instantiate(prefab, newParent);
            newShopItem.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
            newShopItem.GetComponentInChildren<HorizontalLayoutGroup>().transform.GetChild(0).GetComponent<Text>().text = item.name;
            newShopItem.GetComponentInChildren<HorizontalLayoutGroup>().transform.GetChild(1).GetComponent<Text>().text = ((int)(item.price + ((item.price / 100f) * modifier))).ToString();
            newShopItem.SetActive(true);
            if (modifier >= 0) buyItems.Add(item);
            else if (modifier < 0) sellItems.Add(item);
            size += 120;
        }

        newParent.GetComponent<RectTransform>().sizeDelta = new Vector2(size, newParent.GetComponent<RectTransform>().sizeDelta.y);
    }
}
