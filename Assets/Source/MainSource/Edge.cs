using UnityEngine;

public class Edge : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;

	public Vector2 Size
	{
		get => spriteRenderer.size;
		set => spriteRenderer.size = value;
	}
}
