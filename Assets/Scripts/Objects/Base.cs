using Photon.Pun;
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

  


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {        
            if (stream.IsWriting)
            {
                stream.SendNext(Health.Currenthealth);
            }
            else
            {
                _correcthealth = Mathf.Max((float)stream.ReceiveNext(), 0f);
                Health.SetHp(_correcthealth);
            }
        }
    }
}
