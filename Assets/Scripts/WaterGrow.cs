using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaterGrow : MonoBehaviour
{

    public GameObject plant;
    public float growSpeed;
    public float size;

    public bool isGrowing;

    public Text isDone;

    public GameObject waterParticle;
    // Start is called before the first frame update
    void Start()
    {
        growSpeed = 0.001f;
        size = 0.1f;

        isDone.text = "Plant is still growing!";
        GameObject.Find("PlantText").GetComponent<Text>().color = Color.red;
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
        if (isGrowing)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if(size <= 1)
                {
                    size += growSpeed;
                    isDone.text = "Plant is still growing!";
                    GameObject.Find("PlantText").GetComponent<Text>().color = Color.red;
                }if(size >= 1)
                {
                    isDone.text = "Plant is done growing!";
                    GameObject.Find("PlantText").GetComponent<Text>().color = Color.green;
                }
            }
        }
        else
        {
            return;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrowing = true;
        plant.transform.localScale = new Vector2(size, size);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrowing = false;
    }



}
