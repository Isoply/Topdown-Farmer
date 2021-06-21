using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [HideInInspector]  public GameManager gameManager;

    [HideInInspector] public new string name;
    [HideInInspector] public int amount;

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
}
