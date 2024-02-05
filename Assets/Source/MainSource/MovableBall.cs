using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovableBall : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private Vector2 speeds;
	[SerializeField] private Vector2 sizes;
	[SerializeField] private GameObject destroyEffect;

	public Action Destroyed { get; set; }
	private bool isDestroying;

	public void SetInitialized(Vector2 position)
	{
		gameObject.SetActive(true);
		transform.position = position;

		var randomSize = Random.Range(sizes.x, sizes.y);
		spriteRenderer.size = new Vector2(randomSize, randomSize);

		var randomX = Random.Range(-1f, 1f);
		var randomY = Random.Range(-1f, 1f);
		rigid.velocity = Random.Range(speeds.x, speeds.y) * new Vector2(randomX, randomY).normalized;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (isDestroying) return;

		if (collider.TryGetComponent<Edge>(out Edge edge))
		{
			isDestroying = true;
			StartCoroutine(DestroyEff());
		}
	}

	private IEnumerator DestroyEff()
	{
		spriteRenderer.enabled = false;
		destroyEffect.SetActive(true);
		yield return new WaitForSeconds(1f);
		isDestroying = false;
		gameObject.SetActive(false);
		spriteRenderer.enabled = true;
	}
}
