using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class Character : MonoBehaviour
    {
        public Movement Movement;
        public Health Health;
        public Weapon Weapon;

        private void OnEnable()
        {
            if (Health != null)
                Health.DieEvent += OnDeath;
        }
        private void OnDisable()
        {
            if (Health != null)
                Health.DieEvent -= OnDeath;
        }
        private void OnDeath()
        {
            Destroy(gameObject);
        }
        public void Fire(GameObject target)
        {
            if (Weapon == null)
                return;
            transform.rotation = Quaternion.LookRotation(target.transform.position);
            transform.Rotate(new Vector3(0, 135f, 0));
            Weapon.Fire(target);

        }
      
    }
}
