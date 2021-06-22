using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [HideInInspector]  public GameManager gameManager;

    [HideInInspector] public new string name;
    [HideInInspector] public int amount;
    public enum ItemTypes
    {
        Sell,
        Buy,
        Craft,
    }
    [HideInInspector] public ItemTypes itemType;

    private void Start()
    {
        gameManager.UpdateShop += UpdateButton;
        gameManager.LateStart += LateStart;
    }

    void LateStart()
    {
        UpdateButton();
    }

    public void ClickButton(int itemAmount)
    {
        if (gameManager.player.money >= amount)
        {
            if (itemAmount < 0)
            {
                if (gameManager.itemManager.CheckItemAmount(name) > 0)
                {
                    gameManager.player.money -= amount;
                    gameManager.itemManager.ChangeItemAmount(name, itemAmount);
                }
            }
            else
            {
                gameManager.player.money -= amount;
                gameManager.itemManager.ChangeItemAmount(name, itemAmount);
            }
        }
    }

    void UpdateButton()
    {
        if (itemType == ItemTypes.Buy) if (gameManager.player.money >= amount) GetComponent<Button>().interactable = true;
            else if (itemType == ItemTypes.Buy) GetComponent<Button>().interactable = false;
        if (itemType == ItemTypes.Sell) if (gameManager.itemManager.CheckItemAmount(name) >= 1) GetComponent<Button>().interactable = true;
            else if (itemType == ItemTypes.Sell) GetComponent<Button>().interactable = false;
        //if (itemType == ItemTypes.Craft) if (gameManager.player.money >= amount) GetComponent<Button>().interactable = false;
            //else if (itemType == ItemTypes.Craft) GetComponent<Button>().interactable = false;
        print(itemType);
    }
}
