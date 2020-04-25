using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class Health : MonoBehaviour
    {
        public event Action DieEvent;
        public event Action<float> ChangeHealthEvent;
        [Range(0, 200)]
        public float Hitpoints;
        public float Currenthealth { get; private set; }
        private void OnEnable()
        {
            Currenthealth = Hitpoints;
        }

        public void Damage(float damage)
        {
            Currenthealth -= damage;
            if (Currenthealth <= 0)
                DieEvent?.Invoke();
            ChangeHealthEvent?.Invoke(Currenthealth);


        }


    }
}
