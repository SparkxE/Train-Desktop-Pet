using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawnerFactory : Factory
{
    public override ISpawner GetSpawnItem(GameObject trainPrefab, Vector3 position)
    {
        GameObject instance = Instantiate(trainPrefab, position, Quaternion.identity);
        instance.name = trainPrefab.name;
        TrainSpawner spawner = instance.AddComponent<TrainSpawner>();
        spawner.Initialize();

        // Train train = instance.AddComponent<Train>();

        Debug.Log(instance.name + " is being created");
        return spawner;
    }
}
