using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    Player player;

    Text playerFeedback;
    
    [HideInInspector] public Crop curCrop;
    Crop hoveredCrop;

    Feedback[] allFeedback =
    {
        new Feedback("soil", "E"),
        new Feedback("barrel", "E"),
        new Feedback("Crop", "Q"),
        new Feedback("Shop", "E"),
        new Feedback("Crafting", "E"),
    };

    GameObject soil;
    Grow grow;
    public GameObject crop;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        playerFeedback = GameObject.Find("PlayerFeedback").GetComponent<Text>();
        playerFeedback.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PlantCrop();
        if (grow != null && Input.GetKeyDown(KeyCode.Q)) HarvestCrop();
        if (Input.GetKey(KeyCode.E) && GetFeedback("Barrel").range) TakeFromBarrel();
        if (Input.GetKeyDown(KeyCode.E) && GetFeedback("Shop").range) player.gameManager.UIManager.StartAnimationBool(GameObject.Find("HUD").transform.Find("Shop").GetComponent<Animator>());
        if (Input.GetKeyDown(KeyCode.E) && GetFeedback("Crafting").range) player.gameManager.UIManager.StartAnimationBool(GameObject.Find("HUD").transform.Find("Crafting").GetComponent<Animator>());
        if (playerFeedback.text != "")
        {
            playerFeedback.transform.position = player.transform.position + new Vector3(0,1,0) + GameObject.FindObjectOfType<Canvas>().transform.position;
        }
        if (grow != null) UpdateCropText();
    }

    void UpdateCropText()
    {
        if (grow != null && grow.curSize >= grow.endSize) playerFeedback.text = GetFeedback("Crop").text;
        else if (grow != null && !grow.isGrowing) playerFeedback.text = GetFeedback("Crop").text;
        else if (grow != null && grow.isGrowing) playerFeedback.text = "";
        
    }

    public void PlantCrop()
    {
        if (GetFeedback("Soil").range == true && soil.transform.childCount <= 0 && player.playerControl.curCrop != null)
        {
            GameObject newCrop = Instantiate(crop, soil.transform);
            newCrop.transform.position = soil.transform.position;
            newCrop.GetComponent<Grow>().type = player.playerControl.curCrop;
        }
    }

    public void HarvestCrop()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (grow.curSize >= grow.endSize && crop != null)
            {
                player.gameManager.itemManager.ChangeItemAmount(grow.type.item.name, Random.Range(1, player.gameManager.crops.FindCrop(grow.type.item.name).maxRange));
                Destroy(grow.gameObject);
            }
            else if (crop != null && !grow.isGrowing) Destroy(grow.gameObject);
        }
    }

    void TakeFromBarrel()
    {
        curCrop = hoveredCrop;
        GameObject.Find("CurrentItem").GetComponent<Image>().sprite = curCrop.item.icon;
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

        if (other.name.Length >= 4)
        {
            if (other.name.Substring(0, 4) == "Soil")
            {
                if (other.transform.childCount <= 0)
                {
                    if (curCrop != null) playerFeedback.text = GetFeedback("Soil").text;
                    soil = other.gameObject;
                    GetFeedback("Soil").range = true;
                }
            }
        }
        if (other.name.Length >= 6)
        {
            if (other.name.Substring(0, 6) == "Barrel")
            {
                playerFeedback.text = GetFeedback("Barrel").text;
                hoveredCrop = CheckBarrelType(other.name.Substring(8, (other.name.Length - 8) - 1));
                GetFeedback("Barrel").range = true;
            }
        }
        if (other.name == "Shop")
        {
            playerFeedback.text = GetFeedback("Shop").text;
            GetFeedback("Shop").range = true;
        }
        if (other.name == "Crafting")
        {
            playerFeedback.text = GetFeedback("Crafting").text;
            GetFeedback("Crafting").range = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Grow>())
        {
            grow = null;
            playerFeedback.text = "";
        }
        if (other.name.Length >= 6)
        {
            if (other.name.Substring(0, 6) == "Barrel")
            {
                playerFeedback.text = "";
                GetFeedback("Barrel").range = false;
            }
        }
        if (other.name.Substring(0, 4) == "Soil")
        {
            if (curCrop != null) playerFeedback.text = "";
            soil = null;
            GetFeedback("Soil").range = false;
        }
        if (other.name == "Shop")
        {
            playerFeedback.text = "";
            GetFeedback("Shop").range = false;
        }
        if (other.name == "Crafting")
        {
            playerFeedback.text = "";
            GetFeedback("Crafting").range = false;
        }
    }

    Feedback GetFeedback(string _name)
    {
        foreach (var feedback in allFeedback) if (feedback.name.ToLower() == _name.ToLower()) return feedback;
        return null;
    }
}

public class Feedback
{
    public string name;
    public string text;
    public bool range;

    public Feedback(string _name, string _text, bool _range = false)
    {
        name = _name;
        text = _text;
        range = _range;
    }
}