using UnityEngine;

public class Kucing : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    public float speed;
    public float jump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Jalan(Vector2.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Jalan(Vector2.right);
        } 
        else
        {
            Diem();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Loncat();
        }
    }

    void Diem()
    {
        anim.SetFloat("speed", 0);
    }

    void Jalan(Vector2 arah)
    {
        if (!GroundCheck())
            return;

        if (arah.x > 0)
            sr.flipX = false;
        else if (arah.x < 0)
            sr.flipX = true;

        rb.MovePosition(rb.position + arah * speed * Time.deltaTime);
        anim.SetFloat("speed", 1.0f);
    }

    void Loncat()
    {
        if (!GroundCheck()) {
            return;
        }

        rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
    }

    bool GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 1f, 1<<7);
        if (hit.collider != null) {
            Debug.Log(hit.collider);
            return true;
        }
        return false;
    }
}
