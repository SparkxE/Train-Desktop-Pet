using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : Subject, IObserver
{
    //vars for indicating when to stop at the Station
    [SerializeField] private Transform stationStop;
    [SerializeField] private Transform stationMark;
    [SerializeField] private float stationStartTime;
    [SerializeField] private float stationBuffer;
    private bool isStopping;

    //vars for indicating when to reset at end of screen
    // [SerializeField] private Collider screenMark;
    // [SerializeField] private Transform screenStart;
    [SerializeField] private Subject screenEnd;
    [SerializeField] private IObserver spawner;

    //movement and braking speed vars
    private Rigidbody trainBody;
    [SerializeField] private float accelSpeed;
    // [SerializeField] private float maxSpeed; //may use later (delete if still unused)
    [SerializeField] private float brakeSpeed;
    [SerializeField] private float brakeDistance;
    [SerializeField] private float dragDefault;


    // Start is called before the first frame update
    void Start()
    {
        trainBody = gameObject.GetComponent<Rigidbody>();
        stationMark = GameObject.Find("stationMark").GetComponent<Transform>();
    }

    //using Observer & Subject allows the train to detect when it has left the screen without needing 
    //exact position data or calculations
    private void OnEnable()
    {
        if(screenEnd == null) screenEnd = GameObject.Find("screenEnd").GetComponent<ScreenEndTrigger>();
        Debug.Log("Enabled");
        screenEnd.AddObserver(this);
        Debug.Log("Added");
        // AddObserver(spawner);
    }

    private void OnDestroy()
    {
        screenEnd.RemoveObserver(this);
        Debug.Log("Removed");
        // RemoveObserver(spawner);
    }

    private void SelfDestruct()
    {
        Debug.Log("Destroying");
        Destroy(gameObject);
    }

    public void BecomeObserved()
    {
        Debug.Log("Becoming Observed");
        if (screenEnd != null) screenEnd.AddObserver(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // //prevent the train from going so fast it can't slow down in time
        // if(trainBody.velocity.magnitude > maxSpeed){
        //     // trainBody.AddForce(gameObject.transform.forward * -(accelSpeed + (trainBody.velocity.magnitude - maxSpeed)));
        //     // trainBody.velocity = Vector3.ClampMagnitude(trainBody.velocity, maxSpeed);
        //     trainBody.AddForce(-trainBody.velocity);
        // }        //this statement didn't end up working as expected, leaving it here in case it's needed later (delete if still unused)

        //apply drag to stop properly at the station, then start heading off again after stationStartTime seconds
        if (stationStop.position.x < stationMark.position.x - stationBuffer && stationStop.position.x >= stationMark.position.x - brakeDistance)
        {
            if (/*(stationStop.position.x <= stationMark.position.x - stationBuffer || stationStop.position.x >= stationMark.position.x + stationBuffer) &&*/ isStopping == true)
            {
                isStopping = false;
                Invoke("StartFromStop", stationStartTime);
            }
            else if (trainBody.drag == dragDefault)
            {
                // Debug.Log("Stopping");
                isStopping = true;
            }
            trainBody.drag = trainBody.drag + (brakeSpeed * Time.deltaTime);
        }
        //only move forward if train is out of stopping range && drag is being applied to keep from blowing past the station
        else if (stationStop.position.x > stationMark.position.x - stationBuffer && trainBody.drag == dragDefault)
        {
            trainBody.AddForce(gameObject.transform.forward * accelSpeed);
        }
    }

    private void StartFromStop()
    {
        // Debug.Log("Starting");
        trainBody.drag = dragDefault;
        trainBody.AddForce(gameObject.transform.forward * accelSpeed);  //"kick" the train out of stopping range so it starts moving on its own
    }

    public void OnNotify()
    {
        // Debug.Log("Notifed");
        NotifyObservers();
        // screenEnd.RemoveObserver(this);
        SelfDestruct();
        // gameObject.transform.position = new Vector3(screenStart.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // void OnGUI()     //only for debugging values
    // {
    //     GUI.color = Color.red;
    //     GUI.Label(new Rect(750, 70, 200, 100), "Current Speed: " + gameObject.GetComponent<Rigidbody>().velocity.magnitude);
    // }
}
