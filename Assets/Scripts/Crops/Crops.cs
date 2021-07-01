using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops
{
    //array
    public Crop[] allCrops =
    {
        //price gaat x 5 voor buy en -5voor sell, crop decay en max harvest amount
        new Crop(new Item("Wheat", 2), 40, 1),
        new Crop(new Item ("Strawberries", 15 ), 30, 1),
        new Crop(new Item ("Corn", 87), 25, 2),
        new Crop(new Item ("Potatoes", 1050), 20, 2),
        new Crop(new Item ("Cauliflower", 13270), 15, 3),
        new Crop(new Item ("Pumpkins", 16000), 10, 4),
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