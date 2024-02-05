using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsSpawner : MonoBehaviour
{
	[SerializeField] private ObjectPool pool;
	[SerializeField] private Vector2 delays;
	[SerializeField] private float spawnRadius;

	public bool Spawning
	{
		get => m_isSpawning;
		set
		{
			m_isSpawning = value;
			if (!value)
			{
				StopAllCoroutines();
				m_isBusy = false;
			}
		}
	}

	private bool m_isSpawning;
	private bool m_isBusy;

	private void Update()
	{
		if (!m_isSpawning) return;
		if (m_isBusy) return;

		m_isBusy = true;

		Vector2 randomPostion = new Vector2(Random.Range(0, spawnRadius), Random.Range(0, spawnRadius));
		StartCoroutine(Spawn(randomPostion));
	}

	private IEnumerator Spawn(Vector2 position)
	{
		pool.Get(position);
		yield return new WaitForSeconds(Random.Range(delays.x, delays.y));
		m_isBusy = false;
	}
}
