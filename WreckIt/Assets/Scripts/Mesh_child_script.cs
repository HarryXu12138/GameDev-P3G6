using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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

    //debris generation
    public GameObject debriPrefab;
    private GameObject bb;
    public int prefabNumbers;
    public float prefabDestroyTime;
    public float spread;
    public float shift;
    public float impulseMagnitude = 1.0f;
    public float breakpoint;
    private bool trigger = false;
    // Use this for initialization
    void Start()
    {
        destroyed = false;
        da_ball = GameObject.Find("da_ball");
        components = GameObject.FindGameObjectsWithTag(group);
        rb = GetComponent<Rigidbody>();
        transform.parent = parent.transform;
        //transform.SetParent(parent.transform);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.impulse.magnitude > collapse_point && !destroyed){
            destroyed = true;
            Debug.Log(collision.impulse.magnitude);
            //transform.SetParent(null);
          
            FixedJoint[] FJs = (GetComponents<FixedJoint>());
            for (int b = 0; b < FJs.Length; b++)
            {
                //FJs[b].connectedBody.
                Debug.Log("joint broke");
                Destroy(FJs[b]);
            }
            Vector3 force = collision.impulse;
            //force.Scale(new Vector3(-1f*impulseMagnitude, -1f * impulseMagnitude, -1f * impulseMagnitude));
            //force = Random.onUnitSphere * collision.impulse.magnitude;
            print(force);

            createDebris(force);
            Destroy(gameObject);
            print("Bulls Eyes");

            
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

    private void addForceToPrefab(GameObject prefab, Vector3 force)
    {
        Rigidbody rb2 = prefab.GetComponent<Rigidbody>();
        rb2.AddForce(force, ForceMode.Impulse);
    }

    private void createDebris(Vector3 force)
    {
        int counter = prefabNumbers;
        /*
        int offset = 0;
        while((offset*2+1)*(offset*2+1) < counter)
        {
            offset += 1;
        }
        Vector3 startPoint = transform.position;
        startPoint.x += offset;
        startPoint.y += offset;
        startPoint.z += offset;
        */
        while (counter > 0)
        {
            GameObject temp = Instantiate<GameObject>(debriPrefab);
            Debug.Log(transform.position);
            temp.transform.position = parent.transform.position + transform.localPosition;
            //check the direction
            Vector3 tempF = force;
            //print(Mathf.Abs(tempF.x - 0f) < 0.01f);
            float maxPower = Mathf.Max(tempF.x, tempF.y, tempF.z);
            if (maxPower == tempF.x)
            {
                //print("work");
                if (Mathf.Max(tempF.z, tempF.y) == tempF.z)
                {
                    tempF.z += Random.Range(-spread + tempF.z * 10f, spread + tempF.z * 10f);
                    tempF.y += Random.Range(-spread, spread);
                    if (tempF.y < -10.0f)
                    {
                        tempF.y = -tempF.y;
                        if (tempF.y < 2.0f)
                        {
                            tempF.y = 2.0f;
                        }
                        if (tempF.y > tempF.z)
                        {
                            tempF.y = tempF.z;
                        }
                    }
                }
                else
                {
                    tempF.z += Random.Range(-spread, spread);
                    tempF.y += Random.Range(-spread + tempF.y * 10f, spread + tempF.y * 10f);
                    if (tempF.y < -10.0f)
                    {
                        tempF.y = -tempF.y;
                        if (tempF.y < 2.0f)
                        {
                            tempF.y = 2.0f;
                        }
                        if (tempF.y > tempF.z)
                        {
                            tempF.y = tempF.z;
                        }
                    }
                }



            }
            else if (maxPower == tempF.z)
            {
                if (Mathf.Max(tempF.x, tempF.y) == tempF.x)
                {
                    tempF.x += Random.Range(-spread + tempF.x * shift, spread + tempF.x * shift);
                    tempF.y += Random.Range(-spread, spread);
                    if (tempF.y < -10.0f)
                    {
                        tempF.y = -tempF.y;
                        if (tempF.y < 2.0f)
                        {
                            tempF.y = 2.0f;
                        }
                        if (tempF.y > tempF.z)
                        {
                            tempF.y = tempF.z;
                        }
                    }
                }
                else
                {
                    tempF.x += Random.Range(-spread, spread);
                    tempF.y += Random.Range(-spread + tempF.y * shift, spread + tempF.y * shift);
                    if (tempF.y < -10.0f)
                    {
                        tempF.y = -tempF.y;
                        if (tempF.y < 2.0f)
                        {
                            tempF.y = 2.0f;
                        }
                        if (tempF.y > tempF.z)
                        {
                            tempF.y = tempF.z;
                        }
                    }
                }

            }
            else
            {
                if (Mathf.Max(tempF.x, tempF.z) == tempF.x)
                {
                    tempF.z += Random.Range(-spread, spread);
                    tempF.x += Random.Range(-spread + tempF.x * shift, spread + tempF.x * shift);
                }
                else
                {
                    tempF.z += Random.Range(-spread + tempF.z * shift, spread + tempF.z * shift);
                    tempF.x += Random.Range(-spread, spread);
                }

            }

            if (tempF.y < -10.0f)
            {
                tempF.y = -tempF.y;
                if (tempF.y < 2.0f)
                {
                    tempF.y = 2.0f;
                }
                if (tempF.y > maxPower)
                {
                    tempF.y = maxPower;
                }
            }
            tempF.Scale(new Vector3(1f * impulseMagnitude, 1f * impulseMagnitude, 1f * impulseMagnitude));
            print(tempF);
            addForceToPrefab(temp, tempF);
            Destroy(temp, prefabDestroyTime);
            counter--;

        }
    }




}


