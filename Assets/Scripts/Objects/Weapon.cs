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
        public int MaxAmmo;
        private float _timer;
        public int Ammo { get; private set; }
        private GameObject _taget;
        private void OnEnable()
        {
            _weaponState = WeaponState.None;
            Ammo = MaxAmmo;
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
                    Ammo = MaxAmmo;
                    AmmoChangeEvent?.Invoke(Ammo, MaxAmmo);
                }
            }
            
        }

        public void Fire(GameObject target )
        {
            if (_weaponState != WeaponState.None)
                return;
            if (Ammo < 1)
            {
                Reload();
                return;
            }
            _taget = target;
            _weaponState = WeaponState.Firing;
            _timer = FireTime;
            Ammo--;
            AmmoChangeEvent?.Invoke(Ammo, MaxAmmo);
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