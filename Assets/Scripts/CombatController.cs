using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

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

    [Serializable]
    public class AttackParams
    {
        public float AttackSpeed;
        public float AttackAgainSpeed;
        public int AttackingFrames;
        public int CurrentAttackingFrames;
        public int AttackedFrames;
        public int CurrentAttackedFrames;
    }

    public AttackParams AttackVars = new AttackParams();

    [SerializeField]
    private CharacterData _playerData;
    [SerializeField]
    private CombatStates _currentState;
    [SerializeField]
    private CombatMode _currentMode;
    [SerializeField]
    private Direction _currentCombatDirection;

    public Action<Direction> OnAttack;
    public Action<Direction> OnDefense;

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

    private void ChangeState(CombatStates newState)
    {
        _currentState = newState;
    }

    private void ChangeMode(CombatMode newMode)
    {
        _currentMode = newMode;
    }

    public void SwitchMode()
    {
        _currentMode = IsOffensive() ? CombatMode.Defensive : CombatMode.Offensive;
    }

    private void UpdateFrames()
    {
        AttackVars.CurrentAttackedFrames--;
        AttackVars.CurrentAttackingFrames--;
    }

    private void Start ()
    {
        _playerData = new CharacterData();
        AttackVars.AttackingFrames = GetFrameCount(AttackVars.AttackSpeed);
        AttackVars.AttackedFrames = GetFrameCount(AttackVars.AttackAgainSpeed);
        _currentMode = CombatMode.Offensive;
    }
	
	private void FixedUpdate ()
	{
	    UpdateFrames();
        UpdateState();
    }

    private void UpdateState()
    {
        if (IsOffensive())
        {
            if (IsAttacking() && AttackVars.CurrentAttackingFrames <= 0)
            {
                ChangeState(CombatStates.Attacked);
                AttackVars.CurrentAttackedFrames = AttackVars.AttackedFrames;
                return;
            }
            if (IsAttacked() && AttackVars.CurrentAttackedFrames <= 0)
            {
                ChangeState(CombatStates.Idle);
                _currentCombatDirection = NormalizedAttack;
                return;
            }
            if (NormalizedAttack == Direction.None || !IsWaiting())
            {
                return;
            }
                ChangeState(CombatStates.Attacking);
                AttackVars.CurrentAttackingFrames = AttackVars.AttackingFrames;
                _currentCombatDirection = NormalizedAttack;
        }
        else if (IsDefensive())
        {
            if (NormalizedAttack == Direction.None)
            {
                return;
            }
            ChangeState(CombatStates.Defending);
            _currentCombatDirection = NormalizedAttack;
        }
    }

    public static int GetFrameCount(float inp)
    {
        return Mathf.RoundToInt(inp/Time.fixedDeltaTime);
    }
}
