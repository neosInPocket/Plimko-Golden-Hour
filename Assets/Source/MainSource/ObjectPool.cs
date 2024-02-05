using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[SerializeField] private MovableBall prefab;
	[SerializeField] private List<MovableBall> startList;

	private List<MovableBall> currentList;

	private void Start()
	{
		currentList = startList;
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
			return returnable;
		}
	}
}
