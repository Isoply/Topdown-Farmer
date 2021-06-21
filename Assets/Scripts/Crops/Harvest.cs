using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }


    /*gebruik de endsize variable uit grow voor checken met harvest
      gebruik de range voor check met harvest
      knop voor harvesten
    */

    
    void Update()
    {
        HarvestCrop();
    }

    public void HarvestCrop()
    {
        /*
        if (endsize == 1 && soilrange == true)
        {
            press f to harvest
        }
        

        if (gameManager.grow.curSize >= gameManager.grow.endSize && gameManager.plant.soilRange == true)
        {
            Debug.Log("harvest it ya cunt");

        }
        */
    }
}
