using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    public abstract ISpawner GetSpawnItem();

    public string GetLog(ISpawner spawner)
    {
        string message = "Factory: spawned object " + spawner.SpawnerName;
        return message;
    }
}
