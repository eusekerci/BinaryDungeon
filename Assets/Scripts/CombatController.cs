using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    public enum CombatStates
    {
        Idle,
        Defending,
        AttackSignal,
        Attacked,
        Damaged,
        Blocked,
        SkillChanneling,
        SkillUsed,
        Stun
    }

    private CharacterData PlayerData;

    public Action<Direction> onAttack;
    public Action<Direction> onDefense;

    public Direction normalizedAttack;

    void Start ()
    {
        PlayerData = new CharacterData();
	}
	
	void Update ()
    {
		
	}
}
