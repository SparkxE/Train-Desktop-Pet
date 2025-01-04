using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEndTrigger : Subject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            Debug.Log("Trigger entered!");
            Debug.Log("End point reached!");
            NotifyObservers();
        }
    }
}
