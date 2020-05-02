using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class ShotgunBullet : Bullet
    {

        [SerializeField]
        private List<Bullet> _bullets;
        private Vector3 _normalizeDirection;
        public override void  SetTarget(GameObject target) {
            _target = target;
            if(_target!=null)
                _normalizeDirection = (_target.transform.position - transform.position).normalized;

        }
       
        private void Update()
        {
            foreach (Bullet bullet in _bullets)
            {   if(bullet!=null)
                    bullet.transform.position += _normalizeDirection* bullet.Speed * Time.deltaTime;
            }
        }


    }
}
