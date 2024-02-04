using UnityEngine;
using UnityEngine.UI;


public class OptionsHolder : MonoBehaviour
{
	[SerializeField] private Image onImageMusic;
	[SerializeField] private Image onImageEffect;
	private AudioHolder currentManager;

	private void Start()
	{
		currentManager = FindFirstObjectByType<AudioHolder>();
		bool music = SaveSystem.Document.mainVolume;
		bool effects = SaveSystem.Document.sideVolume;

		onImageMusic.enabled = music;
		onImageEffect.enabled = effects;
	}

	public void ToggleMusic()
	{
		bool enabled = currentManager.Toggle();
		onImageMusic.enabled = enabled;
	}

	public void ToggleSFX()
	{
		onImageEffect.enabled = !onImageEffect.enabled;
		SaveSystem.Document.sideVolume = onImageEffect.enabled;
		SaveSystem.SetData();
	}
}