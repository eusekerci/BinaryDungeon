using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : MonoBehaviour
{
    public CombatController EnemyController;
    public Character Enemy;

	void Start ()
    {
        StartCoroutine(AITick());
    }

    void Update ()
    {

	}

    IEnumerator AITick()
    {
        if (Enemy.Stats.CurrentStamina < Enemy.Stats.MaxStamina / 4)
        {
            EnemyController.DoRecover();
            yield return new WaitForSeconds(1);
        }
        else
        {
            int rand = Random.Range(0, 10);
            if (rand < 3)
            {
                EnemyController.DefenseInput = false;
                EnemyController.DoAttack(Direction.Left);
            }
            else if (rand < 6)
            {
                EnemyController.DefenseInput = false;
                EnemyController.DoAttack(Direction.Right);
            }
            else if (rand < 8)
            {
                EnemyController.DefenseInput = true;
                EnemyController.DoDefend(Direction.Left);
                yield return new WaitForSeconds(0.25f);
            }
            else if (rand < 10)
            {
                EnemyController.DefenseInput = true;
                EnemyController.DoDefend(Direction.Right);
            }
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(AITick());
    }
}
