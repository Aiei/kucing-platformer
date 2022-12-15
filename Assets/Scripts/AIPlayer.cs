using UnityEngine;

namespace KucingGame
{
    public class AIPlayer : MonoBehaviour
    {
        public Vector2 terrainCheckOffset;

        [HideInInspector]
        public Movement2D movement;

        void Awake()
        {
            movement = GetComponent<Movement2D>();
        }

        void FixedUpdate()
        {
            movement.Move(1f);

            if (!CekTerrain())
            {
                movement.Jump();
            }
        }

        /// <summary>
        /// Melakukan cek apabila didepan karakter terdapat jurang
        /// </summary>
        bool CekTerrain()
        {
            Vector2 offset = terrainCheckOffset;
            if (movement.facingLeft())
            {
                offset.x *= -1;
            }
            Vector2 point = (Vector2)transform.position + offset;
            // Menampilkan garis debug untuk raycast
            Debug.DrawLine(point, point + Vector2.down * 5.0f, Color.blue);
            
            return Physics2D.Raycast(point, Vector2.down, 5.0f);
        }
    } 
}

