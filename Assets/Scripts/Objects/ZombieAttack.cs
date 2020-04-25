using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class ZombieAttack : MonoBehaviour
    {
        [HideInInspector]
        public event Action<GameObject> AttackPlayerEvent;
  
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                AttackPlayerEvent.Invoke(collision.gameObject);
            }
        }
    }
}
