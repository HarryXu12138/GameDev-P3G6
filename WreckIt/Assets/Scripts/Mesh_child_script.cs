using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_child_script : MonoBehaviour
{
    private GameObject[] components;
    private Rigidbody rb;
    private GameObject da_ball;
    public string group;
    private float collapse_point;
    public GameObject parent;
    private bool destroyed;
    public bool break_self_joint;
    // Use this for initialization
    void Start()
    {
        destroyed = false;
        da_ball = GameObject.Find("da_ball");
        components = GameObject.FindGameObjectsWithTag(group);
        rb = GetComponent<Rigidbody>();
        //transform.SetParent(parent.transform);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.impulse.magnitude > collapse_point && !destroyed){
            Debug.Log(collision.impulse.magnitude);
            //transform.SetParent(null);
            if (break_self_joint)
            {
                FixedJoint[] FJs = (GetComponents<FixedJoint>());
                for (int b = 0; b < FJs.Length; b++)
                {
                    Destroy(FJs[b]);
                }
            }
            else {
                for (int a = 0; a < components.Length; a++)
                {
                    components[a].SendMessage("setDestroyed", true, SendMessageOptions.DontRequireReceiver);
                    FixedJoint[] FJs = (components[a].GetComponents<FixedJoint>());
                    for (int b = 0; b < FJs.Length; b++)
                    {
                        Destroy(FJs[b]);
                    }
                    components[a].GetComponent<Rigidbody>().useGravity = true;
                }
            }
            
           
            parent.SendMessage("add_score", SendMessageOptions.DontRequireReceiver);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetCollapsePoint(float f){
        collapse_point = f;
    }
    public void setDestroyed(bool d)
    {
        destroyed = d;
    }
}
