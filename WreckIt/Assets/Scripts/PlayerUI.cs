using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public Button ReturnButton;

	// Use this for initialization
	void Start () {
        ReturnButton.onClick.AddListener(ClickReturnButton);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ClickReturnButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
    }
}
