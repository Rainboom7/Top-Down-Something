using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public  class Weapon : MonoBehaviour
    {
        private enum WeaponState
        {
            Firing,
            Reloading,
            None
        }
        public float Damage;
        public event Action<int,int> AmmoChangeEvent;
        private WeaponState _weaponState;
        public Bullet Bullet;
        public float ReloadTime;
        public float FireTime;
        public int Ammo;
        private float _timer;
        private int _ammo;
        private GameObject _taget;
        private void OnEnable()
        {
            _weaponState = WeaponState.None;
            _ammo = Ammo;
        }

        private void Update()
        {
            if (_timer > 0f)
                _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                if (_weaponState == WeaponState.Firing)
                {
                    _timer = 0f;
                   _weaponState = WeaponState.None;
                }
                else if (_weaponState == WeaponState.Reloading)
                {
                    _weaponState = WeaponState.None;
                    _timer = 0f;
                    _ammo = Ammo;
                    AmmoChangeEvent?.Invoke(_ammo, Ammo);
                }
            }
            
        }

        public void Fire(GameObject target )
        {
            if (_weaponState != WeaponState.None)
                return;
            if (_ammo < 1)
            {
                Reload();
                return;
            }
            _taget = target;
            _weaponState = WeaponState.Firing;
            _timer = FireTime;
            _ammo--;
            AmmoChangeEvent?.Invoke(_ammo, Ammo);
            ShootBullet();
        }
        private void ShootBullet()
        {
            var bullet = Instantiate(Bullet, transform.position, transform.rotation);
            bullet?.SetTarget(_taget);
        }
        private void Reload()
        {
            _timer = ReloadTime;
            _weaponState = WeaponState.Reloading;
        }

    }
}