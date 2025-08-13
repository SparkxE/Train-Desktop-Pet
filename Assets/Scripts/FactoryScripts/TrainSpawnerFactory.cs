using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawnerFactory : Factory, IObserver
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject[] trainPrefabSet;
    [SerializeField] private float timerMin;
    [SerializeField] private float timerMax;

    private TrainMovement trainObject;
    private Vector3 oldVelocity;

    private void Start()
    {
        trainObject = GameObject.Find("train_1").GetComponent<TrainMovement>();
        trainObject.AddObserver(this);
    }

    public override ISpawner GetSpawnItem(GameObject trainPrefab)
    {
        GameObject instance = Instantiate(trainPrefab, startPoint.position, trainPrefab.transform.rotation);
        // instance.name = trainPrefab.name;
        TrainSpawner spawner = instance.AddComponent<TrainSpawner>();
        spawner.Initialize();

        trainObject = instance.GetComponent<TrainMovement>();
        trainObject.AddObserver(this);
        // trainObject.BecomeObserved();
        trainObject.GetComponent<Rigidbody>().velocity = oldVelocity;
        // Train train = instance.AddComponent<Train>();

        Debug.Log("Train is being created");
        return spawner;
    }

    private void DelaySpawn()
    {
        GameObject trainPrefab = trainPrefabSet[Random.Range(0, trainPrefabSet.Length - 1)];
        GetSpawnItem(trainPrefab);
    }

    public void OnNotify()
    {
        Debug.Log("Notifed");
        if (trainObject != null)
        {
            oldVelocity = trainObject.GetComponent<Rigidbody>().velocity;
        }
        else Debug.Log("oops");

        // GetSpawnItem(trainPrefabSet[0]);
        float spawnDelay = Random.Range(timerMin, timerMax);
        Invoke("DelaySpawn", spawnDelay);

        // GetSpawnItem(trainPrefabSet[Random.Range(0, trainPrefabSet.Length - 1)]);
    }
}
