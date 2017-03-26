using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour {

    public CombatController Controller;

    private Vector2 _combatInput;

    //Swipe Variables
    private float _startTime;
    private Vector2 _startPos;
    private readonly float _minSwipeDist = 0.05f;
    private readonly float _maxSwipeTime = 1f;
    private bool _isSwipeValid;
	
	void Update ()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        //Alternative if
        //if (Input.mousePosition.y < Screen.height / 2.0f)

        //Alternative if #2
        //if (Input.GetButton("DefendLeft") || Input.GetButton("DefendRight"))

        //if (Input.GetButton("DefendActivate"))
        //if (Input.GetButton("DefendLeft") || Input.GetButton("DefendRight"))
        if (Input.GetButton("DefendLeft") || Input.GetButton("DefendRight"))
        {
            Controller.DefenseInput = true;
            if (Input.GetButton("DefendLeft"))
            {
                Controller.DoDefend(Direction.Left);
            }
            else if (Input.GetButton("DefendRight"))
            {
                Controller.DoDefend(Direction.Right);
            }
        }
        else
        {
            Controller.DefenseInput = false;
            if (Input.GetButtonDown("AttackLeft"))
            {
                Controller.DoAttack(Direction.Left);
            }
            else if (Input.GetButtonDown("AttackRight"))
            {
                Controller.DoAttack(Direction.Right);
            }
        }
#endif
#if UNITY_ANDROID
        //_combatInput = SwipeCheck();
        //if(_combatInput != Vector2.zero)
        //{
        //    if (_startPos.y > Screen.height / 2.0f)
        //    {
        //        Controller.DefenseInput = false;
        //        if (_combatInput.x > 0)
        //        {
        //            Controller.DoAttack(Direction.Left);
        //        }
        //        else
        //        {
        //            Controller.DoAttack(Direction.Right);
        //        }
        //    }
        //    else
        //    {
        //        Controller.DefenseInput = true;
        //        if (_combatInput.x > 0)
        //        {
        //            Controller.DoDefend(Direction.Right);
        //        }
        //        else
        //        {
        //            Controller.DoDefend(Direction.Left);
        //        }
        //    }
        //}
#endif
    }

    public void ActivateDefense()
    {
        Controller.DefenseInput = true;
    }

    public void ReleaseDefense()
    {
        Controller.DefenseInput = false;
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
}
