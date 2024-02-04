using System.Linq;
using UnityEngine;

public class AudioHolder : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        var others = FindObjectsOfType<AudioHolder>();
        var mine = others.FirstOrDefault(x => x.gameObject.scene.name == "DontDestroyOnLoad");

        if (mine != null && mine != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        audioSource.enabled = SaveSystem.Document.mainVolume;
    }

    public bool Toggle()
    {
        audioSource.enabled = !audioSource.enabled;
        audioSource.enabled = SaveSystem.Document.mainVolume = audioSource.enabled;
        SaveSystem.SetData();
        return audioSource.enabled;
    }
}
