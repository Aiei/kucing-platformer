using UnityEngine;

namespace KucingGame
{
    public class Kucing : MonoBehaviour
    {
        public bool hasWeapon;
        Animator animator;
        public Bullet bulletPrefab;
        public Transform firePosition;
        Vector2 offset;

        void Start()
        {
            animator = GetComponent<Animator>();
            offset = firePosition.position - transform.position;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && hasWeapon && !animator.GetBool("firing"))
            {
                Fire();
            }
        }

        public void AddWeapon()
        {
            hasWeapon = true;
            animator.SetBool("has weapon", true);
        }

        public void RemoveWeapon()
        {
            hasWeapon = false;
            animator.SetBool("has weapon", false);
        }

        public void Fire()
        {
            animator.SetBool("firing", true);

            Bullet b;
            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                b = Instantiate<Bullet>(bulletPrefab, transform.position + new Vector3(-offset.x, offset.y, 0), Quaternion.identity);
                b.speed *= -1;
                b.Flip();
            }
            else
            {
                b = Instantiate<Bullet>(bulletPrefab, transform.position + new Vector3(offset.x, offset.y, 0), Quaternion.identity);
            }
            
            Invoke("StopFiring", 0.3f);
        }

        public void StopFiring()
        {
            animator.SetBool("firing", false);
        }
    }
}