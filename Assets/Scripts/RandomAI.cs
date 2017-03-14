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
        if (rand < 3)
            EnemyController.SwitchMode();
        if (rand < 4)
            EnemyController.NormalizedAttack = Direction.Left;
        else if (rand < 8)
            EnemyController.NormalizedAttack = Direction.Right;
        else
            EnemyController.NormalizedAttack = Direction.None;
        yield return new WaitForSeconds(1);
        StartCoroutine(AITick());
    }
}
