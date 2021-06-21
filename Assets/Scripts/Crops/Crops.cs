using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops
{
    //array
    public Crop[] allCrops =
    {
        new Crop(new Item("Wheat", 3), 3),
        new Crop(new Item ("Carrots", 9)),
        new Crop(new Item ("Corn", 12)),
        new Crop(new Item ("Potatoes", 16)),
        new Crop(new Item ("Cauliflower", 20)),
        new Crop(new Item ("Pumpkin", 25)),
    };

    

    public void Awake()
    {
        foreach (var crop in allCrops)
        {
            crop.item.icon = Resources.Load<Sprite>($"icons/{crop.item.name}");
        }
    }

    public Crop FindCrop(string name)
    {
        foreach (var crop in allCrops)
        {
            if (crop.item.name == name) return crop;
        }
        return null;
    }

    public Item[] ToItems()
    {
        List<Item> items = new List<Item>();
        foreach (var crop in allCrops)
        {
            items.Add(crop.item);
        }
        return items.ToArray();
    }
}

public class Crop
{
    public Item item;
    public int maxRange;

    public Crop(Item _item, int _maxRange = 1)
    {
        item = _item;
        maxRange = _maxRange;
    }
}