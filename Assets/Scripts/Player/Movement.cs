using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed;
    private Rigidbody2D rb2d;
    private Vector2 moveDir;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
    }

    void Move()
    {
        rb2d.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
