using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    //collection of observers
    List<IObserver> observers = new List<IObserver>();

    //add the observer to the subject's collection
    public void AddObserver(IObserver observer){
        observers.Add(observer);
    }

    //remove the observer from the subject's collection
    public void RemoveObserver(IObserver observer){
        observers.Remove(observer);
    }

    //notify each observer that an event has occurred
    protected void NotifyObservers(){
        foreach(IObserver observer in observers){
            observer.OnNotify();
        }
    }
}