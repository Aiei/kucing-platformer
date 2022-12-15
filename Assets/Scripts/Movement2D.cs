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

        /// <summary>
        /// Kumpulan suara steps
        /// </summary>
        public AudioClip[] stepSound;

        /// <summary>
        /// Waktu terakhir step
        /// </summary>
        float lastStep = 0;

        /// <summary>
        /// Jarak antara steps
        /// </summary>
        public float stepSpeed;

        /// <summary>
        /// Index audio step yg terakhir kali dimainkan
        /// </summary>
        int lastStepId = 0;

        void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _sprite = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _audio = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Menjalankan karakter sesuai dengan arah.
        /// </summary>
        /// <param name="direction">Arah pergerakan horizontal</param>
        public void Move(float direction)
        {
            // Menentukan arah hadap sprite berdasarkan arah input
            if (direction > 0.01f)
            {
                _sprite.flipX = false;
            }
            if (direction < -0.01f)
            {
                _sprite.flipX = true;
            }

            // Mencegah pergerakan di udara
            if (allowAirMovement == false && !grounded)
                return;
            // Bergerak dengan merubah velocity rigidbody
            _body.velocity = new Vector2(direction * speed, _body.velocity.y);

            // Untuk animator
            _animator.SetFloat("speed", Mathf.Abs(direction));

            // Hanya memainkan suara steps ketika karakter berjalan
            if (grounded && Mathf.Abs(direction) > 0.01f 
                && Time.time - lastStep > stepSpeed) // Memastikan clip berikutnya tidak terlalu cepat dimainkan
            {
                // Memainkan clip step
                _audio.PlayOneShot(stepSound[lastStepId]);
                // Mencatat waktu terakhir kali step dimainkan
                lastStep = Time.time;
                // Menaikkan index array clip step
                lastStepId++;
                if (lastStepId >= stepSound.Length) {
                    lastStepId = 0;
                }
            }
        }

        /// <summary>
        /// Lompat
        /// </summary>
        public void Jump()
        {
            // Mencegah lompat 2 kali ketika di udara
            if (!grounded)
                return;
            // Melompatkan karakter dengan merubah velocity vertikal rigidbody
            _body.velocity = new Vector2(_body.velocity.x, jump);
        }

        public bool facingLeft()
        {
            return (_sprite.flipX == true);
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

