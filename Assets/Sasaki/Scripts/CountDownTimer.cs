using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
	public int CountDownMinutes;
	private float CountDownSeconds;

	public Text timeText;

	void Start()
	{
		timeText = GetComponent<Text>();
		CountDownMinutes = 3;
		CountDownSeconds = CountDownMinutes * 60;
	}

	void Update()
	{
		CountDownSeconds -= Time.deltaTime;
		var span = new TimeSpan(0, 0, (int)CountDownSeconds);
		timeText.text = span.ToString(@"mm\:ss");

		if (CountDownSeconds <= 0)
        {
			SceneManager.LoadScene("ResultScene");
        }
	}
}