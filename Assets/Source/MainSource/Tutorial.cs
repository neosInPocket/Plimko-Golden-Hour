using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TMP_Text characterText;
    private Action TutorialCompleted;
    private void Start()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }

    public void StartAction(Action tutorialCompleted)
    {
        TutorialCompleted = tutorialCompleted;
        gameObject.SetActive(true);
        Touch.onFingerDown += Play;
        characterText.text = "WELCOME TO Plimko Golden Hour!";
    }

    private void Play(Finger finger)
    {
        Touch.onFingerDown -= Play;
        Touch.onFingerDown += Play1;
        characterText.text = "HIT THE FLYING BALLS SO THAT THEY FALL INTO THE SPIKE OF THE CORRESPONDING COLOR. HOW TO DO IT?";
    }

    private void Play1(Finger finger)
    {
        Touch.onFingerDown -= Play1;
        Touch.onFingerDown += Play2;
        characterText.text = "HOLD THE SCREEN, AIM AND RELEASE TO LAUNCH YOUR BALL!";
    }

    private void Play2(Finger finger)
    {
        Touch.onFingerDown -= Play2;
        Touch.onFingerDown += Play3;
        characterText.text = "COMPLETE LEVELS, COLLECT COINS AND BUY VARIOUS UPGRADES IN THE STORE!";
    }

    private void Play3(Finger finger)
    {
        Touch.onFingerDown -= Play3;
        Touch.onFingerDown += PlatEnd;
        characterText.text = "DO YOU HAVE ENOUGH AGILITY TO PASS ALL LEVELS? LET'S FIND OUT!";
    }

    private void PlatEnd(Finger finger)
    {
        Touch.onFingerDown -= PlatEnd;
        TutorialCompleted();

        if (gameObject != null)
        {
            gameObject.SetActive(false);
        }
    }
}
