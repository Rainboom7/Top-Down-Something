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
        private const int SECONDSTILLCHECK = 5;
        public GameObject[] Waypoints { get; set; }
        private Coroutine _patrolRoutine;



        public void MovePosition(Vector3 destination)
        {
            if (Agent == null)
                return;
            if (Agent.isStopped)
                Agent.isStopped = false;
            Agent.speed = Speed;
            Agent.SetDestination(destination);
        }
        public void StartPatrol()
        {
            if(Waypoints!=null)
                _patrolRoutine=StartCoroutine(Patrol());
        }
        public void StopPatrol() {
            _patrolRoutine = null;
            if (Agent != null)
                Agent.isStopped = true;

            StopAllCoroutines();

        }
        private void OnDisable()
        {

            StopAllCoroutines();
        }
        public bool IsHere() {
            return (Agent.pathStatus == NavMeshPathStatus.PathComplete);

        }
        private IEnumerator Patrol() {
            yield return new WaitForSeconds(SECONDSTILLCHECK/5);
            while (true) {
                int position = Random.Range(0, Waypoints.Length);
                MovePosition(Waypoints[position].transform.position);
                yield return new WaitForSeconds(SECONDSTILLCHECK);

            }
            
        }
       
    }
}