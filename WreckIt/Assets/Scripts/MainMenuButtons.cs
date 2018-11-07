using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour {
    public Button StartButton;

	// Use this for initialization
	void Start () {
        StartButton.onClick.AddListener(ClickStartButton);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ClickStartButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
    }
}
