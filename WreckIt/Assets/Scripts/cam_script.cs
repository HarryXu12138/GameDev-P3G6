using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class cam_script : MonoBehaviour {
    //rotation sensitivity
    public float speedX = 2.0f;
    public float speedY = 2.0f;

    private float angleX = 0.0f;
    private float angleY = 0.0f;

    private float screenWidth;
    private float screenHeight;
    public Text score_text;
    public Text target_score_text;
    //the speed of the camera in the x-z plane
    public float speed = 0.05f;

    private int score;
    private Int32 target_score;

	// Use this for initialization
    void Start (){
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        score = 0;
        score_text.text = score.ToString();
        //hard-coded
        target_score = 11;
	}

    // Update is called once per frame
    void Update()
    {
        
        score_text.text = score.ToString();
        if(score >= target_score){
            score_text.color = Color.green;
        }
        //this is the code to rotate camera
        //only rotates when
        if (Input.mousePosition.x > 0 && Input.mousePosition.x < screenWidth &&
            Input.mousePosition.y > 0 && Input.mousePosition.y < screenHeight && 
            Input.GetKey(KeyCode.Mouse0))
        {

            angleX += speedX * Input.GetAxis("Mouse X");
            angleY -= speedY * Input.GetAxis("Mouse Y");

            //don't ask why Y goes before X, that's how it works
            transform.eulerAngles = new Vector3(angleY, angleX, 0);
        }
        //this is the code to move camera on the xz plane
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        p_Velocity *= speed;
        Vector3 newPosition = transform.position;
        transform.Translate(p_Velocity);

        //this keeps the camera from moving along the Y axis (vertically)
        newPosition.x = transform.position.x;
        newPosition.z = transform.position.z;
        transform.position = newPosition;

        
    }
    public void add_score(int s)
    {
        score += s;
    }
}
