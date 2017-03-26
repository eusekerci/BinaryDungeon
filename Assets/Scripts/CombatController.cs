using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] private int _attackingFrames;
    [SerializeField] private int _currentAttackingFrames;
    [SerializeField] private int _attackedFrames;
    [SerializeField] private int _currentAttackedFrames;

    public enum CombatStates
    {
        Idle,
        Attacking,
        Attacked,
        Defending,
        Blocked,
        Damaged,
        SkillChanneling,
        SkillUsed,
        Stun
    }

    public enum CombatMode
    {
        Offensive,
        Defensive,
        Recover
    }

    [SerializeField] private CombatStates _currentState;
    [SerializeField] private CombatMode _currentMode;
    [SerializeField] private Direction _currentAttackDirection;
    [SerializeField] private Direction _currentDefenseDİrection;

    public bool DefenseInput = false;
    public bool CanAttack = true;

    public Action<Direction> OnAttackStarted;
    public Action<Direction> OnAttack;
    public Action<Direction> OnBlocked;
    public Action<Direction> OnDamaged;

    public const float NearZero = 0.01f;

    public bool IsWaiting()
    {
        return _currentState == CombatStates.Idle;
    }

    public bool IsDefending()
    {
        return _currentState == CombatStates.Defending;
    }

    public bool IsBlocked()
    {
        return _currentState == CombatStates.Blocked;
    }

    public bool IsDamaged()
    {
        return _currentState == CombatStates.Damaged;
    }

    public bool IsAttacking()
    {
        return _currentState == CombatStates.Attacking;
    }

    public bool IsAttacked()
    {
        return _currentState == CombatStates.Attacked;
    }

    public bool IsOffensive()
    {
        return _currentMode == CombatMode.Offensive;
    }

    public bool IsDefensive()
    {
        return _currentMode == CombatMode.Defensive;
    }

    public bool IsRecover()
    {
        return _currentMode == CombatMode.Recover;
    }

    public Direction GetDirection()
    {
        if (IsOffensive())
            return _currentAttackDirection;
        else if (IsDefensive())
            return _currentDefenseDİrection;

        return Direction.None;
    }

    private void ChangeState(CombatStates newState)
    {
        _currentState = newState;
    }

    public void ChangeMode(CombatMode newMode)
    {
        _currentMode = newMode;
    }

    public void SwitchMode()
    {
        _currentMode = IsOffensive() ? CombatMode.Defensive : CombatMode.Offensive;
        _currentState = CombatStates.Idle;
        _currentAttackDirection = Direction.None;
    }

    private void UpdateFrames()
    {
        _currentAttackedFrames--;
        _currentAttackingFrames--;
    }

    private void Start()
    {
        _attackingFrames = GameManager.GetFrameCount(GetComponent<Character>().Stats.AttackSpeed);
        _attackedFrames = GameManager.GetFrameCount(GetComponent<Character>().Stats.AttackAgainSpeed);
        _currentMode = CombatMode.Recover;
    }

    private void FixedUpdate()
    {
        UpdateFrames();
        UpdateState();
    }

    private void UpdateState()
    {
        if (IsOffensive())
        {
            if (IsAttacking() && _currentAttackingFrames <= 0)
            {
                ChangeState(CombatStates.Attacked);
                _currentAttackedFrames = _attackedFrames;
                if (OnAttack != null)
                {
                    OnAttack(_currentAttackDirection);
                }
                return;
            }
            else if (IsAttacked() && _currentAttackedFrames <= 0)
            {
                ChangeMode(CombatMode.Recover);
                _currentAttackDirection = Direction.None;
                return;
            }
        }
        else if (IsDefensive())
        {
            if (!DefenseInput)
            {
                DoRecover();
                return;
            }
        }
    }

    public void DoAttack(Direction direction)
    {
        if (!IsOffensive())
        {
            if (!CanAttack)
            {
                return;
            }
            ChangeMode(CombatMode.Offensive);
            ChangeState(CombatStates.Attacking);
            _currentAttackingFrames = _attackingFrames;
            _currentAttackDirection = direction;
            if (OnAttackStarted != null)
            {
                OnAttackStarted(direction);
            }
        }
    }

    public void DoAttackLeft()
    {
        DoAttack(Direction.Left);
    }

    public void DoAttackRight()
    {
        DoAttack(Direction.Right);
    }

    public void DoDefend(Direction direction)
    {
        ChangeMode(CombatMode.Defensive);
        ChangeState(CombatStates.Defending);
        _currentDefenseDİrection = direction;
    }

    public void DoDefendLeft()
    {
        DoDefend(Direction.Left);
    }

    public void DoDefendRight()
    {
        DoDefend(Direction.Right);
    }

    public void DoRecover()
    {
        ChangeMode(CombatMode.Recover);
        ChangeState(CombatStates.Idle);
    }
}
