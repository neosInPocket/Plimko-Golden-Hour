using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSource : MonoBehaviour
{
	[SerializeField] private TMP_Text goalRewardText;
	[SerializeField] private TMP_Text text;
	[SerializeField] private TMP_Text btnCaption;
	[SerializeField] private Animator controller;
	private Action OnClose;

	public void StartResultAction(bool win, int coinsAdded)
	{
		gameObject.SetActive(true);
		string buttonCaption = default;
		string rewardCaption = default;
		string resultCaption = default;

		if (win)
		{
			buttonCaption = "NEXT LEVEL";
			resultCaption = "YOU WIN!";
		}
		else
		{
			buttonCaption = "RETRY";
			resultCaption = "LOSE";
		}

		rewardCaption = "+" + coinsAdded.ToString();

		goalRewardText.text = rewardCaption;
		text.text = resultCaption;
		btnCaption.text = buttonCaption;
	}

	public void MenuAction()
	{
		controller.SetTrigger("close");
		OnClose = () => SceneManager.LoadScene("EntryScene");
	}

	public void LevelAction()
	{
		controller.SetTrigger("close");
		OnClose = () => SceneManager.LoadScene("Level");
	}

	public void HideAction()
	{
		gameObject.SetActive(false);
		OnClose();
	}
}
