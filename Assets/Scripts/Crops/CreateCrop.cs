using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCrop : MonoBehaviour
{
    Player player;

    GameObject soil;
    public GameObject crop;
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
            soil = other.gameObject;
            soilRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Substring(0, 4) == "Soil")
        {
            soil = null;
            soilRange = false;
        }
    }

    public void PlantCrop()
    {
        if (Input.GetKeyDown(KeyCode.E) && soilRange == true && soil.transform.childCount <= 0 && player.playerControl.curCrop != null)
        {
            GameObject newCrop = Instantiate(crop, soil.transform);
            newCrop.transform.position = soil.transform.position;
            newCrop.GetComponent<Grow>().type = player.playerControl.curCrop;
        }
    }
}
