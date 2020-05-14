﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class Base : MonoBehaviour, IPunObservable
    {
        public Health Health;
        private float _correcthealth;
        public PhotonView PhotonView;
        private bool _firstData;
        private void OnEnable()
        {
            _correcthealth = Health.Hitpoints;
            Health.SetHp(_correcthealth);
        }
        private void Update()
        {
        }




        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {        
            if (stream.IsWriting)
            {
                stream.SendNext(_correcthealth);
            }
            else if(stream.IsReading)
            {
                _correcthealth = (float)stream.ReceiveNext();
                _correcthealth = Mathf.Max(_correcthealth, 0f);
                Health.SetHp(_correcthealth);
            }
        }
    }
}
