using UnityEngine;

namespace Objects
{
    public class TargetedBullet : Bullet
    {
        private void FixedUpdate()
        {
            if (_target == null)
            {
                Destroy(gameObject);
                return;
            }
            Vector3 destination = _target.transform.position;
            Vector3 direction = (_target.transform.position - transform.position).normalized;
            if (Vector3.Distance(destination, transform.position) <= 0.2f)
                DamageTarget(_target);
            gameObject.transform.position += direction * Speed * Time.deltaTime;
        }

        
    }
}
