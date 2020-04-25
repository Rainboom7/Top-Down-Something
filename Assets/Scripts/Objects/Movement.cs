using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Objects
{
    public class Movement : MonoBehaviour
    {
        public NavMeshAgent Agent;
        public float Speed;


       public void MovePosition(Vector3 destination)
        {
            Agent.speed = Speed;
            Agent.SetDestination(destination);
        }
    }
}