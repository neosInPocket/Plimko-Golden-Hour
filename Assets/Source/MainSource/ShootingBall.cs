using System;
using System.Collections;
using System.Drawing;
using UnityEngine;

public class ShootingBall : MonoBehaviour
{
	[SerializeField] private GameObject spawnEffect;
	[SerializeField] private CircleCollider2D circleCollider2D;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private GameObject destroyEffect;
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private float[] forces;
	[SerializeField] private float[] sizes;
	private float forceDecrease;

	private Action onEndAction;

	private void Start()
	{
		forceDecrease = forces[SaveSystem.Document.ballSpeed];
		var size = sizes[SaveSystem.Document.ballSize];
		spriteRenderer.size = new Vector2(size, size);
		circleCollider2D.radius = size / 2;
	}


	public void Shoot(Vector2 speed, Vector2 position, Action onEnd)
	{
		StopAllCoroutines();
		spriteRenderer.enabled = false;
		destroyEffect.gameObject.SetActive(false);
		spawnEffect.gameObject.SetActive(false);

		rigid.constraints = RigidbodyConstraints2D.None;
		onEndAction = onEnd;
		gameObject.SetActive(true);
		transform.position = position;
		StartCoroutine(Spawn(spawnEffect, null, OnSpawnStartEffects));
		rigid.velocity = speed / forceDecrease;
	}

	private IEnumerator Spawn(GameObject effect, Action onEnd, Action onStart)
	{
		if (onStart != null)
		{
			onStart();
		}

		effect.gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		effect.gameObject.SetActive(false);

		if (onEnd != null)
		{
			onEnd();
		}
	}

	private void OnSpawnStartEffects()
	{
		spriteRenderer.enabled = true;
	}

	private void OnDestroyStartEffects()
	{
		spriteRenderer.enabled = false;
		rigid.constraints = RigidbodyConstraints2D.FreezeAll;
	}

	private void OnDestroyEndEffects()
	{
		gameObject.SetActive(false);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		StartCoroutine(Spawn(destroyEffect, OnDestroyEndEffects, OnDestroyStartEffects));
		onEndAction();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		StartCoroutine(Spawn(destroyEffect, OnDestroyEndEffects, OnDestroyStartEffects));
		onEndAction();
	}
}
