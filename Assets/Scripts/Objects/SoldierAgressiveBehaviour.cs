using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class SoldierAgressiveBehaviour : Behaviour
    {
        public Weapon Weapon;
        private GameObject _target;
        private float _timer = 0.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Enemy>() != null &&_target==null)
                _target = other.gameObject;
        }
        private void Update()
        {
            if (IsPlayer)
                return;
            if (_timer > 0)
                _timer -= Time.deltaTime;
            else
            {
                _timer = 0.5f;
                if (Weapon != null && _target != null)
                    Weapon.Fire(_target);
            }
            
        }
    }
}
