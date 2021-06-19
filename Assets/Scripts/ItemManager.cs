using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    GameManager gameManager;

    List<Slot> allSlots = new List<Slot>();

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        foreach (var crop in gameManager.crops.allCrops)
        {
            allSlots.Add(new Slot(crop.name, 0));
        }
    }

    public void ChangeItemAmount(string itemName, int amount)
    {
        foreach (var slot in allSlots)
        {
            if (slot.name == itemName) slot.amount += amount;
        }
        UpdateDisplay();
    }

    public int CheckItemAmount(string itemName)
    {
        foreach (var slot in allSlots)
        {
            if (slot.name == itemName) return slot.amount;
        }
        return 0;
    } 

    void UpdateDisplay()
    {
        foreach (var slot in allSlots)
        {
            //Update UI amounts
        }
    }
}

class Slot
{
    public string name;
    public int amount;

    public Slot(string _name, int _amount)
    {
        name = _name;
        amount = _amount;
    }
}
