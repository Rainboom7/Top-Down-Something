using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
namespace Objects
{
    public class Enemy : MonoBehaviour
    {
        public ZombieAttack Attack;
        public Character Character;
        [Range(0,100)]
        public float DamageToPlayer = 20f;
        [Range(0,100)]
        public float DamageToBase = 10f;
        public GameController Controller;
        private GameObject _target;
        public float Speed = 10f;

        private void OnEnable()
        {
            //Character.Health.DieEvent+=
            Character.Movement.Speed = Speed;
            _target = Controller.Base;
            Attack.AttackPlayerEvent += AttackPlayer;
            Character.Movement.MovePosition(_target.transform.position);

        }
        private void AttackPlayer(GameObject player)
        {
            _target = player;
            Character.Movement.MovePosition(_target.transform.position);

        }







    }
}
