using UnityEngine;

namespace KucingGame
{
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        public float speed;
        public float damage;

        Rigidbody2D _body;
        SpriteRenderer _sprite;
        AudioSource _audio;

        void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _sprite = GetComponent<SpriteRenderer>();
            _audio = GetComponent<AudioSource>();
            if (_audio != null)
                _audio.Play();
        }

        void FixedUpdate()
        {
            _body.velocity = new Vector2(speed, 0);
        }

        public void Flip()
        {
            _sprite.flipX = true;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Character c;
            if (c = collision.gameObject.GetComponent<Character>())
            {
                c.Hit(damage);
            }
        }
    }
}