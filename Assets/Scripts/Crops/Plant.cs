using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject soil;
    public GameObject player;
    public Rigidbody2D rb2d;
    public GameObject wheat;
    public bool soilRange;

    
    // Start is called before the first frame update
    void Start()
    {
           
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
        Debug.Log("player hits soil");
        Debug.Log("press  to plant");
        soilRange = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("player out of range");
        soilRange = false;
    }

    public void PlantCrop()
    {
        if (Input.GetKeyDown(KeyCode.E)&& soilRange == true)
        {
            Debug.Log("plant key clicked");
            wheat.transform.SetParent(soil.transform.parent);
            
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
