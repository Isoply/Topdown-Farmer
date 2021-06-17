using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{

    public bool isGrowing;


    public GameObject waterParticle;
    // Start is called before the first frame update
    void Start()
    {
        waterParticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            waterParticle.SetActive(true);
        }
        else
        {
            waterParticle.SetActive(false);
        }

    }
}
