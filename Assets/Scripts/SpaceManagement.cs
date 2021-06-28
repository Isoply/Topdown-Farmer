using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManagement : MonoBehaviour
{

    GameManager gameManager;
    List<GameObject> allBarrels = new List<GameObject>();
    List<GameObject> AllSoil = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.LateStart += LateStart;
    }

    void LateStart()
    {
        foreach (var crop in gameManager.crops.allCrops)
        {
            allBarrels.Add(GameObject.Find($"Barrel ({crop.item.name})").gameObject);
            if (crop.item.name != "Wheat") GameObject.Find($"Barrel ({crop.item.name})").SetActive(false);
        }
        AllSoil.Add(GameObject.Find("Row2").gameObject);
        GameObject.Find("Row2").SetActive(false);
        AllSoil.Add(GameObject.Find("Row3").gameObject);
        GameObject.Find("Row3").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var crop in gameManager.crops.allCrops)
        {
            if (gameManager.itemManager.CheckItemAmount(crop.item.name) >= 1 && !GetBarrel(crop.item.name).activeSelf)
            {
                GetBarrel(crop.item.name).SetActive(true);
                gameManager.itemManager.ChangeItemAmount(crop.item.name, -1);
            }
        }
        if (gameManager.itemManager.CheckItemAmount("Row2") >= 1 && !GetSoil("Row2").activeSelf)
        {
            GetSoil("Row2").SetActive(true);
            gameManager.itemManager.ChangeItemAmount("Row2", -1);

        }
        if (gameManager.itemManager.CheckItemAmount("Row3") >= 1 && !GetSoil("Row3").activeSelf)
        {
            GetSoil("Row3").SetActive(true);
            gameManager.itemManager.ChangeItemAmount("Row3", -1);
        }
    }

    GameObject GetBarrel(string _name)
    {
        foreach (var barrel in allBarrels)
        {
            if (barrel.name == $"Barrel ({_name})") return barrel;
        }
        return null;
    }

    GameObject GetSoil(string _name)
    {
        foreach (var soil in AllSoil)
        {
            if (soil.name == _name) return soil;
        }
        return null;
    }

}
