using System;
using System.Collections;
using UnityEngine;

namespace Playclapp
{
    public class MovingCube : MonoBehaviour
    {
        public event Action<MovingCube> TransferredEvent;
        
        public void Move(float speed, float distance)
        {
            StartCoroutine(Transfer(speed, distance));
        }

        private IEnumerator Transfer(float speed, float distance)
        {
            Vector3 position = transform.position;
            Vector3 target = new Vector3(position.x, position.y, position.z + distance);
            
            while (transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed  * Time.deltaTime);
                yield return null;
            }
            
            gameObject.SetActive(false);
            TransferredEvent?.Invoke(this);
        }
    }
}
