using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipPageControl : MonoBehaviour {
    public Button nextPage;
    public Button prevPage;
    public List<string> bookPageNames;

    private List<GameObject> bookPages;
    private int currentPage;
    private bool flipping = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
