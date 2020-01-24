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
    Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
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

        cam.transform.position = new Vector3(transform.position.x,transform.position.y,cam.transform.position.z);

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        foreach(ContactPoint2D c in other.contacts) {
            Debug.Log(c.point);
            if(c.point.y < this.transform.position.y) {
                grounded = true;
                break;
            }
        }
    }
}
