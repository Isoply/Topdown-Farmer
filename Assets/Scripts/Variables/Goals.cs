using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals
{
    public List<Day> allGoals = new List<Day>
    {
        new Day(1, 10),
        new Day(5, 2000),
        new Day(10, 150000),
        new Day(15, 500000),
        new Day(20, 1250000),
        new Day(25, 2000000),
        new Day(30, 5000000),
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
