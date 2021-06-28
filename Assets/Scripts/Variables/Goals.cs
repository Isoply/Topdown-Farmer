﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals
{
    public List<Day> allGoals = new List<Day>
    {
        new Day(1, 100),
    };
}

public class Day
{
    public int number;
    public int money;

    public Day(int _dayNumber, int _moneyAmount)
    {
        number = _dayNumber;
        money = _moneyAmount;
    }
}
