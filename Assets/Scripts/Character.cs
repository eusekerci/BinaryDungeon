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
        _playerController = gameObject.GetComponent<CombatController>();
        _enemyController = EnemyObject.GetComponent<CombatController>();

        _enemy = EnemyObject.GetComponent<Character>();

        _playerController.OnAttack += OnAttacked;
        _playerController.OnBlocked += OnBlocked;
        _playerController.OnDamaged += OnDamaged;
        _playerController.OnAttackStarted += OnAttackStarted;
    }

    private void FixedUpdate()
    {
        if (_playerController.IsRecover() && Stats.CurrentStamina < Stats.MaxStamina)
        {
            StaminaIncrease(Stats.RecoverRate * Time.deltaTime);
        }
        if (_playerController.IsDefending())
        {
            StaminaDecrease(Stats.RecoverRate * Time.deltaTime);
        }

        _playerController.CanAttack = Stats.CurrentStamina > Stats.AttackCost;
    }


    public void StaminaIncrease(float amount)
    {
        Stats.CurrentStamina += amount;

        if (Stats.CurrentStamina > Stats.MaxStamina)
        {
            Stats.CurrentStamina = Stats.MaxStamina;
        }
    }

    public void StaminaDecrease(float amount)
    {
        Stats.CurrentStamina -= amount;

        if (Stats.CurrentStamina < 0)
        {
            Stats.CurrentStamina = 0;
        }
    }

    public void HPIncrease(float amount)
    {
        Stats.CurrentHp += amount;

        if (Stats.CurrentHp > Stats.MaxHp)
        {
            Stats.CurrentHp = Stats.MaxHp;
        }
    }

    public void HPDecrease(float amount)
    {
        Stats.CurrentHp -= amount;

        if (Stats.CurrentHp < 0 )
        {
            Stats.CurrentHp = 0;
        }
    }

    private void OnAttackStarted(Direction direction)
    {
        Stats.CurrentStamina -= Stats.AttackCost;
    }

    private void OnAttacked(Direction direction)
    {
        if (_enemyController.IsDefending() && _enemyController.GetDirection() == direction)
        {
            _enemyController.OnBlocked(direction);
        }
        else
        {
            _enemyController.OnDamaged(direction);
        }
    }

    private void OnBlocked(Direction direction)
    {

    }

    private void OnDamaged(Direction direction)
    {
        int damage = Random.Range(_enemy.Stats.MinDamage, _enemy.Stats.MaxDamage + 1);
        Stats.CurrentHp -= damage;
    }

    private void OnEnable()
    {
        if (_playerController != null)
        {
            _playerController.OnAttack += OnAttacked;
            _playerController.OnAttackStarted += OnAttackStarted;
            _playerController.OnBlocked += OnBlocked;
            _playerController.OnDamaged += OnDamaged;
        }
    }

    private void OnDisable()
    {
        if (_playerController != null)
        {
            _playerController.OnAttack -= OnAttacked;
            _playerController.OnAttackStarted -= OnAttackStarted;
            _playerController.OnBlocked -= OnBlocked;
            _playerController.OnDamaged -= OnDamaged;
        }
    }
}
                            