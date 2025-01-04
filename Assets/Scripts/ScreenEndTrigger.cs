using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEndTrigger : Subject
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");

        if (other.CompareTag("Respawn"))
        {
            Debug.Log("End point reached!");
            NotifyObservers();
        }
    }
}
