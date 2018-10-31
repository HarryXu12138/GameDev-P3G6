using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour {
    public Button StartButton;
    public Button HelpButton;

	// Use this for initialization
	void Start () {
        StartButton.onClick.AddListener(ClickStartButton);
        HelpButton.onClick.AddListener(ClickHelpButton);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ClickHelpButton()
    {
        // UnityEngine.SceneManagement.SceneManager.LoadScene("");
        Debug.Log("Enter Help Page.");
    }

    void ClickStartButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
