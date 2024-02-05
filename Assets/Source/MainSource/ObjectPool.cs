using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[SerializeField] private MovableBall prefab;
	[SerializeField] private List<MovableBall> startList;
	[SerializeField] private Main main;

	private List<MovableBall> currentList;

	private void Awake()
	{
		currentList = startList;
		foreach (var ball in currentList)
		{
			ball.IsColorMatch += OnColorMatch;
		}
	}

	public MovableBall Get(Vector2 position)
	{
		MovableBall simpleState = currentList.FirstOrDefault(x => !x.gameObject.activeSelf);

		if (simpleState != null)
		{
			simpleState.SetInitialized(position);
			return simpleState;
		}
		else
		{
			MovableBall returnable = Instantiate(prefab, position, Quaternion.identity, transform);
			currentList.Add(returnable);
			returnable.SetInitialized(position);
			returnable.IsColorMatch += OnColorMatch;
			return returnable;
		}
	}

	private void OnColorMatch(bool isColorMatch)
	{
		main.OnColorMatch(isColorMatch);
	}

	public void ClearPool()
	{
		foreach (var ball in currentList)
		{
			Destroy(ball.gameObject);
		}
	}

	private void OnDestroy()
	{
		foreach (var ball in currentList)
		{
			ball.IsColorMatch -= OnColorMatch;
		}
	}
}
