using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private string spawnerName;

    public string SpawnerName
    {
        get { return this.spawnerName; }
        set { this.spawnerName = value; }
    }

    public void Initialize()
    {
        gameObject.name = SpawnerName;
    }

    
}
