using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
using Photon.Pun;

namespace Controllers
{
    public class PlayerController : MonoBehaviour, IPunObservable
    {
        public PhotonView PhotonView;
        public Player Player;
        public Health Health;
        public Movement Movement;
        public Weapon Weapon;
        public Rigidbody Rigidbody;
        public GameController Controller;
        public string BulletPrefab;



        private bool _firstData;
        private Vector3 _correctPosition = Vector3.zero;
        private Quaternion _correctRotation = Quaternion.identity;
        private Vector3 _correctVelocity = Vector3.zero;
        private float _healthPoints;
        private int _ammo;
        public string PlayerName="none";
        public int Score;


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
                stream.SendNext(Rigidbody.velocity);
                stream.SendNext(Health.Currenthealth);
                stream.SendNext(Weapon.Ammo);

            }
            else
            {
                _correctPosition = (Vector3)stream.ReceiveNext();
                _correctRotation = (Quaternion)stream.ReceiveNext();
                _correctVelocity = (Vector3)stream.ReceiveNext() * 0.5f;
                _healthPoints = (float)stream.ReceiveNext();
                _ammo = (int)stream.ReceiveNext();

                if (_firstData)
                {
                    transform.position = _correctPosition;
                    transform.rotation = _correctRotation;
                    Rigidbody.velocity = _correctVelocity;
                    Health.Currenthealth = _healthPoints;
                    Weapon.Ammo = _ammo;
                    _firstData = false;
                }
            }
        }

        private void OnEnable()
        {

            _correctPosition = transform.position;
            _correctRotation = transform.rotation;
            _correctVelocity = Rigidbody.velocity;
            _healthPoints = Health.Hitpoints;
            _ammo = Weapon.MaxAmmo;
            Weapon.SetAmmo(_ammo);
            if (PhotonView.IsMine)
                Controller.FollowCamera.Target = transform;
            PlayerName = (string)PhotonView.Controller.CustomProperties["PlayerName"];
            Controller.NetworkManager.AddPlayer(this);
        }

        private void Update()
        {
            if (PhotonView.IsMine)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                    {
                        Player?.Movement?.MovePosition(hit.point);
                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                    {
                        Fire(hit);
                    }


                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _correctPosition, Time.deltaTime * 5f);
                transform.rotation = Quaternion.Lerp(transform.rotation, _correctRotation, Time.deltaTime * 5f);
                Rigidbody.velocity = Vector3.Lerp(Rigidbody.velocity, _correctVelocity, Time.deltaTime * 5f);
                Health.SetHp(_healthPoints);
                Weapon.SetAmmo(_ammo);
            }
        }
        private void Fire(RaycastHit hit) {
            if (Player == null)
                return;
            if (Player.Fire(hit.point))
            {
                GameObject prefab = PhotonNetwork.Instantiate(BulletPrefab, transform.position, Quaternion.Euler(90, 0, 0));
                Bullet bullet = prefab.gameObject.GetComponent<Bullet>();
                bullet?.SetTarget(hit.point);
                bullet?.SetShooter(PlayerName);
            }
        }
    }
}
