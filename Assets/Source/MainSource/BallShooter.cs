using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BallShooter : MonoBehaviour
{
	[SerializeField] private LineRenderer lineRenderer;

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
	private Vector2 lastTapPosition;
	private Vector2 lastMovePosition;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void OnFingerDown(Finger finger)
	{
		lineRenderer.positionCount = 1;
		lastTapPosition = LevelExtensions.WorldPoint(finger.screenPosition);
		lastMovePosition = lastTapPosition;
		lineRenderer.SetPosition(0, lastTapPosition);
		lineRenderer.enabled = true;
	}

	private void OnFingerMove(Finger finger)
	{
		lineRenderer.positionCount++;
		var newMovePosition = LevelExtensions.WorldPoint(finger.screenPosition);
		lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector2(lastMovePosition.x - newMovePosition.x, 0));
	}

	private void OnFingerUp(Finger finger)
	{
		lineRenderer.enabled = false;
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnFingerDown;
		Touch.onFingerMove -= OnFingerMove;
		Touch.onFingerUp -= OnFingerUp;
	}
}
