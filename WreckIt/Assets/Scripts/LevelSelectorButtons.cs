﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorButtons : MonoBehaviour {
    public Button Level1Button;
    public Button Level2Button;
    public Button Level3Button;
    public Button GoBackButton;

	// Use this for initialization
	void Start () {
        Level1Button.onClick.AddListener(delegate { ButtonClicked(1); });
        Level2Button.onClick.AddListener(delegate { ButtonClicked(2); });
        Level2Button.onClick.AddListener(delegate { ButtonClicked(3); });
        GoBackButton.onClick.AddListener(ClickGoBackButton);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ClickGoBackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }

    void ButtonClicked(int buttonNo)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        /*if (buttonNo == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }*/
    }
}