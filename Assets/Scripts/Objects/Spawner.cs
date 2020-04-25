using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class Spawner : MonoBehaviour
    {
        [Range(0.5f, 10)]
        public float SpawnTime;
        public GameObject prefab;

        private float _timer;
        private Coroutine _spawnRoutine;
        private void OnEnable()
        {
            _spawnRoutine = StartCoroutine(SpawnRoutine);
        }
        private void OnDisable()
        {
            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
                _spawnRoutine = null;
            }
        }
        private IEnumerator SpawnRoutine
        {
            get
            {
                yield return new WaitForSeconds(SpawnTime);
                while(true)
                {
                    Instantiate(prefab, transform.position, transform.rotation);
                    yield return new WaitForSeconds(SpawnTime);

            
                }


            }


        }

    }
}
