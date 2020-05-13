using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class Base : MonoBehaviour, IPunObservable
    {
        public Health Health;
        private float _correcthealth = -1;
        private void OnEnable()
        {
            _correcthealth = Health.Hitpoints;
        }
        private void Update()
        {                 
            Health.SetHp(_correcthealth);
        }


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(Health.Currenthealth);
                Debug.Log(Health.Currenthealth);
            }
            else
            {
                _correcthealth = Mathf.Max((float)stream.ReceiveNext(),0f);
            }
        }
    }
}
