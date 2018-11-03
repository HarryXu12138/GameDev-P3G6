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
        initializeBookPages();
	}

    private void initializeBookPages()
    {
        bookPageNames = new List<string>();
        bookPageNames.Add("Page1");
        bookPageNames.Add("Page2");

        bookPages = new List<GameObject>();
        for (int i = 0; i < bookPageNames.Count; ++i)
        {
            GameObject page = GameObject.Find(bookPageNames[i]);
            page.transform.
            bookPages.Add(GameObject.Find(bookPageNames[i]));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
