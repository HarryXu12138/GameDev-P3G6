using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mesh_prefab_test : MonoBehaviour {

    public float collapse_point;
    private GameObject[] children;
    public string child_tag;
    public int score;
    private GameObject player;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Main Camera");
        children = GameObject.FindGameObjectsWithTag(child_tag);
        for (int a = 0; a < children.Length; a++)
        {
            children[a].SendMessage("SetCollapsePoint", collapse_point);
            children[a].GetComponent<Rigidbody>().isKinematic = false;
            children[a].GetComponent<Rigidbody>().useGravity = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void add_score()
    {
        player.SendMessage("add_score", score, SendMessageOptions.DontRequireReceiver);
    }

}
