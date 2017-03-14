using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    //Temporary
    public bool p1;

    private CombatController _controller;
    private Character _character;
    private Transform _weapon;
    private Transform _shield;
    private Transform _equipment;

    private bool isAttacking = false;

	void Start ()
	{
        _controller = GetComponent<CombatController>();
        _character = GetComponent<Character>();
	    _equipment = transform.GetChild(0);
	    _weapon = _equipment.FindChild("Weapon");
	    _shield = _equipment.FindChild("Shield");
	}
	
	void Update ()
	{
	    if (!_controller.IsAttacking())
	    {
	        if (_controller.GetDirection() == Direction.Left)
	        {
                _equipment.localPosition = new Vector3(-1, 0, 0);
            }
            else if (_controller.GetDirection() == Direction.Right)
	        {
	            _equipment.localPosition = new Vector3(1, 0, 0);

	        }
	        else
	        {
                _equipment.localPosition = new Vector3(0, p1 ? 1 : -1, 0);
            }
            isAttacking = false;
	    }
        else if (!isAttacking)
	    {
	        isAttacking = true;
            if (_controller.GetDirection() == Direction.Left)
            {
                _equipment.localPosition = new Vector3(-1, 0, 0);
            }
            else if (_controller.GetDirection() == Direction.Right)
            {
                _equipment.localPosition = new Vector3(1, 0, 0);

            }
            else
            {
                _equipment.localPosition = new Vector3(0, p1 ? 1 : -1, 0);
            }
            StartCoroutine(AttackingAnimationCoroutine(_character.Stats.AttackSpeed));
	    }
	    _weapon.gameObject.SetActive(_controller.IsOffensive());
        _shield.gameObject.SetActive(_controller.IsDefensive());
    }

    IEnumerator AttackingAnimationCoroutine(float time)
    {
        float moveInterval = 3.5f / GameManager.GetFrameCount(time);
        int p2Modif = p1 ? 1 : -1;
        if (!p1)
            moveInterval *= -1;
        int modif = _controller.GetDirection() == Direction.Left ? -1*p2Modif : 1*p2Modif;
        for (float i = 0; i < time; i += Time.fixedDeltaTime)
        {
            if (i > time / 2.0f)
            {
                modif = _controller.GetDirection() == Direction.Left ? 1*p2Modif : -1*p2Modif;
            }
            _equipment.position = new Vector3(_equipment.position.x + modif*moveInterval, _equipment.position.y + moveInterval, _equipment.position.z);
            yield return new WaitForFixedUpdate();
        }
    }
}
