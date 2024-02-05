using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BallShooter : MonoBehaviour
{
	[SerializeField] private LineRenderer lineRenderer;
	[SerializeField] private ShootingBall shootingBall;

	public bool Enabled
	{
		get => isEnabled;
		set
		{
			isEnabled = value;
			if (value)
			{
				Touch.onFingerDown += OnFingerDown;
				Touch.onFingerMove += OnFingerMove;
				Touch.onFingerUp += OnFingerUp;
			}
			else
			{
				Touch.onFingerDown -= OnFingerDown;
				Touch.onFingerMove -= OnFingerMove;
				Touch.onFingerUp -= OnFingerUp;
			}
		}
	}

	private bool isEnabled;
	private bool locked;
	private bool started;
	private Vector3 zeroPosition;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void OnFingerDown(Finger finger)
	{
		if (locked) return;

		started = true;
		lineRenderer.positionCount = 2;
		var lastTapPosition = LevelExtensions.WorldPoint(finger.screenPosition);
		lineRenderer.transform.position = lastTapPosition;
		lineRenderer.SetPosition(0, lastTapPosition);
		lineRenderer.SetPosition(1, lastTapPosition);
		zeroPosition = lastTapPosition;
	}

	private void OnFingerMove(Finger finger)
	{
		if (locked) return;
		if (!started) return;

		var newMovePosition = LevelExtensions.WorldPoint(finger.screenPosition);

		var delta = zeroPosition - newMovePosition;
		lineRenderer.SetPosition(1, zeroPosition + delta);
	}

	private void OnFingerUp(Finger finger)
	{
		if (locked) return;
		if (!started) return;

		locked = true;
		var firstPosition = lineRenderer.GetPosition(0);
		var secondPosition = lineRenderer.GetPosition(1);

		shootingBall.Shoot(secondPosition - firstPosition, firstPosition, Unlock);
		lineRenderer.positionCount = 0;
	}

	private void Unlock()
	{
		locked = false;
		started = false;
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnFingerDown;
		Touch.onFingerMove -= OnFingerMove;
		Touch.onFingerUp -= OnFingerUp;
	}
}
