using UnityEngine;

namespace Objects
{
    public class Bullet : MonoBehaviour
    {
        public float Speed = 5f;
        public float Damage = 10f;
        public Rigidbody Rigidbody;

        private float _timer;
        private GameObject _target;

        public void SetTarget(GameObject target)
        {
            _target = target;
        }
    

        private void FixedUpdate()
        {
            if (_target == null)
                return;
            Vector3 destination = _target.transform.position;
            Vector3 direction = (_target.transform.position - transform.position).normalized;
            if (Vector3.Distance(destination, transform.position) <= 0.2f)
                DamageTarget();
            gameObject.transform.position += direction * Speed * Time.deltaTime;
        }

        private void DamageTarget()
        {
            {
                if (_target.gameObject.GetComponent<Enemy>()!=null)
                {
                    var health = _target.gameObject.GetComponentInParent<Health>();
                    if (health != null)
                        health.Damage(Damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
