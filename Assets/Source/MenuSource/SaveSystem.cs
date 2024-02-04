using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	[SerializeField] private bool removePrev;
	private static string saveFilePath => Application.persistentDataPath + "/SaveSystemInfo.json";
	public static SaveDocument Document { get; private set; }

	private void Awake()
	{
		if (removePrev)
		{
			Document = new SaveDocument();
			SetData();
		}
		else
		{
			GetSettings();
		}
	}

	public static void SetData()
	{
		if (!File.Exists(saveFilePath))
		{
			CreateNewSaveFile();
		}
		else
		{
			WriteDataFile();
		}
	}

	public static void GetSettings()
	{
		if (!File.Exists(saveFilePath))
		{
			CreateNewSaveFile();
		}
		else
		{
			string text = File.ReadAllText(saveFilePath);
			Document = JsonUtility.FromJson<SaveDocument>(text);
		}
	}

	private static void CreateNewSaveFile()
	{
		Document = new SaveDocument(); ;
		File.WriteAllText(saveFilePath, JsonUtility.ToJson(Document));
	}

	private static void WriteDataFile()
	{
		File.WriteAllText(saveFilePath, JsonUtility.ToJson(Document));
	}
}
