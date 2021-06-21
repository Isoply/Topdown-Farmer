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
    }


    

    
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                gameManager.itemManager.ChangeItemAmount(grow.type.item.name, Random.Range(1, gameManager.crops.FindCrop(grow.type.item.name).maxRange));
                Destroy(grow.gameObject);
                Debug.Log($"your amount of {grow.type.item.name}: {gameManager.itemManager.CheckItemAmount(grow.type.item.name)}");
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
