using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string name;
    public int price;
    public Sprite icon;
    public List<Ingrediant> recipe = new List<Ingrediant>();

    public Item(string _name, int _price = 0)
    {
        name = _name;
        price = _price;
    }

    public Item(string _name, int _price, Ingrediant ingrediant1 = null, Ingrediant ingrediant2 = null, Ingrediant ingrediant3 = null)
    {
        name = _name;
        price = _price;
        if(ingrediant1 != null) recipe.Add(ingrediant1);
        if(ingrediant2 != null) recipe.Add(ingrediant2);
        if(ingrediant3 != null) recipe.Add(ingrediant3);
    }
}

public class Ingrediant
{
    public Item item;
    public int amount;

    public Ingrediant(Item _item, int _amount = 1)
    {
        item = _item;
        if (_amount < 1) amount = 1;
        else amount = _amount;
    }
}

