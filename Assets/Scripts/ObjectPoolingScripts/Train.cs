using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Train : MonoBehaviour
{
    public IObjectPool<Train> Pool { get; set; }

    public void ReturnToPool()
    {
        Pool.Release(this);
    }
}
