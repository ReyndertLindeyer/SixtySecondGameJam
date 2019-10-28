using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;

    private float xSpeed, ySpeed;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        ySpeed = 0.0f;
        xSpeed = 0.0f;

        if (Input.GetKey(KeyCode.W))
        {
            ySpeed = moveSpeed;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            ySpeed = -moveSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            xSpeed = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            xSpeed = -moveSpeed;
        }

        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + new Vector2(xSpeed, ySpeed) * Time.deltaTime);
    }
}
