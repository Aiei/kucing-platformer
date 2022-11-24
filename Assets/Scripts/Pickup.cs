using UnityEngine;

namespace KucingGame
{
    [RequireComponent(typeof(Collider2D))]
    public class Pickup : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            Kucing kucing;
            if (kucing = other.GetComponent<Kucing>())
            {
                kucing.AddWeapon();
                Destroy(gameObject);
            }
        }
    }
}