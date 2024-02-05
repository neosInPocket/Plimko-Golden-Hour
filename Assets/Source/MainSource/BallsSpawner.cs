using UnityEngine;

public class BallsSpawner : MonoBehaviour
{
	[SerializeField] private ObjectPool pool;
	[SerializeField] private float spawnRadius;

	public bool Spawning
	{
		get => m_isSpawning;
		set
		{
			m_isSpawning = value;
			if (value)
			{
				Shoot();
			}
		}
	}

	private bool m_isSpawning;

	private void Shoot()
	{
		if (!m_isSpawning) return;
		var movable = pool.Get(Vector2.zero);
		movable.Destroyed += OnMovableDestroyed;
	}

	private void OnMovableDestroyed(MovableBall movableBall)
	{
		movableBall.Destroyed -= OnMovableDestroyed;
		Shoot();
	}
}
