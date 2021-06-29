using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed;
    private Rigidbody2D rb2d;
    private Vector2 moveDir;
    private SpriteRenderer player;
    public Sprite[] playerSprites;
    WaterScript ws;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = this.GetComponent<SpriteRenderer>();
        ws = FindObjectOfType<WaterScript>();
    }

    private void Update()
    {
        Process();
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Process()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(X, Y).normalized;
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.sprite = playerSprites[0];
            if (ws.isWatering == true)
            {
                player.sprite = playerSprites[4];
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            player.sprite = playerSprites[1];
            if (ws.isWatering == true)
            {
                player.sprite = playerSprites[5];
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.sprite = playerSprites[2];
            if (ws.isWatering == true)
            {
                player.sprite = playerSprites[6];
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            player.sprite = playerSprites[3];
            if (ws.isWatering == true)
            {
                player.sprite = playerSprites[7];
            }
        }
        if (player.sprite == playerSprites[0] && ws.isWatering == true)
        {
            player.sprite = playerSprites[4];
        }
        if (player.sprite == playerSprites[1] && ws.isWatering == true)
        {
            player.sprite = playerSprites[5];
        }
        if (player.sprite == playerSprites[2] && ws.isWatering == true)
        {
            player.sprite = playerSprites[6];
        }
        if (player.sprite == playerSprites[3] && ws.isWatering == true)
        {
            player.sprite = playerSprites[7];
        }
    }

    void Move()
    {
        rb2d.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
