using UnityEngine;

public class DisableEffectsVolume : MonoBehaviour
{
	[SerializeField] private AudioSource[] sorces;

	private void Start()
	{
		Enable();
	}

	private void Enable()
	{
		var enabled = SaveSystem.Document.sideVolume;

		foreach (var source in sorces)
		{
			source.enabled = enabled;
		}
	}
}
