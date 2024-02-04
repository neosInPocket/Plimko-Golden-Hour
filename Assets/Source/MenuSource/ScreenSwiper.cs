using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSwiper : MonoBehaviour
{
	[SerializeField] private float speed;

	public void Swipe(int direction)
	{
		StopAllCoroutines();
		StartCoroutine(SwipeRoutine(direction));
	}
	private IEnumerator SwipeRoutine(int destination)
	{
		var goal = CalculateGoal(destination);
		if (goal == transform.position.x + float.Epsilon || goal == transform.position.x - float.Epsilon) yield break;
		var magnitude = Mathf.Abs(goal - transform.position.x) / Screen.width;
		var direction = (goal - transform.position.x) / magnitude;
		var currentPosition = transform.position;

		while (magnitude > 0)
		{
			currentPosition.x += direction * speed * magnitude * Time.deltaTime;
			transform.position = currentPosition;
			magnitude = Mathf.Abs(goal - transform.position.x) / Screen.width;
			yield return null;
		}

		currentPosition.y = CalculateGoal(destination);
		transform.position = currentPosition;
	}

	private int CalculateGoal(int mult)
	{
		return Screen.width * mult - Screen.width / 2;
	}

	public void LoadLevel()
	{
		SceneManager.LoadScene("Level");
	}
}
