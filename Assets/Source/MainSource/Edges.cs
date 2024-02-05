using UnityEngine;

public class Edges : MonoBehaviour
{
	[SerializeField] private Edge[] edges;
	[SerializeField] private float edgeWidth;
	[SerializeField] private float topEdge;


	private void Start()
	{
		var ss = LevelExtensions.ScreenSize;
		var topEdgeMinus = (ss.y * topEdge + ss.y) / 2;

		edges[0].transform.position = new Vector2(0, topEdgeMinus + edgeWidth / 4);
		edges[1].transform.position = new Vector2(0, -ss.y - edgeWidth / 2);
		edges[2].transform.position = new Vector2(ss.x + edgeWidth / 2, 0);
		edges[3].transform.position = new Vector2(-ss.x - edgeWidth / 2, 0);

		edges[0].Size = new Vector2(2 * ss.x, edgeWidth);
		edges[1].Size = new Vector2(2 * ss.x, edgeWidth);
		edges[2].Size = new Vector2(edgeWidth, 2 * ss.y);
		edges[3].Size = new Vector2(edgeWidth, 2 * ss.y);
	}
}
