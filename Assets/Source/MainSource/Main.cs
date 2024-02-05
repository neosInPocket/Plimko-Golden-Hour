using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
	[SerializeField] private BallsSpawner spawner;
	[SerializeField] private BallShooter ballShooter;
	[SerializeField] private Image fill;
	[SerializeField] private int addScore;
	[SerializeField] private Tutorial tutorial;
	[SerializeField] private TapSource tapSource;
	[SerializeField] private ResultSource resultSource;
	[SerializeField] private TMP_Text levelHolder;
	[SerializeField] private TMP_Text fillText;
	[SerializeField] private ObjectPool pool;
	private int goal => (int)(2 * Mathf.Log(SaveSystem.Document.gameLevel + 1) + 2);
	private int rewardGemns => (int)(3 * Mathf.Log(SaveSystem.Document.gameLevel + 1) + 3 + SaveSystem.Document.gameLevel);
	private int points;

	private void Start()
	{
		points = 0;
		levelHolder.text = "LEVEL " + SaveSystem.Document.gameLevel.ToString();

		fill.fillAmount = (float)points / (float)goal;
		fillText.text = $"{points}/{goal}";

		if (SaveSystem.Document.needTutor)
		{
			SaveSystem.Document.needTutor = false;
			SaveSystem.SetData();

			tutorial.StartAction(OnTutorialCompleted);
		}
		else
		{
			OnTutorialCompleted();
		}
	}

	private void OnTutorialCompleted()
	{
		tapSource.StartAction(OnTapCompleted);
	}

	public void OnColorMatch(bool value)
	{
		if (value)
		{
			points += addScore;
			if (points >= goal)
			{
				spawner.Spawning = false;
				ballShooter.Enabled = false;
				resultSource.StartResultAction(true, rewardGemns);
				pool.ClearPool();

				fill.fillAmount = (float)points / (float)goal;
				fillText.text = $"{points}/{goal}";

				SaveSystem.Document.currency += rewardGemns;
				SaveSystem.Document.gameLevel++;
				SaveSystem.SetData();
				return;
			}
		}
		else
		{
			spawner.Spawning = false;
			ballShooter.Enabled = false;
			resultSource.StartResultAction(false, 0);
			pool.ClearPool();
		}

		fill.fillAmount = (float)points / (float)goal;
		fillText.text = $"{points}/{goal}";
	}

	private void OnTapCompleted()
	{
		spawner.Spawning = true;
		ballShooter.Enabled = true;
	}

	public void Menu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void Retry()
	{
		SceneManager.LoadScene("GameMain");
	}
}
