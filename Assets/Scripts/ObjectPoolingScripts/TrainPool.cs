using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TrainPool : MonoBehaviour
{
    [SerializeField] private int maxPoolSize = 1;
    [SerializeField] private int stackDefaultCapacity = 1;
    [SerializeField] private GameObject[] trainPrefabs;
    [SerializeField] private Vector3 spawnLocation;

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
        TrainSpawnerFactory spawner = GetComponent<TrainSpawnerFactory>();
        Train train;
        ISpawner spawnedTrain = spawner.GetSpawnItem(trainPrefabs[Random.Range(0, trainPrefabs.Length - 1)], spawnLocation);
        GameObject trainObject = GameObject.Find(spawnedTrain.SpawnerName);
        train = trainObject.AddComponent<Train>();
        train.Pool = Pool;
        // Train train = spawner.GetSpawnItem();
        // train.Pool = Pool;

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

    public void SpawnTrain(){
        var train = Pool.Get();
    }
}
