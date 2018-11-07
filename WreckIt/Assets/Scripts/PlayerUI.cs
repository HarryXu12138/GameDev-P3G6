using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public Button ReturnButton;
    public Text timer;
    public GameObject result_window;
    public Text scoreText;
    public Text targetScoreText;
    public Text resultText;

    private float timer_number;

	// Use this for initialization
	void Start () {
        ReturnButton.onClick.AddListener(ClickReturnButton);
        timer_number = 5;
        result_window.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        timer_number -= Time.deltaTime;
        if (timer_number < 0) timer.text = "0.0";
        else timer.text = timer_number.ToString("n2");
        if (timer_number < 10) timer.color = Color.red;
        if (timer_number < 0)
        {
            result_window.SetActive(true);
            ReturnButton.gameObject.transform.SetParent(result_window.transform);
            ReturnButton.transform.localPosition = new Vector3(0, -50, 0);
            int score = Int32.Parse(scoreText.text);
            int targetScore = Int32.Parse(targetScoreText.text);
            if (score < targetScore)
            {
                resultText.text = "You Lose!";
            }
            else resultText.text = "You Win!";
        }
	}

    void ClickReturnButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
    }
}
