using UnityEngine;
using UnityEngine.Pool;

namespace Chapter.ObjectPool 
{
    public class DroneObjectPool : MonoBehaviour
    {
        public int maxPoolSize = 500; //this value keeps up decently with my autoclicker
        public int stackDefaultCapacity = 500; //can these values even be different? if so what does that do?

        //Unity's built in object pool interface
        public IObjectPool<Drone> Pool //this kind of acts like a public constroctor for the private _pool
        {
            get 
            {
                if (_pool == null)
                    _pool = 
                        //sets up the methods that the pool uses
                        new ObjectPool<Drone>(
                            CreatedPooledItem, 
                            OnTakeFromPool, 
                            OnReturnedToPool, 
                            OnDestroyPoolObject, 
                            true, 
                            stackDefaultCapacity,
                            maxPoolSize); //sets the pools max size and stack capacity
                return _pool;
            }
        }

        private IObjectPool<Drone> _pool;

        private Drone CreatedPooledItem() 
        {
            GameObject go = 
                GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            Drone drone = go.AddComponent<Drone>();
            
            go.name = "Drone";
            drone.Pool = Pool;
            
            return drone;
        }

        private void OnReturnedToPool(Drone drone) 
        {
            drone.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(Drone drone) 
        {
            drone.gameObject.SetActive(true);
        }

        private void OnDestroyPoolObject(Drone drone) 
        {
            Destroy(drone.gameObject);
        }

        public void Spawn() 
        {
            var amount = Random.Range(1, 10);
            
            for (int i = 0; i < amount; ++i) {
                var drone = Pool.Get();
                
                drone.transform.position = 
                    Random.insideUnitSphere * 10;
            }
        }
    }
}