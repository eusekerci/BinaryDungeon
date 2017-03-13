using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour {

    public CombatController Controller;
    //Temporary
    public bool p1;

    private Vector2 _combatInput;

    //Swipe Variables
    private float _startTime;
    private Vector2 _startPos;
    private readonly float _minSwipeDist = 0.1f;
    private readonly float _maxSwipeTime = 1f;
    private bool _isSwipeValid;

	void Start ()
    {
		
	}
	
	void Update ()
    {
#if UNITY_EDITOR
        _combatInput = KeyboardCheck();
        if (p1 && Input.GetKeyDown(KeyCode.Space))
        {
            Controller.SwitchMode();
        }
        else if (!p1 && Input.GetKeyDown(KeyCode.RightControl))
        {
            Controller.SwitchMode();
        }
#elif UNITY_ANDROID
    combatInput = SwipeCheck();
#endif

        if (_combatInput.x > 0)
            Controller.NormalizedAttack = Direction.Right;
        else if (_combatInput.x < 0)
            Controller.NormalizedAttack = Direction.Left;
        else
            Controller.NormalizedAttack = Direction.None;
    }

    private Vector2 SwipeCheck()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _isSwipeValid = true;
                    _startPos = touch.position;
                    _startTime = Time.time;
                    break;
                case TouchPhase.Stationary:
                    _isSwipeValid = false;
                    break;
                case TouchPhase.Ended:
                    float swipeTime = Time.time - _startTime;
                    float swipeDist = (touch.position - _startPos).magnitude;
                    if (_isSwipeValid && swipeTime < _maxSwipeTime && swipeDist > _minSwipeDist)
                    {
                        return (touch.position - _startPos).normalized;
                    }
                    break;
            }
        }
        else
        {
            _isSwipeValid = false;
        }

        return Vector2.zero;
    }

    private Vector2 KeyboardCheck()
    {
        if (p1)
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        }
        else
        {
            return new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2")).normalized;
        }
    }
}
