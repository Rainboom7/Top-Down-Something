using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;

namespace Controllers
{
    public class InputController : MonoBehaviour
    {

        private Character _player;
        public void SetPlayer(Character player)
        {

            _player = player;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {       
                    _player?.Movement?.MovePosition(hit.point);
                }


            }
             

        }
    }

}
