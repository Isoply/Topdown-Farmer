using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    GameManager gameManager;
    Grow grow;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        Debug.Log("your amount of wheat " + gameManager.itemManager.CheckItemAmount("wheat"));
    }


    /*gebruik de endsize variable uit grow voor checken met harvest
      gebruik de range voor check met harvest
      knop voor harvesten
    */

    
    void Update()
    {
        if (grow != null)
        {
            HarvestCrop();
        }
        
    }

    public void HarvestCrop()
    {
     

        if (grow.curSize >= grow.endSize && gameManager.createCrop.soilRange == true)
        {
            Debug.Log("harvest it ya cunt press R");

            if (Input.GetKeyDown(KeyCode.R))
            {
                gameManager.itemManager.ChangeItemAmount("Wheat", Random.Range(1, 3));
                Destroy(grow.gameObject);
                Debug.Log("your amount of wheat " + gameManager.itemManager.CheckItemAmount("wheat"));
            }
            

        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Grow>())
        {
            grow = other.GetComponent<Grow>();
        }
    }
}
