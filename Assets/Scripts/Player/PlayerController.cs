using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Player player;

    public bool barrelRange;

    [HideInInspector] public Crop curCrop;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name.Length >= 6)
        {
            if ((Input.GetKey(KeyCode.Space)))
            {
                if (other.name.Substring(0, 6) == "Barrel")
                {
                    Debug.Log($"Picked up {other.name.Substring(8, (other.name.Length - 8) - 1)}");
                    CheckBarrelType(other.name.Substring(8, (other.name.Length - 8) - 1));
                    barrelRange = true;
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Length >= 6)
        {
            if (other.name.Substring(0, 6) == "Barrel") barrelRange = false;
        }
    }

    void CheckBarrelType(string type)
    {
        foreach (var crop in player.gameManager.crops.allCrops)
        {
            if (crop.item.name == type)
            {
                curCrop = player.gameManager.crops.FindCrop(type);
                return;
            }
        }
    }
}