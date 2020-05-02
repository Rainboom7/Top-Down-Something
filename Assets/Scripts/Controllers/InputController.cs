using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;

namespace Controllers
{
    public class InputController : MonoBehaviour
    {

        private Player _player;
        public void SetPlayer(Player player)
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
            if (Input.GetMouseButtonDown(1))
            {
                Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(castPoint, out hit, Mathf.Infinity);
                if (hit.collider.gameObject.GetComponent<Enemy>() != null)
                     _player.Fire(hit.collider.gameObject);


            }
        }

    }
}
