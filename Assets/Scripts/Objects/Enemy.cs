using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Photon.Pun;
using System;
using Photon.Pun.UtilityScripts;

namespace Objects
{
    public class Enemy : Character, IPunObservable
    {
        public Rigidbody Rigidbody;
        [Range(0, 100)]
        public float DamageToPlayer = 20f;
        [Range(0, 100)]
        public float DamageToBase = 10f;
        [HideInInspector]
        public Base Base;
        public PhotonView PhotonView;
        public int ScoreCost = 20;
        private bool _firstData = false;
        public string Killer = "NONE";
        public float Speed = 10f;
        private float _healthPoints;
        private Vector3 _correctPosition = Vector3.zero;
        private Quaternion _correctRotation = Quaternion.identity;
        private Vector3 _correctVelocity = Vector3.zero;

        public event Action<string, int> GiveScoreEvent;

        private void OnEnable()
        {
            Movement.Speed = Speed;
            if (Base != null)
                Movement.MovePosition(Base.transform.position);
            Health.DieEvent += Die;
        }
        private void OnDestroy()
        {
            Health.DieEvent -= Die;
        }
        public void Die()
        {
            ScoreExtensions.AddScore(PhotonNetwork.LocalPlayer, ScoreCost);
            Debug.Log(PhotonNetwork.LocalPlayer);
            PhotonView.RPC("MasterDestroy", RpcTarget.MasterClient);
        }
        [PunRPC]
        public void MasterDestroy()
        {
            PhotonNetwork.Destroy(gameObject);
        }

        private void Update()
        {
            if (!_firstData)
                return;
            transform.position = Vector3.Lerp(transform.position, _correctPosition, Time.deltaTime * 5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, _correctRotation, Time.deltaTime * 5f);
            Rigidbody.velocity = Vector3.Lerp(Rigidbody.velocity, _correctVelocity, Time.deltaTime * 5f);
            Health.SetHp(_healthPoints);
        }
        public void ChangeTarget(GameObject target)
        {

            if (target != null)
                Movement.MovePosition(target.transform.position);
            else if (Base != null)
            {
                Movement.MovePosition(Base.transform.position);
            }

        }
      

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Player>()!=null)
            {
                var health = collision.gameObject.GetComponent<Health>();
                health?.Damage(DamageToPlayer);
                PhotonNetwork.Destroy(gameObject);
            }
            if (collision.gameObject.GetComponent<Base>() != null)
            {
                var health = collision.gameObject.GetComponent<Health>();
                health?.Damage(DamageToBase);
                PhotonNetwork.Destroy(gameObject);
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
                stream.SendNext(Rigidbody.velocity);
                stream.SendNext(Health.Currenthealth);

            }
            else
            {
                _correctPosition = (Vector3)stream.ReceiveNext();
                _correctRotation = (Quaternion)stream.ReceiveNext();
                _correctVelocity = (Vector3)stream.ReceiveNext() * 0.5f;
                _healthPoints = (float)stream.ReceiveNext();

                if (_firstData)
                {
                    transform.position = _correctPosition;
                    transform.rotation = _correctRotation;
                    Rigidbody.velocity = _correctVelocity;
                    Health.Currenthealth = _healthPoints;
                    _firstData = false;
                }
            }
        }
    }
}
