using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float moveSpeed;
    Vector2 moveDir;


    // Update is called once per frame
    void Update()
    {
        Process();
    }
    private void FixedUpdate()
    {
        Move();
    }


    public void Process()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(X, Y).normalized;
    }
    public void Move()
    {
        rb2d.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
