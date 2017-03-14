using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : MonoBehaviour
{

    public CombatController EnemyController;

	void Start ()
    {
        StartCoroutine(AITick());
    }

    void Update ()
    {

	}

    IEnumerator AITick()
    {
        int rand = Random.Range(0, 10);
        if (rand < 2)
        {
            EnemyController.DefenseInput = false;
            EnemyController.DoAttack(Direction.Left);
        }
        else if (rand < 4)
        {
            EnemyController.DefenseInput = false;
            EnemyController.DoAttack(Direction.Right);
        }
        else if (rand < 7)
        {
            EnemyController.DefenseInput = true;
            EnemyController.DoDefend(Direction.Left);
            yield return new WaitForSeconds(1);
        }
        else if (rand < 10)
        {
            EnemyController.DefenseInput = true;
            EnemyController.DoDefend(Direction.Right);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(AITick());
    }
}
