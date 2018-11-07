using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipPageControl : MonoBehaviour {
    public Button nextPage;
    public Button prevPage;
    public int flipSpeed = 5;

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
        bookPages[flippingPageIndex].transform.rotation = Quaternion.Euler(0, 0, 0);
        currentPageIndex += 1;
        bookPages[currentPageIndex].SetActive(true);
        bookPages[currentPageIndex].transform.Find("BehindPage").gameObject.SetActive(false);
        bookPages[currentPageIndex].transform.Find("FrontPage").gameObject.SetActive(false);
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
        bookPages[flippingPageIndex].transform.rotation = Quaternion.Euler(0, 178, 0);
        if (flippingPageIndex - 1 >= 0)
        {
            bookPages[flippingPageIndex - 1].SetActive(true);
            bookPages[flippingPageIndex - 1].transform.Find("FrontPage").gameObject.SetActive(false);
            bookPages[flippingPageIndex - 1].transform.Find("BehindPage").gameObject.SetActive(false);
            bookPages[flippingPageIndex - 1].transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void initializeBookPages()
    {
        List<string> bookPageNames = new List<string>();
        bookPageNames.Add("Page1");
        bookPageNames.Add("Page2");
        bookPageNames.Add("Page3");
        bookPageNames.Add("Page4");
        bookPageNames.Add("Page5");

        bookPages = new List<GameObject>();
        for (int i = 0; i < bookPageNames.Count; ++i)
        {
            GameObject page = GameObject.Find(bookPageNames[i]);
            page.SetActive(false);
            page.transform.localPosition = new Vector3(0, 0, 0);
            bookPages.Add(page);
        }
        bookPages[0].SetActive(true);
        bookPages[0].transform.Find("BehindPage").gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        flippingUpdate();
        if (currentPageIndex == bookPages.Count - 1) nextPage.gameObject.SetActive(false);
        else if (currentPageIndex == 0) prevPage.gameObject.SetActive(false);
        else
        {
            nextPage.gameObject.SetActive(true);
            prevPage.gameObject.SetActive(true);
        }
    }

    private void flippingUpdate()
    {
        if (flipping)
        {
            if (bookPages[flippingPageIndex].transform.eulerAngles.y > 178 || bookPages[flippingPageIndex].transform.eulerAngles.y < 0)
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
        }
        if (flipping)
        {
            if (flippingPageDirection)
            {
                bookPages[flippingPageIndex].transform.Rotate(0, flipSpeed * Time.deltaTime, 0);
                if (bookPages[flippingPageIndex].transform.eulerAngles.y > 88)
                {
                    bookPages[flippingPageIndex].transform.Find("FrontPage").gameObject.SetActive(false);
                    bookPages[flippingPageIndex].transform.Find("BehindPage").gameObject.SetActive(true);
                    bookPages[flippingPageIndex + 1].transform.Find("FrontPage").gameObject.SetActive(true);
                    if (flippingPageIndex - 1 >= 0) bookPages[flippingPageIndex - 1].transform.Find("BehindPage").gameObject.SetActive(false);
                }
            }
            else
            {
                bookPages[flippingPageIndex].transform.Rotate(0, -flipSpeed * Time.deltaTime, 0);
                if (bookPages[flippingPageIndex].transform.eulerAngles.y < 92)
                {
                    bookPages[flippingPageIndex].transform.Find("FrontPage").gameObject.SetActive(true);
                    bookPages[flippingPageIndex].transform.Find("BehindPage").gameObject.SetActive(false);
                    if (flippingPageIndex - 1 >= 0) bookPages[flippingPageIndex - 1].transform.Find("BehindPage").gameObject.SetActive(true);
                    if (flippingPageIndex + 1 < bookPages.Count) bookPages[flippingPageIndex + 1].transform.Find("FrontPage").gameObject.SetActive(false);
                }
            }
        }
    }
}
