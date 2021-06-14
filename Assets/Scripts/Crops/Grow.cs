using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Grow : MonoBehaviour
{

    public GameObject plant;
    public float growSpeed;
    public float size;

    public bool isGrowing;
    // Start is called before the first frame update
    void Start()
    {
        growSpeed = 0.001f;
        size = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrowing)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (size <= 1)
                {
                    size += growSpeed;
                    Debug.Log("Plant is still growing!");
                }
                if (size >= 1)
                {
                    Debug.Log("Plant is done growing!");
                    
                }
            }
            else
            {
                isGrowing = false;
            }
        }
        else
        {
            return;
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        isGrowing = true;
        plant.transform.localScale = new Vector2(size, size);
        Debug.Log("hoi");
    }
}
