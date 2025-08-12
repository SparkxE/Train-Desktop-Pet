using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    public string SpawnerName { get; set; }
    public void Initialize();
}
