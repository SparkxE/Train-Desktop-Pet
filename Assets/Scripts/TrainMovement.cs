using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    private const float nonZeroBuffer = 1; //buffer value to prevent divide by zero when adjusting speed values

    //vars for indicating when to stop at the Station
    [SerializeField] private Transform stationStop;
    [SerializeField] private Transform stationMark;

    //vars for indicating when to reset at end of screen
    [SerializeField] private Transform screenStop;
    [SerializeField] private float screenStart;
    [SerializeField] private float screenEndMark;

    //movement and braking speed vars
    [SerializeField] private float moveSpeed;
    [SerializeField] private float brakeSpeed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if(stationStop.position.x <= stationMark.position.x - 20){
        //     gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward / moveSpeed);
        //     gameObject.GetComponent<Rigidbody>().drag = gameObject.GetComponent<Rigidbody>().drag + brakeSpeed;
        //     Invoke("StartFromStop", 10);
        // }

        // if(screenStop.position.x == screenEndMark){
        //     gameObject.transform.position = new Vector3(screenStart, gameObject.transform.position.y, gameObject.transform.position.z);
        // }
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * moveSpeed);
        // gameObject.GetComponent<Rigidbody>().drag = gameObject.GetComponent<Rigidbody>().drag / brakeSpeed;
    }

    // void OnGUI()     //only for debugging values
    // {
    //     GUI.color = Color.red;
    //     GUI.Label(new Rect(750, 70, 200, 100), "Current Drag: " + gameObject.GetComponent<Rigidbody>().drag);
    // }


}
