using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPooler : MonoBehaviour
{
    private TrainPool pool;

    // Start is called before the first frame update
    void Start()
    {
        pool = gameObject.GetComponent<TrainPool>();
        pool.SpawnTrain();
    }

}
