using System.Collections;
using UnityEngine;

namespace Lesson1.Task1
{
    public class Unit : MonoBehaviour
    {
        private const int MAX_HEALTH = 100;
        private const int MAX_TIME = 3;
        private const float TIME_STEP = .5f;
    
        public int health = 50;

        private bool _isReceiving;
        private float _currentTime;
    
        public void ReceiveHealing()
        {
            if (_isReceiving)
                return;

            StartCoroutine(RestoreHealth());
        }

        private IEnumerator RestoreHealth()
        {
            _currentTime = 0f;
            _isReceiving = true;
        
            while (_currentTime < MAX_TIME)
            {
                yield return new WaitForSeconds(TIME_STEP);
                health = Mathf.Clamp(health + 5, 0, MAX_HEALTH);
                _currentTime += TIME_STEP;
            
                if (health == MAX_HEALTH)
                    break;
            }

            _isReceiving = false;
        }


        private void Start()
        {
            if (health < MAX_HEALTH)
            {
                ReceiveHealing();
            }
        }
    
    }
}
