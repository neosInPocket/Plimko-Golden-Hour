using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
	[SerializeField] private BallsSpawner spawner;
	[SerializeField] private BallShooter ballShooter;
	[SerializeField] private List<Image> energy;
	[SerializeField] private Image fill;
	[SerializeField] private int addScore;
	// [SerializeField] private LevelTime timer;
	// [SerializeField] private TutorialManager tutorialManagment;
	// [SerializeField] private CountinueFrame countinueFrame;
	// [SerializeField] private ResultAction resultShow;
	[SerializeField] private TMP_Text levelHolder;
	[SerializeField] private TMP_Text fillText;
	private int goalScore => (int)(2 * Mathf.Log(SaveSystem.Document.gameLevel + 1) + 11);
	private int reward => (int)(3 * Mathf.Log(SaveSystem.Document.gameLevel + 1) + 3 + SaveSystem.Document.gameLevel);
	private int currentScore;

	private void Start()
	{
		spawner.Spawning = true;
		ballShooter.Enabled = true;


		currentScore = 0;
		levelHolder.text = "LEVEL " + SaveSystem.Document.gameLevel.ToString();

		fill.fillAmount = (float)currentScore / (float)goalScore;
		fillText.text = $"{currentScore}/{goalScore}";

		if (SaveSystem.Document.needTutor)
		{
			SaveSystem.Document.needTutor = false;
			SaveSystem.SetData();
		}
		else
		{
			TutorialEnd();
		}
	}

	private void TutorialEnd()
	{

	}

	private void OnTap()
	{

	}

	private void OnTargetHit()
	{
		currentScore += addScore;

		if (currentScore >= goalScore)
		{
			currentScore = goalScore;
			DisableAll();
		}

		fill.fillAmount = (float)currentScore / (float)goalScore;
		fillText.text = $"{currentScore}/{goalScore}";
	}

	private void OnTimerEnd()
	{
		DisableAll();

	}

	private void OnPlayerTakeDamage(int lifesLeft)
	{
		if (lifesLeft == 0)
		{
			DisableAll();
		}

		RefreshEnergyPoints(lifesLeft);
	}

	private void RefreshEnergyPoints(int lifes)
	{
		energy.ForEach(x => x.enabled = false);
		for (int i = 0; i < lifes; i++)
		{
			energy[i].enabled = true;
		}
	}

	private void DisableAll()
	{

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
