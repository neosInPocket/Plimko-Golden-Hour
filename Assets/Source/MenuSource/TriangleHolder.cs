using UnityEngine;

public class TriangleHolder : MonoBehaviour
{
	[SerializeField] private float topPosition;
	[SerializeField] private float bottomPosition;
	[SerializeField] private Triangle[] triangles;

	private void Start()
	{
		var screenSize = LevelExtensions.ScreenSize;

		triangles[0].transform.position = new Vector2(-screenSize.x, 2 * screenSize.y * topPosition - screenSize.y);
		triangles[1].transform.position = new Vector2(screenSize.x, 2 * screenSize.y * topPosition - screenSize.y);
		triangles[2].transform.position = new Vector2(-screenSize.x, 2 * screenSize.y * bottomPosition - screenSize.y);
		triangles[3].transform.position = new Vector2(screenSize.x, 2 * screenSize.y * bottomPosition - screenSize.y);
	}
}
