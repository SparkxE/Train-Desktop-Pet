using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private string spawnerName;
    private GameObject trainModel;

    public string SpawnerName
    {
        get { return this.spawnerName; }
        set { this.spawnerName = value; }
    }

    public void Initialize()
    {
        trainModel.name = SpawnerName;
    }

    
}
