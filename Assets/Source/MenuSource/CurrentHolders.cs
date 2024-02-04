using TMPro;
using UnityEngine;

public class CurrentHolders : MonoBehaviour
{
	[SerializeField] private TMP_Text currentText;

	private void Start()
	{
		Refresh();
	}

	public void Refresh()
	{
		currentText.text = SaveSystem.Document.currency.ToString();
	}
}
