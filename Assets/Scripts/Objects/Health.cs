using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class Health : MonoBehaviour
    {
        public RectTransform Line;
        public event Action DieEvent;
        private Vector2 _healthSize;
        [Range(0, 200)]
        public float Hitpoints;
        public float Currenthealth { get;  set; }
        private void OnEnable()
        {
            Currenthealth = Hitpoints;
      
        }

        public void Damage(float damage)
        {

            Currenthealth -= damage;
            if (Currenthealth <= 0)
                DieEvent?.Invoke();
            ChangeView();
        }
        public void SetHp(float points) {
            Currenthealth = points;
            if (Currenthealth <= 0)
                DieEvent?.Invoke();
            ChangeView();

        }
        private void ChangeView()
        {
            Line.localScale *= Currenthealth / Hitpoints;
        }


    }

}
