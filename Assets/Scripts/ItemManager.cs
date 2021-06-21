﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    GameManager gameManager;

    public List<Slot> allSlots = new List<Slot>();

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        foreach (var crop in gameManager.crops.allCrops)
        {
            allSlots.Add(new Slot(crop.item.name, 0));
        }
    }

    public void ChangeItemAmount(string itemName, int amount)
    {
        foreach (var slot in allSlots)
        {
            if (slot.name == itemName) slot.amount += amount;
        }
        gameManager.UIManager.UpdateInventory();
    }

    public int CheckItemAmount(string itemName)
    {
        foreach (var slot in allSlots)
        {
            if (slot.name == itemName) return slot.amount;
        }
        return 0;
    } 

    public Slot FindSlot(string name)
    {
        foreach (var slot in allSlots)
        {
            if (slot.name == name) return slot;
        }
        return null;
    }
}

public class Slot
{
    public string name;
    public int amount;

    public Slot(string _name, int _amount)
    {
        name = _name;
        amount = _amount;
    }
}
