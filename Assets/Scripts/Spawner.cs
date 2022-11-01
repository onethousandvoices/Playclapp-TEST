using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Playclapp
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField, Range(1f, 10f)]
        private float _speed;

        [SerializeField, Range(1f, 10f)]
        private float _distance;

        [SerializeField, Range(0.5f, 3f)]
        private float _spawnDelay;

        [SerializeField]
        private MovingCube _objPrefab;

        private List<MovingCube> _pool;

        private void Start()
        {
            _pool = new List<MovingCube>();
            StartCoroutine(Spawning());
        }

        private IEnumerator Spawning()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnDelay);

                MovingCube cube;

                if (_pool.Any())
                {
                    cube = _pool.First();
                    _pool.Remove(cube);
                }
                else
                    cube = Instantiate(_objPrefab);
                
                
                cube.gameObject.SetActive(true);
                cube.transform.position = transform.position;
                cube.TransferredEvent += OnCubeTransferred;
                cube.Move(_speed, _distance);
            }
        }

        private void OnCubeTransferred(MovingCube cube)
        {
            cube.TransferredEvent -= OnCubeTransferred;
            _pool.Add(cube);
        }
    }
}
