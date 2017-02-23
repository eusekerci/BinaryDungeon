using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour {

    public CombatController controller;

    private Vector2 combatInput;

    //Swipe Variables
    private float startTime;
    private Vector2 startPos;
    private float minSwipeDist = 0.1f;
    private float maxSwipeTime = 1f;
    private bool isSwipeValid;

	void Start ()
    {
		
	}
	
	void Update ()
    {
#if UNITY_EDITOR
    combatInput = KeyboardCheck();
#elif UNITY_ANDROID
    combatInput = SwipeCheck();
#endif

        if (combatInput.x > 0)
            controller.normalizedAttack = Direction.Right;
        else if (combatInput.x < 0)
            controller.normalizedAttack = Direction.Left;
        else
            controller.normalizedAttack = Direction.None;
    }

    private Vector2 SwipeCheck()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isSwipeValid = true;
                    startPos = touch.position;
                    startTime = Time.time;
                    break;
                case TouchPhase.Stationary:
                    isSwipeValid = false;
                    break;
                case TouchPhase.Ended:
                    float swipeTime = Time.time - startTime;
                    float swipeDist = (touch.position - startPos).magnitude;
                    if (isSwipeValid && swipeTime < maxSwipeTime && swipeDist > minSwipeDist)
                    {
                        return (touch.position - startPos).normalized;
                    }
                    break;
            }
        }
        else
        {
            isSwipeValid = false;
        }

        return Vector2.zero;
    }

    private Vector2 KeyboardCheck()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }
}
