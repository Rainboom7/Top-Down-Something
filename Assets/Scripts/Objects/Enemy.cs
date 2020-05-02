using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
namespace Objects
{
    public class Enemy : MonoBehaviour
    {

        public Character Character;
        [Range(0, 100)]
        public float DamageToPlayer = 20f;
        [Range(0, 100)]
        public float DamageToBase = 10f;
        [HideInInspector]
        public Base Base;
        public float Speed = 10f;

        private void OnEnable()
        {

            Character.Movement.Speed = Speed;
            if (Base != null)             
            Character.Movement.MovePosition(Base.transform.position);

        }
        public void ChangeTarget(GameObject target)
        {

            if (target != null)
                Character.Movement.MovePosition(target.transform.position);
            else if (Base != null)
            {
                Character.Movement.MovePosition(Base.transform.position);
            }

        }
      

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Player>()!=null)
            {
                var health = collision.gameObject.GetComponent<Health>();
                health?.Damage(DamageToPlayer);
                Destroy(gameObject);
            }
            if (collision.gameObject.GetComponent<Base>() != null)
            {
                var health = collision.gameObject.GetComponent<Health>();
                health?.Damage(DamageToBase);
                Destroy(gameObject);
            }
        }
    }
}
