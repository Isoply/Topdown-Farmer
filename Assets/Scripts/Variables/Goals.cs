using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals
{
    public List<Day> allGoals = new List<Day>
    {
        new Day(1, 0),
        new Day(5, 2000),
        new Day(10, 5000),
        new Day(15, 15000),
        new Day(20, 30000),
        new Day(25, 500000),
        new Day(30, 1000000),
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
