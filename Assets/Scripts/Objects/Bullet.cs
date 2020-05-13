using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class Bullet : MonoBehaviour, IPunObservable
    {
        public float Speed = 5f;
        public float Damage = 10f;
        public Rigidbody Rigidbody;
        public PhotonView PhotonView;
        public string ShooterName="NONE";
        private Vector3 _normalizeDirection= Vector3.zero;
        protected float _timer=5f;
        private Vector3 _correctPosition = Vector3.zero;
        private Quaternion _correctRotation = Quaternion.identity;
        private Vector3 _correctVelocity = Vector3.zero;

        public virtual void SetTarget(Vector3 target)
        {
            if (target != null)
                _normalizeDirection = (new Vector3(target.x, transform.position.y, target.z) - transform.position).normalized;
            Rigidbody.velocity = _normalizeDirection * Speed;

        }
        public void SetShooter(string killerName)
        {
            ShooterName = killerName;
        }
        private void Update()
        {

            if (!PhotonView.IsMine)
            {
                Rigidbody.velocity = Vector3.Lerp(Rigidbody.velocity, _correctVelocity, Time.deltaTime );
            }



            _timer -= Time.deltaTime;
            if (_timer <= 0&&PhotonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {

           
            var enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                if(ShooterName!=null)
                 if(!ShooterName.Equals("NONE"))
                         enemy.Killer = ShooterName;
                var health = enemy.gameObject.GetComponentInParent<Health>();
                if (health != null)
                    health.Damage(Damage);
                PhotonNetwork.Destroy(gameObject);

            }

        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(Rigidbody.velocity);           
            }
            else
            {
                _correctVelocity = (Vector3)stream.ReceiveNext();
               
            }
        }

       
    }
}
