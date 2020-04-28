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
        public GameController Controller;
        private GameObject _target;
        public float Speed = 10f;
        public float AttackRange = 50f;

        private void OnEnable()
        {

            Character.Movement.Speed = Speed;
            if (Controller != null)
                _target = Controller.Base.gameObject;
            Character.Movement.MovePosition(_target.transform.position);

        }

        private void Update()
        {
            if (Controller.Player != null)
            { 
                var dist = Vector3.Distance(Controller.Player.transform.position, gameObject.transform.position);
                if (dist <= AttackRange)
                {
                    AttackPlayer();
                }
            }
        }
        private void AttackPlayer()
        {
            _target = Controller.Player.gameObject;
            Character.Movement.MovePosition(_target.transform.position);

        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Equals("Player"))//TODO
            {
                var health = collision.gameObject.GetComponent<Health>();
                health?.Damage(DamageToPlayer);
                Destroy(gameObject);
            }
            if (collision.gameObject.GetComponent<Base>()!=null)
            {
                var health = collision.gameObject.GetComponent<Health>();
                health?.Damage(DamageToBase);
                Destroy(gameObject);
            }

        }
    }
}
