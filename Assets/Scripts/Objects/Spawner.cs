using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class Spawner : MonoBehaviour
    {
        [Range(0.5f, 10)]
        public float SpawnTime;
        public Enemy Prefab;
        public Controllers.GameController GameController;

        private float _timer;
        private Coroutine _spawnRoutine;
        private void OnEnable()
        {
            if (GameController != null)
                Prefab.Base = GameController.Base;
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
                    var obj = Instantiate(Prefab, transform.position, transform.rotation);
                    if(obj!=null)
                        GameController.AddObject(obj.gameObject);
                    yield return new WaitForSeconds(SpawnTime);

            
                }


            }


        }

    }
}
