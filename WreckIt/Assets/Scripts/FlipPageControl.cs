using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipPageControl : MonoBehaviour {
    public Button nextPage;
    public Button prevPage;
    public int flipSpeed = 5;

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
        bookPages[flippingPageIndex].transform.SetSiblingIndex(bookPages.Count - 1);
        bookPages[flippingPageIndex].transform.rotation = Quaternion.Euler(0, 0, 0);
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
        flipping = true;
        flippingPageDirection = false;
        currentPageIndex -= 1;
        flippingPageIndex = currentPageIndex;
        bookPages[flippingPageIndex].transform.SetSiblingIndex(bookPages.Count - 1);
        bookPages[flippingPageIndex].transform.eulerAngles = new Vector3(0, 180, 0);
        if (flippingPageIndex - 1 >= 0)
        {
            bookPages[flippingPageIndex - 1].SetActive(true);
            bookPages[flippingPageIndex - 1].transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void initializeBookPages()
    {
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
        if (flipping)
        {
            if (flippingPageDirection)
            {
                bookPages[flippingPageIndex].transform.Rotate(0, flipSpeed * Time.deltaTime, 0);
            } else
            {
                bookPages[flippingPageIndex].transform.Rotate(0, -flipSpeed * Time.deltaTime, 0);
            }
            if (bookPages[flippingPageIndex].transform.eulerAngles.y > 180 || bookPages[flippingPageIndex].transform.eulerAngles.y < 0)
            {
                flipping = false;
                if (flippingPageDirection)
                {
                    if (flippingPageIndex - 1 >= 0)
                    {
                        bookPages[flippingPageIndex - 1].SetActive(false);
                        bookPages[flippingPageIndex - 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                if (!flippingPageDirection)
                {
                    bookPages[flippingPageIndex + 1].SetActive(false);
                }
            }
            Debug.Log(bookPages[flippingPageIndex].transform.eulerAngles.y);
            if (flippingPageDirection && bookPages[flippingPageIndex].transform.eulerAngles.y > 90)
            {
                bookPages[flippingPageIndex].transform.Find("FrontPage").gameObject.SetActive(false);
                bookPages[flippingPageIndex].transform.Find("BehindPage").gameObject.SetActive(true);
            } else if (!flippingPageDirection && bookPages[flippingPageIndex].transform.eulerAngles.y < 90)
            {
                bookPages[flippingPageIndex].transform.Find("FrontPage").gameObject.SetActive(true);
                bookPages[flippingPageIndex].transform.Find("BehindPage").gameObject.SetActive(false);
            }
        }
    }
}
