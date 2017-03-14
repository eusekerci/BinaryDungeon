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
        Defensive
    }

    [SerializeField] private CharacterData _playerData;
    [SerializeField] private CombatStates _currentState;
    [SerializeField] private CombatMode _currentMode;
    [SerializeField] private Direction _currentCombatDirection;

    public Action<Direction> OnAttack;
    public Action<Direction> OnDefense;
    public Action<Direction> OnDamaged;

    public Direction NormalizedAttack;

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

    public Direction GetDirection()
    {
        return _currentCombatDirection;
    }

    public CharacterData GetCharacterData()
    {
        return _playerData;
    }

    private void ChangeState(CombatStates newState)
    {
        _currentState = newState;
    }

    public void ChangeMode(CombatMode newMode)
    {
        _currentMode = newMode;
        _currentState = CombatStates.Idle;
        _currentCombatDirection = Direction.None;
    }

    public void SwitchMode()
    {
        _currentMode = IsOffensive() ? CombatMode.Defensive : CombatMode.Offensive;
        _currentState = CombatStates.Idle;
        _currentCombatDirection = Direction.None;
    }

    private void UpdateFrames()
    {
        _currentAttackedFrames--;
        _currentAttackingFrames--;
    }

    private void Start()
    {
        _attackingFrames = GameManager.GetFrameCount(_playerData.AttackSpeed);
        _attackedFrames = GameManager.GetFrameCount(_playerData.AttackAgainSpeed);
        _currentMode = CombatMode.Offensive;
    }

    private void FixedUpdate()
    {
        UpdateFrames();
        UpdateState();
        //Debug.Log(_currentState + "  " + _currentCombatDirection + "  " + _currentMode);
    }

    private void UpdateState()
    {
        if (IsOffensive())
        {
            if (IsAttacking() && !IsAttacked() && _currentAttackingFrames <= 0)
            {
                ChangeState(CombatStates.Attacked);
                _currentAttackedFrames = _attackedFrames;
                if (OnAttack != null)
                {
                    OnAttack(_currentCombatDirection);
                }
                return;
            }
            if (IsAttacked() && _currentAttackedFrames <= 0)
            {
                ChangeState(CombatStates.Idle);
                _currentCombatDirection = NormalizedAttack;
                return;
            }
            if (NormalizedAttack == Direction.None || !IsWaiting())
            {
                return;
            }
            DoAttack(NormalizedAttack);
        }
        else if (IsDefensive())
        {
            if (NormalizedAttack == Direction.None)
            {
                return;
            }
            DoDefend(NormalizedAttack);
        }
    }

    public void DoAttack(Direction direction)
    {
        ChangeState(CombatStates.Attacking);
        _currentAttackingFrames = _attackingFrames;
        _currentCombatDirection = direction;
    }

    public void DoDefend(Direction direction)
    {
        ChangeState(CombatStates.Defending);
        _currentCombatDirection = direction;
    }
}
