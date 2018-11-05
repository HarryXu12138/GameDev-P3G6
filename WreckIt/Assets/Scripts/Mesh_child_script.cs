using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_child_script : MonoBehaviour
{
    private GameObject[] components;
    private Rigidbody rb;
    private GameObject da_ball;
    public string group;
    public float collapse_point;
    //public GameObject parent;
    // Use this for initialization
    void Start()
    {
        da_ball = GameObject.Find("da_ball");
        components = GameObject.FindGameObjectsWithTag(group);
        rb = GetComponent<Rigidbody>();
       // transform.SetParent(parent.transform);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.impulse.magnitude > collapse_point){
            Debug.Log(collision.impulse.magnitude);
            //transform.SetParent(null);
            for (int a = 0; a < components.Length; a++)
            {
                components[a].GetComponent<Rigidbody>().isKinematic = false;
                components[a].GetComponent<Rigidbody>().useGravity = true;
            }
            Vector3 f = collision.impulse;
            f = f.normalized;
            f.x *= da_ball.GetComponent<Rigidbody>().mass;
            f.y *= da_ball.GetComponent<Rigidbody>().mass;
            f.z *= da_ball.GetComponent<Rigidbody>().mass;

            rb.AddForce(collision.impulse);
            //parent.SendMessage("collapse", true, SendMessageOptions.DontRequireReceiver);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
