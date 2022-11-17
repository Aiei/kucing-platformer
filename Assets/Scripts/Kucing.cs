using UnityEngine;

public class Kucing : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey("a")){
            sr.flipX = true;
            rb.MovePosition(rb.position + Vector2.left * speed * Time.deltaTime);
            anim.SetFloat("speed", 1.0f);
        }
        else if (Input.GetKey("d")){
            sr.flipX = false;
            rb.MovePosition(rb.position + Vector2.right * speed * Time.deltaTime);
            anim.SetFloat("speed", 1.0f);
        }
        else {
            anim.SetFloat("speed", 0);
        }
    }
}
