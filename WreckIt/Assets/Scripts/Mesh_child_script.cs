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
    private float spread = 17.0f;
    private float shift = 1f;
    private float impulseMagnitude = 1.6f;
    //public float breakpoint;
    private float blowout_radius;
    private bool trigger = false;
    private float blowout_multiplier;
    //private GameObject timeControl = null;

    private GameObject audio;
    // Use this for initialization
    void Start()
    {
        parent = transform.parent.gameObject;
        destroyed = false;
        da_ball = GameObject.Find("da_ball");
        audio = GameObject.Find("Audio Controller");
        //components = GameObject.FindGameObjectsWithTag(group);
        rb = GetComponent<Rigidbody>();

        //transform.SetParent(parent.transform);
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.impulse.magnitude > collapse_point && !destroyed){
            destroyed = true;
            int sound_id = (int)Random.Range(0.0f, 13.1f);
            audio.SendMessage("PlayHitSound", sound_id);
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
        //Debug.Log("da");
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
            
            //Debug.Log(transform.position);
            
            //check the direction
            Vector3 tempF = force;
            //print(Mathf.Abs(tempF.x - 0f) < 0.01f);
            float maxPower = Mathf.Max(Mathf.Abs(tempF.x), Mathf.Abs(tempF.y), Mathf.Abs(tempF.z));
            if (maxPower == Mathf.Abs(tempF.x))
            {
                if (Mathf.Max(Mathf.Abs(tempF.z), Mathf.Abs(tempF.y)) == Mathf.Abs(tempF.z))
                {
                    // print("work");
                    tempF.z += Random.Range(-spread + tempF.z , spread + tempF.z );
                    tempF.y += Random.Range(0, spread*1.25f);
                    if (tempF.y < 2.7f)
                    {
                        tempF.y = 2.7f;
                        
                    }
                    
                }
                else
                {
                    //print("work");
                    tempF.z += Random.Range(-spread, spread);
                    tempF.y += Random.Range(0,spread*1.25f);
                    if (tempF.y < 2.7f)
                    {
                        tempF.y = 2.7f;
                        
                    }
                    
                }



            }
            else if (maxPower == Mathf.Abs(tempF.z))
            {
                if (Mathf.Max(Mathf.Abs(tempF.x), Mathf.Abs(tempF.y)) == Mathf.Abs(tempF.x))
                {
                    //print("work");
                    tempF.x += Random.Range(-spread + tempF.x , spread + tempF.x );
                    tempF.y += Random.Range(0, spread * 1.25f);
                    if (tempF.y < 2.7f)
                    {
                        tempF.y = 2.7f;

                    }

                }
                else
                {
                    //print("work");
                    tempF.x += Random.Range(-spread, spread);
                    tempF.y += Random.Range(0, spread * 1.25f);
                    if (tempF.y < 2.7f)
                    {
                        tempF.y = 2.7f;

                    }

                }

            }
            else
            {
                //print("work");
                if (Mathf.Max(Mathf.Abs(tempF.x), Mathf.Abs(tempF.z)) == Mathf.Abs(tempF.x))
                {
                    tempF.z += Random.Range(-spread, spread);
                    tempF.x += Random.Range(-spread + tempF.x  , spread + tempF.x );
                }
                else
                {
                    tempF.z += Random.Range(-spread + tempF.z , spread + tempF.z );
                    tempF.x += Random.Range(-spread, spread);
                }
                if (tempF.y < 2.7f)
                {
                    tempF.y = 2.7f;

                }
                
            }

           
            
            /*
            //Vector3 blowout_up = force.normalized;
            Vector3 blowout_up = Vector3.up;
            Vector3 blowout_xz = Vector3.right * (Random.Range(0.0f, blowout_radius) * blowout_multiplier);
            blowout_xz = Quaternion.AngleAxis(Random.Range(0, 360.0f), Vector3.up) * blowout_xz;
            Vector3 blowout = blowout_up + blowout_xz;
            blowout.Normalize();
            impulseMagnitude = Random.Range(0.0f, force.magnitude) * blowout_multiplier;
            blowout *= impulseMagnitude;
            */
            tempF.Scale(new Vector3(1f * impulseMagnitude, 1f * impulseMagnitude*1.5f, 1f * impulseMagnitude));
            print(counter + ": " + tempF);
            addForceToPrefab(temp, tempF);
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
    /*
    public void Set_parent(GameObject go)
    {
        parent = go;
    }
    */
}


