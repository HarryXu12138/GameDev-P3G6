using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuStartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ClickStartButton() {
        // UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName("SampleScene");
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
