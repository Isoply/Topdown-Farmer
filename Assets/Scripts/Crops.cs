using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops : MonoBehaviour
{
    //array
    Crop[] allCrops =
    {
        new Crop("Wheat", 3),
        new Crop ("Carrots", 9),
        new Crop ("Corn", 12),
        new Crop ("Potatoes", 16),
        new Crop ("Cauliflower", 20),
        new Crop ("Pumpkin", 25),
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

public class Crop
{
    public string name;
    public int price;
    public Sprite icon;
    

    public Crop(string _name, int _price, Sprite _icon = null)
    {
        name = _name;
        price = _price;
        icon = _icon;
        
    }
}
