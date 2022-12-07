using UnityEngine;

namespace KucingGame
{
    public class PlatformerCamera : MonoBehaviour
    {
        public SpriteRenderer sprite;
        public Transform target;
        public float offsetX;

        Vector3 targetPosition;

        public float lerpSpeed = 2f;


        void Update()
        {
            if (!sprite.flipX)
            {
                targetPosition = new Vector3(target.position.x + offsetX, -2.39f, -10f);
            }
            else
            {
                targetPosition = new Vector3(target.position.x - offsetX, -2.39f, -10f);
            }

            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
        }
    }
}