using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Objects
{
    public class Health : MonoBehaviour, IPunObservable
    {
        public RectTransform Line;
        public event Action DieEvent;
        public Text Text;
        private Vector2 _healthSize;
        [Range(0, 200)]
        public float Hitpoints;
        public PhotonView PhotonView;
        public float Currenthealth { get; set; }
        private void OnEnable()
        {
            Currenthealth = Hitpoints;
            ChangeView();

        }

        public void Damage(float damage)
        {

            Currenthealth -= damage;
            if (Currenthealth <= 0)
                DieEvent?.Invoke();
            ChangeView();
        }
        public void SetHp(float points)
        {
            Currenthealth = points;
            if (Currenthealth <= 0)
                DieEvent?.Invoke();


        }
        public void ChangeView()
        {
            PhotonView?.RPC("ChangeViewRPC", RpcTarget.AllBuffered);
        }

        [PunRPC]
        private void ChangeViewRPC()
        {
            Line.localScale *= Currenthealth / Hitpoints;
            if(Text!=null)
            Text.text = Currenthealth.ToString();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(Currenthealth);
            }
            else
                this.Currenthealth = (float)stream.ReceiveNext();
        }

    }

}
