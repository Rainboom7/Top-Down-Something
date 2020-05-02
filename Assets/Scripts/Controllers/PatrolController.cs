using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
namespace Controllers
{

    public class PatrolController : MonoBehaviour
    {
        public List<Player> Patrolers { get; set; }
        public GameObject[] WayPoints;

        public void StartPatroling() {
            foreach (Character patroler in Patrolers)
            {
                if (patroler.Movement != null)
                {
                    patroler.Movement.Waypoints = WayPoints;
                    patroler.Movement.StartPatrol();
                }
                if(patroler.Behaviour!=null)
                patroler.Behaviour.IsPlayer = false;

            }
        }

    }
}