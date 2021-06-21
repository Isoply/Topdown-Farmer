using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Player player;

    public bool barrelRange;
    GameObject barrel;

    [HideInInspector] public Crop curCrop;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   



    private void OnTriggerStay2D(Collider2D other)
    {
        
        if ((Input.GetKeyDown(KeyCode.Space)))
        {
            if (other.name.Substring(0, 6) == "Barrel")
            {
                Debug.Log("player hits barrel");
                CheckBarrelType(other.name.Substring(8, (other.name.Length - 8) - 1));
                barrel = other.gameObject;
                barrelRange = true;
            }
        }
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Substring(0, 6) == "Barrel")
        {
            Debug.Log("player is away from barrel");
            barrel = null;
            barrelRange = false;
        }
    }

    void CheckBarrelType(string type)
    {
        foreach (var crop in player.gameManager.crops.allCrops)
        {
            if (crop.item.name == type)
            {
                curCrop = player.gameManager.crops.FindCrop(type);
                print(curCrop.item.name);
                return;
            }
        }
        //laad door alle croptypes als naam zelfde is als type dan geef aan speler
    }


  
}
