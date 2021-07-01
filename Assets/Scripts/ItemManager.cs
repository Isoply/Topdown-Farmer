using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    GameManager gameManager;

    public List<Slot> allSlots = new List<Slot>();
    public List<Slot> allCrops = new List<Slot>();
    public List<Item> allRecipes = new List<Item>();

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        foreach (var crop in gameManager.crops.allCrops)
        {
            allSlots.Add(new Slot(crop.item.name, crop.item, 0));
        }

        //All recipes
        //recipe name, price, ingredients to make en amount needed to make
        allRecipes.Add(new Item("Bread", 5, new Ingredient(GetItem("Wheat"), 3)));
        allRecipes.Add(new Item("Strawberry Cake", 250, new Ingredient(GetItem("Wheat"), 6), new Ingredient(GetItem("Strawberries"), 4)));
        allRecipes.Add(new Item("Strawberry Juice", 300, new Ingredient(GetItem("Strawberries"), 6)));
        allRecipes.Add(new Item("Pumpkin Soup", 380, new Ingredient(GetItem("Pumpkins"), 2), new Ingredient(GetItem("Corn"), 4)));
        allRecipes.Add(new Item("Stew", 430, new Ingredient(GetItem("Potatoes"), 3), new Ingredient(GetItem("Corn"), 5), new Ingredient(GetItem("Cauliflower"), 3)));
        allRecipes.Add(new Item("Veggie Plate", 460, new Ingredient(GetItem("Potatoes"), 3), new Ingredient(GetItem("Pumpkins"), 2), new Ingredient(GetItem("Cauliflower"), 4)));

        allRecipes.Add(new Item("Row2", 800000, new Ingredient(GetItem("Strawberries"), 6), new Ingredient(GetItem("Corn"), 6)));
        allRecipes.Add(new Item("Row3", 800000, new Ingredient(GetItem("Potatoes"), 6), new Ingredient(GetItem("Cauliflower"), 6)));




        foreach (var recipe in allRecipes) allSlots.Add(new Slot(recipe.name, recipe));
        UpdateIcons();
        allSlots = SortToPrice();
        allCrops = RemoveSlots(allSlots, allRecipes);
    }

    void Update()
    {
        foreach (var crop in gameManager.crops.allCrops)
        {
            UpdateCropDecayTimer(crop);
        }
    }

    void UpdateCropDecayTimer(Crop crop)
    {
        if (CheckItemAmount(crop.item.name) > 0 && !gameManager.isPaused)
        {
            crop.timer += Time.deltaTime;
            if (crop.timer >= crop.decayTime)
            {
                ChangeItemAmount(crop.item.name, -1);
                crop.timer = 0;
            }

            foreach (var inventory in gameManager.UIManager.invSlots[1].allSlots)
            {
                if (inventory.name == crop.item.name) inventory.timer.text = (crop.decayTime - crop.timer).ToString("0.00");
            }
        }
    }

    public void ResetCropDecayTimer(Crop crop)
    {
        crop.timer = 0;
        foreach (var inventory in gameManager.UIManager.invSlots[1].allSlots)
        {
            if (inventory.name == crop.item.name) inventory.timer.text = (crop.decayTime - crop.timer).ToString("0.00");
        }
    }

    Slot GetSlot(string _name)
    {
        foreach (var slot in allSlots)
        {
            if (slot.name == _name) return slot;
        }
        return null;
    }

    List<Slot> RemoveSlots(List<Slot> primary, List<Item> secondary)
    {
        List<Slot> newSlots = new List<Slot>();
        foreach (var primSlot in primary)
        {
            bool exists = false;
            foreach (var secSlot in secondary) if (primSlot.item.name == secSlot.name) exists = true;
            if (!exists) newSlots.Add(primSlot);
        }
        return newSlots;
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
                    newSlots.Remove(slot);
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