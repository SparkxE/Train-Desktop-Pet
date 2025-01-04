using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour, IObserver
{
    //vars for indicating when to stop at the Station
    [SerializeField] private Transform stationStop;
    [SerializeField] private Transform stationMark;
    [SerializeField] private float stationStartTime;

    //vars for indicating when to reset at end of screen
    [SerializeField] private Collider screenMark;
    [SerializeField] private Transform screenStart;
    [SerializeField] private Collider screenEnd;

    //movement and braking speed vars
    [SerializeField] private float moveSpeed;
    [SerializeField] private float brakeSpeed;
    [SerializeField] private float brakeDistance;
    [SerializeField] private float dragDefault;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(stationStop.position.x <= stationMark.position.x && stationStop.position.x >= stationMark.position.x - brakeDistance){
            gameObject.GetComponent<Rigidbody>().drag = gameObject.GetComponent<Rigidbody>().drag + brakeSpeed;
            Invoke("StartFromStop", stationStartTime);
        }

        // if(){
        //     gameObject.transform.position = screenStart.position;
        // }
        else{
            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * moveSpeed);
        }
    }

    private void StartFromStop(){
        gameObject.GetComponent<Rigidbody>().drag = dragDefault; 
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * moveSpeed);
    }

    public void OnNotify(){

    }

    // void OnGUI()     //only for debugging values
    // {
    //     GUI.color = Color.red;
    //     GUI.Label(new Rect(750, 70, 200, 100), "Current Drag: " + gameObject.GetComponent<Rigidbody>().drag);
    // }


}
