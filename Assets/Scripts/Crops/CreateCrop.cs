using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCrop : MonoBehaviour
{
    Player player;

    GameObject soil;
    public GameObject Crop;
    public bool soilRange;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        PlantCrop();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Substring(0, 4) == "Soil")
        {
            Debug.Log("player hits soil");
            Debug.Log("press  to plant");
            soil = other.gameObject;
            soilRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Substring(0, 4) == "Soil")
        {
            Debug.Log("player out of range");
            soil = null;
            soilRange = false;
        }
    }

    public void PlantCrop()
    {
        if (Input.GetKeyDown(KeyCode.E) && soilRange == true && soil.transform.childCount <= 0)
        {
            Debug.Log("plant key clicked");
            //wheat.transform.SetParent(soil.transform.parent);
            GameObject newCrop = Instantiate(Crop, soil.transform);
            newCrop.transform.position = soil.transform.position;
            newCrop.GetComponent<Grow>().type = player.playerControl.curCrop;
        }

        /*
        if (f is pressed && wheat picked)
        {
            Plant what ur holding
        }
        else
        tell player to get a crop
        */
    }
}
