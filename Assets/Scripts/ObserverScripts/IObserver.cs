using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    //method for Observers to receive notifs on event trigger
    void OnNotify();
}