using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    Player player;

    public Text playerFeedback;
    
    

    [HideInInspector] public Crop curCrop;
    Crop hoveredCrop;
    [HideInInspector] public bool soilRange;
    bool barrelRange = false;
    GameObject soil;
    Grow grow;
    public GameObject crop;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PlantCrop();
        if (grow != null && Input.GetKeyDown(KeyCode.R)) HarvestCrop();
        if (Input.GetKey(KeyCode.Space) && barrelRange) TakeFromBarrel();
    }

    public void PlantCrop()
    {
        if (soilRange == true && soil.transform.childCount <= 0 && player.playerControl.curCrop != null)
        {
            GameObject newCrop = Instantiate(crop, soil.transform);
            newCrop.transform.position = soil.transform.position;
            newCrop.GetComponent<Grow>().type = player.playerControl.curCrop;
            
        }
    }

    public void HarvestCrop()
    {
        if (grow.curSize >= grow.endSize && soilRange)
        {
           
            if (Input.GetKeyDown(KeyCode.R))
            {
                player.gameManager.itemManager.ChangeItemAmount(grow.type.item.name, Random.Range(1, player.gameManager.crops.FindCrop(grow.type.item.name).maxRange));
                Destroy(grow.gameObject);
            }
        }
    }

    void TakeFromBarrel()
    {
        curCrop = hoveredCrop;
        Debug.Log($"Picked up {curCrop.item.name}");
    }

    Crop CheckBarrelType(string type)
    {
        foreach (var crop in player.gameManager.crops.allCrops)
        {
            if (crop.item.name == type)
            {
                return player.gameManager.crops.FindCrop(type);
            }
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Grow>()) grow = other.GetComponent<Grow>();
        if (other.name.Substring(0, 4) == "Soil")
        {
            if (grow != null) soilFeedback.SetActive(false);

            else if (curCrop != null) soilFeedback.SetActive(true);
            soil = other.gameObject;
            soilRange = true;
        }
        if (other.name.Length >= 6)
        {
            if (other.name.Substring(0, 6) == "Barrel")
            {
                
                hoveredCrop = CheckBarrelType(other.name.Substring(8, (other.name.Length - 8) - 1));
                barrelRange = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Length >= 6)
        {
            if (other.name.Substring(0, 6) == "Barrel")
            {
                barrelRange = false;
                
            }
        }
        if (other.name.Substring(0, 4) == "Soil")
        {
            if (curCrop != null) soilFeedback.SetActive(false);
            soil = null;
            soilRange = false;
        }
    }
}