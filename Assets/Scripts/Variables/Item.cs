using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string name;
    public int price;
    public Sprite icon;

    public Item(string _name, int _price, Sprite _icon = null)
    {
        name = _name;
        price = _price;
        icon = _icon;
    }
}
