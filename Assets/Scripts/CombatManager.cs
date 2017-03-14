using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public GameObject PlayerObject;
    public GameObject EnemyObject;

    private Character _player;
    private Character _enemy;

    private CombatController _playerController;
    private CombatController _enemyController;

    private void OnPlayerAttacked(Direction direction)
    {
        Debug.Log("Player Attacked from " + direction);
        if (_enemyController.IsDefending() && _enemyController.GetDirection() == direction)
        {
            _enemyController.OnDefense(direction);
        }
        else
        {
            _enemyController.OnDamaged(direction);
        }
    }

    private void OnEnemyAttacked(Direction direction)
    {
        Debug.Log("Enemy Attacked from " + direction);
        if (_playerController.IsDefending() && _playerController.GetDirection() == direction)
        {
            _playerController.OnDefense(direction);
        }
        else
        {
            _playerController.OnDamaged(direction);
        }
    }

    private void OnPlayerDefended(Direction direction)
    {
        Debug.Log("Player Defended from " + direction);

    }

    private void OnEnemyDefended(Direction direction)
    {
        Debug.Log("Enemy Defended from " + direction);

    }

    private void OnPlayerDamaged(Direction direction)
    {
        int damage = Random.Range(_enemy.Stats.MinDamage, _enemy.Stats.MaxDamage+1);
        _player.Stats.CurrentHp -= damage;
        Debug.Log("Player got " + damage + " damage from " + direction + ". Current HP: " + _player.Stats.CurrentHp);
    }

    private void OnEnemyDamaged(Direction direction)
    {
        int damage = Random.Range(_player.Stats.MinDamage, _player.Stats.MaxDamage+1);
        _enemy.Stats.CurrentHp -= damage;
        Debug.Log("Enemy got " + damage + " damage from " + direction + ". Current HP: " + _enemy.Stats.CurrentHp);
    }

    private void OnEnable()
    {
        _playerController = PlayerObject.GetComponent<CombatController>();
        _enemyController = EnemyObject.GetComponent<CombatController>();

        _player = PlayerObject.GetComponent<Character>();
        _enemy = EnemyObject.GetComponent<Character>();

        _playerController.OnAttack += OnPlayerAttacked;
        _enemyController.OnAttack += OnEnemyAttacked;

        _playerController.OnDefense += OnPlayerDefended;
        _enemyController.OnDefense += OnEnemyDefended;

        _playerController.OnDamaged += OnPlayerDamaged;
        _enemyController.OnDamaged += OnEnemyDamaged;
    }

    private void OnDisable()
    {
        _playerController.OnAttack -= OnPlayerAttacked;
        _enemyController.OnAttack -= OnEnemyAttacked;

        _playerController.OnDefense -= OnPlayerDefended;
        _enemyController.OnDefense -= OnEnemyDefended;

        _playerController.OnDamaged -= OnPlayerDamaged;
        _enemyController.OnDamaged -= OnEnemyDamaged;
    }
}
