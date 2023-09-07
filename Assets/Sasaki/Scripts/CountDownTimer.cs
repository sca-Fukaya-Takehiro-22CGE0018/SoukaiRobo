using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
	public int CountDownMinutes;
	public bool isGame;
	public float CountDownSeconds;

	public Text timeText;
	private ScoreManager scoreManager;

	void Start()
	{
		isGame = true;
		timeText = GetComponent<Text>();
		CountDownMinutes = 3;
		CountDownSeconds = CountDownMinutes * 60;
		scoreManager = FindObjectOfType<ScoreManager>();
	}

	void Update()
	{
		if (isGame)
		{
			CountDownSeconds -= Time.deltaTime;
		}
		var span = new TimeSpan(0, 0, (int)CountDownSeconds);
		timeText.text = span.ToString(@"mm\:ss");

		if (CountDownSeconds <= 0)
        {
			SceneManager.LoadScene("ResultScene");
        }
	}

	public void StopTimer()
	{
		isGame = false;
		scoreManager.TimeScoreCalculation();
	}
}