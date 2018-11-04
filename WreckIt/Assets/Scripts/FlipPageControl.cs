using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipPageControl : MonoBehaviour {
    public Button nextPage;
    public Button prevPage;
    public int flipSpeed = 5;

    private Vector3 rotationAxis;
    private List<string> bookPageNames;
    private List<GameObject> bookPages;
    private int currentPageIndex = 0;
    private int flippingPageIndex = 0;
    private bool flipping = false;
    private bool flippingPageDirection;

	// Use this for initialization
	void Start () {
        initializeBookPages();
        initializeButtons();
	}

    private void initializeButtons()
    {
        nextPage.onClick.AddListener(clickNextPage);
        prevPage.onClick.AddListener(clickPrevPage);
    }

    private void clickNextPage()
    {
        if (flipping) return;
        if (currentPageIndex + 1 >= bookPages.Count)
        {
            Debug.Log("Final page!");
            return;
        }
        flipping = true;
        flippingPageDirection = true;
        flippingPageIndex = currentPageIndex;
        bookPages[flippingPageIndex].transform.position += new Vector3(0.0f, 0.0f, 1.0f);
        currentPageIndex += 1;
        bookPages[currentPageIndex].SetActive(true);
    }

    private void clickPrevPage()
    {
        if (flipping) return;
        if (currentPageIndex - 1 < 0)
        {
            Debug.Log("First page!");
            return;
        }
        Debug.Log(currentPageIndex);
        flipping = true;
        flippingPageDirection = false;
        currentPageIndex -= 1;
        flippingPageIndex = currentPageIndex;
        bookPages[flippingPageIndex].transform.eulerAngles = new Vector3(0, 180, 0);
        bookPages[flippingPageIndex].SetActive(true);
    }

    private void initializeBookPages()
    {
        rotationAxis = GameObject.Find("Pages").transform.position;
        bookPageNames = new List<string>();
        bookPageNames.Add("Page1");
        bookPageNames.Add("Page2");
        bookPageNames.Add("Page3");

        bookPages = new List<GameObject>();
        for (int i = 0; i < bookPageNames.Count; ++i)
        {
            GameObject page = GameObject.Find(bookPageNames[i]);
            page.SetActive(false);
            bookPages.Add(page);
        }
        bookPages[0].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        // bookPages[0].transform.Rotate(0, 5, 0);
        if (flipping)
        {
            if (flippingPageDirection)
            {
                bookPages[flippingPageIndex].transform.Rotate(0, flipSpeed * Time.deltaTime, 0);
            } else
            {
                bookPages[flippingPageIndex].transform.Rotate(0, -flipSpeed * Time.deltaTime, 0);
            }
            Debug.Log(bookPages[flippingPageIndex].transform.rotation.y);
            if (bookPages[flippingPageIndex].transform.eulerAngles.y > 180 || bookPages[flippingPageIndex].transform.eulerAngles.y < 0)
            {
                flipping = false;
                if (flippingPageDirection)
                {
                    bookPages[flippingPageIndex].SetActive(false);
                    bookPages[flippingPageIndex].transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (!flippingPageDirection)
                {
                    bookPages[flippingPageIndex + 1].SetActive(false);
                }
            }
        }
    }
}
