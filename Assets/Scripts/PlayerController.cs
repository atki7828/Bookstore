using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    float speed = 10f;
    [SerializeField]
    float acceleration = 2f;
    [SerializeField]
    float jump = 10f;

    bool grounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        rb.velocity = Vector2.MoveTowards(rb.velocity,new Vector2(x*speed, rb.velocity.y),acceleration);
        //rb.transform.position += new Vector3(x, 0, 0) * speed * Time.deltaTime;
        //rb.AddForce(new Vector3(x, 0, 0)*speed,ForceMode2D.Impulse);

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            grounded = false;
            rb.AddForce(new Vector3(0, jump, 0), ForceMode2D.Impulse);
            //rb.velocity = new Vector2(rb.velocity.x, jump);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
    }
}
