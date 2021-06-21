using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool barrelRange;
    GameObject barrel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Substring(0, 6) == "Barrel")
        {
            CheckBarrelType(other.name.Substring(8, (other.name.Length - 8) - 1));
            Debug.Log("player hits barrel");
            barrel = other.gameObject;
            barrelRange = true;
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
        print(type);
        //laad door alle croptypes als naam zelfde is als type dan geef aan speler
    }


  
}
