using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    GameObject soil;
    GameObject player;
    Rigidbody2D rb2d;
    public GameObject wheat;
    bool soilRange;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Movement>().gameObject;
        rb2d = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlantCrop();
    }

    public void SoilCheck()
    {
        

        /*if (player = in range of ground)
        {
            show f
                if f pressed
                    plant
        }
        */
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
            Instantiate(wheat, soil.transform).transform.position = soil.transform.position;
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
