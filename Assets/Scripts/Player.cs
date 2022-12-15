using UnityEngine;

namespace KucingGame
{
    public class Player : MonoBehaviour
    {
        public float direction;

        Movement2D movement;

        void Awake()
        {
            movement = GetComponent<Movement2D>();
        }

        void Update ()
        {
            direction = 0;
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

            movement.Move(direction);

            // Lompat ketika pencet spasi
            if (Input.GetKeyDown(KeyCode.Space))
            {
                movement.Jump();
            }
        }
    }
}