using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ball_script : MonoBehaviour {

    public float throw_speed = 2.0f;

    private float speedX = 2.0f;
    private float speedY = 2.0f;

    private float angleX = 0.0f;
    private float angleY = 0.0f;

    private bool pickable = false;

    public Text pick_ball_text;
    public GameObject da_pick_text_panel;

    public Slider power_bar;
    private bool power_up;

    private GameObject da_cam;
    private Rigidbody rb;
    //private static Rigidbody cam_rb;
    private bool thrown;
    // Use this for initialization
    void Start () {
        da_pick_text_panel.SetActive(false);
        power_up = true;
        power_bar.value = 0;
        pick_ball_text.text = "Press SPACE to pick up the ball";
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
        if(!thrown && Input.GetKey(KeyCode.E))
        {
            //starts charging
            if(power_up){
                Debug.Log("power up");
                power_bar.value += Time.deltaTime * throw_speed;
                if(power_bar.value > 100){
                    power_up = false;
                }
            }
            else{
                Debug.Log("power down");
                power_bar.value -= Time.deltaTime * throw_speed;
                if(power_bar.value < 0){
                    power_up = true;
                }
            }
        }


        if (Input.GetKeyUp(KeyCode.E) && !thrown){
            thrown = true;
            transform.SetParent(null);
            //throw the ball upon clicking the E button
            rb.useGravity = true;
            float tspd = power_bar.value * 1.3f;
            Vector3 velocity = new Vector3(0, 0, tspd);
            velocity = Quaternion.Euler(da_cam.transform.eulerAngles.x, da_cam.transform.eulerAngles.y, 0) * velocity;
            //Console.WriteLine()
            //kinda odd that I can't use *= on this
            //velocity = Quaternion.Euler(da_cam.transform.rotation.x, da_cam.transform.rotation.y, 0) * velocity;
            rb.AddForce(velocity);
            
        }

        if (thrown)
        {
            power_bar.value = 0;
            if ((Math.Abs(transform.position.x - da_cam.transform.position.x) < 1) &&
                (Math.Abs(transform.position.z - da_cam.transform.position.z) < 1) && pickable)
            {

                da_pick_text_panel.SetActive(true);
                if (Input.GetKey(KeyCode.Space))
                {
                    pick();
                }
            }
        }



    }

    void pick()
    {
        //pick_ball_text.text = "";
        da_pick_text_panel.SetActive(false);
        thrown = false;
        pickable = false;
        transform.position = transform.position = da_cam.transform.position + Quaternion.Euler(da_cam.transform.eulerAngles.x, da_cam.transform.eulerAngles.y, 0) * (new Vector3(0.5f, 0.0f, 1.0f));
        transform.SetParent(da_cam.transform);

        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        rb.useGravity = false;
    }

    private void OnCollisionStay(Collision c)
    {
        if(rb.velocity.magnitude < 1 &&
           rb.angularVelocity.magnitude < 1){
            pickable = true;
        }
    }
}
