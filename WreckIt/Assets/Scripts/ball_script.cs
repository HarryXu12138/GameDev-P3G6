using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ball_script : MonoBehaviour {

    public float throw_speed = 2.0f;

    private float speedX = 2.0f;
    private float speedY = 2.0f;

    private float angleX = 0.0f;
    private float angleY = 0.0f;

    private GameObject da_cam;
    private Rigidbody rb;
    //private static Rigidbody cam_rb;
    private bool thrown;
    // Use this for initialization
    void Start () {
        thrown = false;
        da_cam = GameObject.Find("Main Camera");
        transform.position = da_cam.transform.position + (new Vector3(0.5f, 0.0f, 1.0f));
        transform.SetParent(da_cam.transform);
        //cam_rb = da_cam.GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(da_cam.GetComponent<Transform>().eulerAngles.y);
        if (Input.GetKey(KeyCode.E) && !thrown){
            thrown = true;
            transform.SetParent(null);
            //throw the ball upon clicking the E button
            rb.useGravity = true;
            Vector3 velocity = new Vector3(0, 0, throw_speed);
            velocity = Quaternion.Euler(da_cam.transform.eulerAngles.x, da_cam.transform.eulerAngles.y, 0) * velocity;
            //Console.WriteLine()
            //kinda odd that I can't use *= on this
            //velocity = Quaternion.Euler(da_cam.transform.rotation.x, da_cam.transform.rotation.y, 0) * velocity;
            rb.AddForce(velocity);
            
        }
        if(Input.GetKey(KeyCode.Space) && thrown)
        {
            pick();
        }


    }
    
    void pick()
    {
        thrown = false;
        
        transform.position = transform.position = da_cam.transform.position + Quaternion.Euler(da_cam.transform.eulerAngles.x, da_cam.transform.eulerAngles.y, 0) * (new Vector3(0.5f, 0.0f, 1.0f));
        transform.SetParent(da_cam.transform);
        
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        rb.useGravity = false;
    }
    
}
