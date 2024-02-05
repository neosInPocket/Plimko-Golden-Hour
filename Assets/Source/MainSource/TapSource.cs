using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.UI;

public class TapSource : MonoBehaviour
{
	[SerializeField] private GameObject maincompleted;
	[SerializeField] private Image holder;
	[SerializeField] private float alpha;
	private Action OnTapCompleted;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void StartAction(Action onTapCompleted)
	{
		OnTapCompleted = onTapCompleted;

		var clr = holder.color;
		clr.a = alpha;
		holder.raycastTarget = true;

		if (maincompleted != null)
		{
			maincompleted.SetActive(true);
		}

		Touch.onFingerDown += OnTapHandler;
	}

	private void OnTapHandler(Finger finger)
	{
		Touch.onFingerDown -= OnTapHandler;
		OnTapCompleted();

		var clr = holder.color;
		clr.a = 0;
		holder.raycastTarget = false;

		maincompleted.SetActive(false);
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnTapHandler;
	}
}
