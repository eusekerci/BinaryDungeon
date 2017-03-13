using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;

    private CombatController _playerController;
    private CombatController _enemyController;

    private CharacterData _playerData;
    private CharacterData _enemyData;

	void Start ()
	{
	}

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
        int damage = Random.Range(_enemyData.MinDamage, _enemyData.MaxDamage+1);
        _playerData.Hp -= damage;
        Debug.Log("Player got " + damage + " damage from " + direction + ". Current HP: " + _playerData.Hp);
    }

    private void OnEnemyDamaged(Direction direction)
    {
        int damage = Random.Range(_playerData.MinDamage, _playerData.MaxDamage+1);
        _enemyData.Hp -= damage;
        Debug.Log("Enemy got " + damage + " damage from " + direction + ". Current HP: " + _enemyData.Hp);
    }

    private void OnEnable()
    {
        _playerController = Player.GetComponent<CombatController>();
        _enemyController = Enemy.GetComponent<CombatController>();

        _playerData = _playerController.GetCharacterData();
        _enemyData = _enemyController.GetCharacterData();

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
