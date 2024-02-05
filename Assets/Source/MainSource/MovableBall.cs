using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovableBall : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private CircleCollider2D circleCollider2D;
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private Vector2 speeds;
	[SerializeField] private Vector2 sizes;
	[SerializeField] private GameObject destroyEffect;
	[SerializeField] private Material red;
	[SerializeField] private Material blue;
	[SerializeField] private TrailRenderer trailRenderer;
	private ColorType colorType;

	public Action<MovableBall> Destroyed { get; set; }
	private bool isDestroying;
	public bool Destroying => isDestroying;
	public Action<bool> IsColorMatch { get; set; }

	public void SetInitialized(Vector2 position)
	{
		var rnd = Random.Range(0, 2);
		isDestroying = false;
		colorType = rnd == 1 ? ColorType.Red : ColorType.Blue;
		spriteRenderer.material = rnd == 1 ? red : blue;
		trailRenderer.material = rnd == 1 ? red : blue;

		gameObject.SetActive(true);
		transform.position = position;
		circleCollider2D.enabled = true;
		rigid.constraints = RigidbodyConstraints2D.None;

		var randomSize = Random.Range(sizes.x, sizes.y);
		spriteRenderer.size = new Vector2(randomSize, randomSize);
		trailRenderer.startWidth = spriteRenderer.size.x;
		circleCollider2D.radius = spriteRenderer.size.x / 2;

		var randomX = Random.Range(-1f, 1f);
		var randomY = Random.Range(-1f, 1f);
		rigid.velocity = Random.Range(speeds.x, speeds.y) * new Vector2(randomX, randomY).normalized;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (isDestroying) return;
		circleCollider2D.enabled = false;
		rigid.constraints = RigidbodyConstraints2D.FreezeAll;

		if (collider.TryGetComponent<Edge>(out Edge edge))
		{
			isDestroying = true;
			StartCoroutine(DestroyEff());
			Destroyed?.Invoke(this);
			IsColorMatch?.Invoke(false);
			return;
		}

		if (collider.TryGetComponent<Triangle>(out Triangle triangle))
		{
			bool isColorMatch = false;

			if (triangle.ColorType == colorType)
			{
				isColorMatch = true;
			}
			else
			{
				isColorMatch = false;
			}

			IsColorMatch?.Invoke(isColorMatch);
			Destroyed?.Invoke(this);
			isDestroying = true;
			StartCoroutine(DestroyEff());
		}
	}

	private IEnumerator DestroyEff()
	{
		spriteRenderer.enabled = false;
		destroyEffect.SetActive(true);
		yield return new WaitForSeconds(1f);
		destroyEffect.SetActive(false);
		isDestroying = false;
		gameObject.SetActive(false);
		spriteRenderer.enabled = true;
	}
}
