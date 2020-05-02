using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class Bullet : MonoBehaviour
    {
        public float Speed = 5f;
        public float Damage = 10f;
        public Rigidbody Rigidbody;

        protected float _timer;
        protected GameObject _target;
        public virtual void SetTarget(GameObject target)
        {
            _target = target;
        }
        protected void DamageTarget(GameObject target)
        {
            {
                if (_target.gameObject.GetComponent<Enemy>() != null)
                {
                    var health = _target.gameObject.GetComponentInParent<Health>();
                    if (health != null)
                        health.Damage(Damage);
                    Destroy(gameObject);
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {

            if (_target != null)
                return;
            var enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {

                var health = enemy.gameObject.GetComponentInParent<Health>();
                if (health != null)
                    health.Damage(Damage);
                Destroy(gameObject);

            }

        }
    }
}
