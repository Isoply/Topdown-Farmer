using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals
{
    public List<Day> allGoals = new List<Day>
    {
        new Day(1, 5),
        new Day(5, 500),
        new Day(10, 1200),
        new Day(15, 3000),
        new Day(20, 8000),
        new Day(25, 15000),
        new Day(30, 30000),
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
