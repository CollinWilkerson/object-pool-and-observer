using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

namespace Chapter.ObjectPool 
{
    public class Drone : MonoBehaviour 
    {
        public IObjectPool<Drone> Pool;
        public float _currentHealth;

        //these being serialized has no value as far as im aware, Drones are only instantiated
        [SerializeField] private float maxHealth = 100.0f;
        [SerializeField] private float timeToSelfDestruct = 3.0f;

        void Start() 
        {
            _currentHealth = maxHealth; //classic
        }
        
        void OnEnable() 
        {
            AttackPlayer();
            StartCoroutine(SelfDestruct());
        }

        //without this the objects would never revive, THIS IS VITAL
        private void OnDisable() 
        {
            ResetDrone();
        }

        IEnumerator SelfDestruct() {
            yield return new WaitForSeconds(timeToSelfDestruct);
            TakeDamage(maxHealth);
        }
        
        private void ReturnToPool() {
            Pool.Release(this); //unity's default pool release, attached to additional
            //behavior in Drone Object Pool
        }
        
        private void ResetDrone() {
            _currentHealth = maxHealth;
        }

        public void AttackPlayer() {
            Debug.Log("Attack player!");
        }

        //if the object has died, release it so that the object pool can use that object
        public void TakeDamage(float amount) {
            _currentHealth -= amount;
            
            if (_currentHealth <= 0.0f)
                ReturnToPool();
        }
    }
}