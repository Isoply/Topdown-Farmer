using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops
{
    //array
    public Crop[] allCrops =
    {
        new Crop(new Item("Wheat", 3), 5, 2),
        new Crop(new Item ("Strawberries", 9), 4, 3),
        new Crop(new Item ("Corn", 12), 3, 4),
        new Crop(new Item ("Potatoes", 16), 2, 6),
        new Crop(new Item ("Cauliflower", 20), 1, 7),
        new Crop(new Item ("Pumpkin", 25), 0.5f, 8),
    };

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
    public float decayTime = 1;
    public float timer;

    public Crop(Item _item, float _decayTime = 1, int _maxRange = 1)
    {
        item = _item;
        decayTime = _decayTime;
        maxRange = _maxRange;
    }
}