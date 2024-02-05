using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgraderHolder : MonoBehaviour
{
	[SerializeField] private GameObject status;
	[SerializeField] private Button button;
	[SerializeField] private TMP_Text priceText;
	[SerializeField] private int price;
	[SerializeField] private Upgrades upgradeType;
	[SerializeField] private List<Image> points;
	public int Price => price;

	private void Start()
	{
		priceText.text = price.ToString();
	}

	public void Refresh()
	{
		int pointer = 0;
		bool buttonInteractable = false;
		bool upgraded = false;

		if (upgradeType == Upgrades.First)
		{
			pointer = SaveSystem.Document.ballSpeed;
			buttonInteractable = SaveSystem.Document.ballSpeed < 3 && SaveSystem.Document.currency >= price;
			upgraded = SaveSystem.Document.ballSpeed >= 3;
		}
		else
		{
			pointer = SaveSystem.Document.ballSize;
			buttonInteractable = SaveSystem.Document.ballSize < 3 && SaveSystem.Document.currency >= price;
			upgraded = SaveSystem.Document.ballSize >= 3;
		}

		button.interactable = buttonInteractable;
		status.SetActive(!buttonInteractable);

		if (upgraded)
		{
			status.SetActive(false);
		}
		points.ForEach(x => x.enabled = false);
		for (int i = 0; i < pointer; i++)
		{
			points[i].enabled = true;
		}
	}
}

public enum Upgrades
{
	First,
	Second
}
