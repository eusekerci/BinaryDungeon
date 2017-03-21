using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public CharacterData Stats;

    public GameObject EnemyObject;
    private Character _enemy;

    private CombatController _playerController;
    private CombatController _enemyController;

	void Start ()
    {
        _playerController = this.gameObject.GetComponent<CombatController>();
        _enemyController = EnemyObject.GetComponent<CombatController>();

        _enemy = EnemyObject.GetComponent<Character>();

        _playerController.OnAttack += OnAttacked;
        _playerController.OnDefense += OnDefended;
        _playerController.OnDamaged += OnDamaged;
        _playerController.OnAttackStarted += OnAttackStarted;
    }

    void Update ()
    {
		
	}

    private void OnAttackStarted(Direction direction)
    {

    }

    private void OnAttacked(Direction direction)
    {
        
    }

    private void OnDefended(Direction direction)
    {

    }

    private void OnDamaged(Direction direction)
    {


    }
}
                            