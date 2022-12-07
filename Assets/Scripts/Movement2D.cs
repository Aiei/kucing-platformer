using UnityEngine;

namespace KucingGame
{
    /// <summary>
    /// Kelas untuk pergerakan sederhana 2d platformer.
    /// </summary>
    public class Movement2D : MonoBehaviour
    {
        Rigidbody2D _body;
        SpriteRenderer _sprite;
        Animator _animator;
        AudioSource _audio;

        /// <summary>
        /// Kecepatan berjalan
        /// </summary>
        public float speed;

        /// <summary>
        /// Kekuatan loncat
        /// </summary>
        public float jump;

        /// <summary>
        /// Layer yang digunakan untuk membedakan mana yg tanah
        /// </summary>
        public LayerMask groundLayer;

        /// <summary>
        /// Status menginjak tanah
        /// </summary>
        bool grounded;

        /// <summary>
        /// Apakah karakter bisa loncat ketika msh di udara
        /// </summary>
        public bool allowAirMovement = false;

        public AudioClip[] stepSound;

        float lastStep = 0;
        public float stepSpeed;
        int lastStepId = 0;

        void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _sprite = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _audio = GetComponent<AudioSource>();
        }

        void Update()
        {
            float direction = 0;
            if (Input.GetKey(KeyCode.A))
            {
                direction -= 1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += 1f;
            }
            // Memastikan nilai direction di rang -1 sampai 1
            direction = Mathf.Clamp(direction, -1f, 1f);
            // Menentukan arah hadap sprite berdasarkan arah input
            if (direction > 0.01f)
            {
                _sprite.flipX = false;
            }
            if (direction < -0.01f)
            {
                _sprite.flipX = true;
            }
            // Gerakkan karakter berdasarkan nilai direction
            Move(direction);
            _animator.SetFloat("speed", Mathf.Abs(direction));

            // Lompat ketika pencet spasi
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        /// <summary>
        /// Menjalankan karakter sesuai dengan arah.
        /// </summary>
        /// <param name="direction">Arah pergerakan horizontal</param>
        void Move(float direction)
        {
            // Mencegah pergerakan di udara
            if (allowAirMovement == false && !grounded)
                return;
            // Bergerak dengan merubah velocity rigidbody
            _body.velocity = new Vector2(direction * speed, _body.velocity.y);

            if (grounded && Mathf.Abs(direction) > 0.01f && Time.time - lastStep > stepSpeed)
            {
                _audio.PlayOneShot(stepSound[lastStepId]);
                lastStep = Time.time;
                lastStepId++;
                if (lastStepId >= stepSound.Length) {
                    lastStepId = 0;
                }
            }
        }

        /// <summary>
        /// Lompat
        /// </summary>
        void Jump()
        {
            // Mencegah lompat 2 kali ketika di udara
            if (!grounded)
                return;
            // Melompatkan karakter dengan merubah velocity vertikal rigidbody
            _body.velocity = new Vector2(_body.velocity.x, jump);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            // Menentukan status karakter menyentuh tanah
            grounded = (groundLayer & 1<<other.gameObject.layer) != 0;
        }

        void OnCollisionExit2D(Collision2D other)
        {
            // Menentukan status karakter berhenti menyentuh tanah
            grounded = !((groundLayer & 1<<other.gameObject.layer) != 0);
        }
    }
}

