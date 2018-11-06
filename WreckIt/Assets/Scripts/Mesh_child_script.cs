using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Mesh_child_script : MonoBehaviour
{
    private GameObject[] components;
    private Rigidbody rb;
    private GameObject da_ball;
    //public string group;
    private float collapse_point;
    private GameObject parent;
    private bool destroyed;
    public bool break_self_joint;

    //debris generation
    private GameObject debriPrefab;
    private GameObject bb;
    private int prefabNumbers;
    private float prefabDestroyTime;
    public float spread;
    public float shift;
    private float impulseMagnitude = 1.0f;
    //public float breakpoint;
    private float blowout_radius;
    private bool trigger = false;
    private float blowout_multiplier;
    //private GameObject timeControl = null;
    // Use this for initialization
    void Start()
    {
        destroyed = false;
        da_ball = GameObject.Find("da_ball");
        //components = GameObject.FindGameObjectsWithTag(group);
        rb = GetComponent<Rigidbody>();
        transform.parent = parent.transform;
        //transform.SetParent(parent.transform);
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.impulse.magnitude > collapse_point && !destroyed){
            destroyed = true;
            //Debug.Log(collision.impulse.magnitude);
            //transform.SetParent(null);
          
            FixedJoint[] FJs = (GetComponents<FixedJoint>());
            for (int b = 0; b < FJs.Length; b++)
            {
                //FJs[b].connectedBody.
                //Debug.Log("joint broke");
                Destroy(FJs[b]);
            }
            Vector3 force = collision.impulse;
            //force.Scale(new Vector3(-1f*impulseMagnitude, -1f * impulseMagnitude, -1f * impulseMagnitude));
            //force = Random.onUnitSphere * collision.impulse.magnitude;
            print(force);

            createDebris(force);
            Destroy(gameObject);
            print("Bulls Eyes");

            
            parent.SendMessage("Add_score", SendMessageOptions.DontRequireReceiver);
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
        rb2.AddTorque(new Vector3(Random.Range(0.0f, 100.0f), Random.Range(0.0f, 100.0f), Random.Range(0.0f, 100.0f)));
    }

    private void createDebris(Vector3 force)
    {
        Debug.Log("da");
        //timeControl.SendMessage("slowDown", SendMessageOptions.DontRequireReceiver);
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
            temp.transform.position = transform.position + new Vector3(0, 0.3f, 0);
            /*
            Debug.Log(transform.position);
            
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
            */
            //Vector3 blowout_up = force.normalized;
            Vector3 blowout_up = Vector3.up;
            Vector3 blowout_xz = Vector3.right * (Random.Range(0.0f, blowout_radius) * blowout_multiplier);
            blowout_xz = Quaternion.AngleAxis(Random.Range(0, 360.0f), Vector3.up) * blowout_xz;
            Vector3 blowout = blowout_up + blowout_xz;
            blowout.Normalize();
            impulseMagnitude = Random.Range(0.0f, force.magnitude) * blowout_multiplier;
            blowout *= impulseMagnitude;

            //tempF.Scale(new Vector3(1f * impulseMagnitude, 1f * impulseMagnitude, 1f * impulseMagnitude));
            //print(tempF);
            addForceToPrefab(temp, blowout);
            Destroy(temp, prefabDestroyTime);
            counter--;

        }
    }
    public void Set_BO_radius(float f)
    {
        blowout_radius = f;
    }
    public void Set_BO_mult(float f)
    {
        blowout_multiplier = f;
    }
    public void Set_prefab_num(int i)
    {
        prefabNumbers = i;
    }
    public void Set_prefab_des_t(float f)
    {
        prefabDestroyTime = f;
    }
    public void Set_debris_prefab(GameObject go)
    {
        debriPrefab = go;
    }
    /*
    public void Set_tc(GameObject go)
    {
        timeControl = go;
    }
    */
    public void Set_parent(GameObject go)
    {
        parent = go;
    }
}


