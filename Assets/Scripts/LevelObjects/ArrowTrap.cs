using System.Collections;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField]private Transform firepoint;
	[SerializeField] private GameObject arrow;
    public bool canAttack;

	private void Update()
	{
		if (canAttack)
		{
			Attack();
		}
	}

	private void Attack()
    {
		Instantiate(arrow, firepoint.position, arrow.transform.rotation);
		StartCoroutine(ResetCanAttack());
    }

	private IEnumerator ResetCanAttack()
	{
		float random = Random.Range(1, 6);
		canAttack = false;
		yield return new WaitForSeconds(random);
		canAttack = true;
	}
}
