using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops : MonoBehaviour
{
    //array
    public Item[] allCrops =
    {
        new Item("Wheat", 3),
        new Item ("Carrots", 9),
        new Item ("Corn", 12),
        new Item ("Potatoes", 16),
        new Item ("Cauliflower", 20),
        new Item ("Pumpkin", 25),
    };
    
    void Start()
    {
        foreach (var crop in allCrops)
        {
            crop.icon = Resources.Load<Sprite>($"icons/{crop.name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}