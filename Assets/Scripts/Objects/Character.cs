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
    }
}
