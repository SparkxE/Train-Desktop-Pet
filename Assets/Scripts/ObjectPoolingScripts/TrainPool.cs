using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TrainPool : MonoBehaviour
{
    [SerializeField] private int maxPoolSize = 10;
    [SerializeField] private int stackDefaultCapacity = 10;
    [SerializeField] private GameObject[] trainPrefabs;
    [SerializeField] private int trainMaxX;
    [SerializeField] private int trainMinX;
    [SerializeField] private int trainMaxZ;
    [SerializeField] private int trainMinZ;
    [SerializeField] private float trainDropHeight;

    private IObjectPool<Train> _pool;
    public IObjectPool<Train> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<Train>(
                    CreatePooledItem,
                    OnTakeFromPool,
                    OnReturnedToPool,
                    OnDestroyPoolObject,
                    true,
                    stackDefaultCapacity,
                    maxPoolSize
                );
            }
            return _pool;
        }
    }
    
    private Train CreatePooledItem()
    {
        GameObject go = GameObject.Instantiate(trainPrefabs[Random.Range(0, trainPrefabs.Length)]);
        Train train = go.AddComponent<Train>();
        train.Pool = Pool;

        return train;
    }

    private void OnTakeFromPool(Train train)
    {
        train.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(Train train)
    {
        train.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Train train)
    {
        Destroy(train.gameObject);
    }

    public void SpawnTrains(){
        for(int i = 0; i < stackDefaultCapacity; i++){
            var train = Pool.Get();
            train.transform.position = new Vector3(Random.Range(trainMinX, trainMaxX), trainDropHeight, Random.Range(trainMinZ, trainMaxZ));
        }
    }
}
