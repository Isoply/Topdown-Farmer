using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    GameManager gameManager;

    public List<Slot> allSlots = new List<Slot>();
    public List<Item> allRecipes = new List<Item>();

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        foreach (var crop in gameManager.crops.allCrops)
        {
            allSlots.Add(new Slot(crop.item.name, crop.item, 0));
        }

        //All recipes
        allRecipes.Add(new Item("Bread", 15, new Ingrediant(GetItem("Wheat"), 3)));

        foreach (var recipe in allRecipes) allSlots.Add(new Slot(recipe.name, recipe));
        UpdateIcons();
        allSlots = SortToPrice();
    }

    public Item[] ToItems(Slot[] slots)
    {
        List<Item> items = new List<Item>();
        foreach (var slot in slots) items.Add(slot.item);
        return items.ToArray();
    }

    List<Slot> SortToPrice()
    {
        List<Slot> newSlots = new List<Slot>();
        newSlots.AddRange(allSlots);
        List<int> allPrices = new List<int>();
        foreach (var slot in allSlots) allPrices.Add(slot.item.price);
        allPrices.Sort();
        foreach (var slot in allSlots)
        {
            for (int i = 0; i < allPrices.Count; i++)
            {
                if (slot.item.price == allPrices[i])
                {
                    newSlots.RemoveAt(i);
                    newSlots.Insert(i, slot);
                }
            }
        }
        return newSlots;
    }

    public void UpdateIcons()
    {
        foreach (var item in allSlots) GetItem(item.name).icon = Resources.Load<Sprite>($"icons/{item.name}");
    }

    public void ChangeItemAmount(string itemName, int amount)
    {
        foreach (var slot in allSlots)
        {
            if (slot.name == itemName) slot.amount += amount;
        }
        gameManager.UIManager.UpdateInventory();
    }

    public int CheckItemAmount(string _name)
    {
        foreach (var slot in allSlots)
        {
            if (slot.name == _name) return slot.amount;
        }
        Debug.LogWarning($"Error! Could not find: {_name} in CheckItemAmount()");
        return 0;
    } 

    public Item GetItem(string _name)
    {
        foreach (var slot in allSlots)
        {
            if (slot.name == _name) return slot.item;
        }
        Debug.LogWarning($"Error! Could not find: {_name} in GetItem()");
        return null;
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
    public Item item;

    public Slot(string _name, Item _item = null, int _amount = 0)
    {
        name = _name;
        item = _item;
        amount = _amount;
    }
}