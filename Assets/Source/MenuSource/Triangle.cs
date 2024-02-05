using UnityEngine;

public class Triangle : MonoBehaviour
{
	[SerializeField] private ColorType colorType;

	public ColorType ColorType => colorType;
}

public enum ColorType
{
	Red,
	Blue
}
