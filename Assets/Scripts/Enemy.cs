using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private Transform pointA;
	[SerializeField] private Transform pointB;

	private Animator anim;
	private SpriteRenderer spriteRenderer;

	private Vector3 currentTarget;
	private bool isMoving = true;
	private bool canDealDamage = true;

	private void Awake()
	{
		currentTarget = pointB.position;
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		if (!isMoving) return;
		Movement();
		FlipEnemy();
	}

	private void Movement()
	{
		if (transform.position == pointA.position)
		{
			currentTarget = pointB.position;
			StartCoroutine(WaitBeforeMove());
		}
		else if (transform.position == pointB.position)
		{
			currentTarget = pointA.position;
			StartCoroutine(WaitBeforeMove());
		}

		transform.position = Vector3.MoveTowards(transform.position, currentTarget, 2 * Time.deltaTime);
	}

	IEnumerator WaitBeforeMove()
	{
		isMoving = false;
		anim.SetBool("isMoving", false);
		yield return new WaitForSeconds(2f);
		isMoving = true;
		anim.SetBool("isMoving", true);
	}

	public void FlipEnemy()
	{
		if (currentTarget == pointA.position)
		{
			spriteRenderer.flipX = false;
		}
		else if (currentTarget == pointB.position)
		{
			spriteRenderer.flipX = true;
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			PlayerController player = other.gameObject.GetComponent<PlayerController>();
			if (player != null && canDealDamage)
			{
				player.TakeDamage();
				StartCoroutine(ResetDealDamage());
			}
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		canDealDamage = true;
	}

	private IEnumerator ResetDealDamage()
	{
		canDealDamage = false;
		yield return new WaitForSeconds(3f);
		canDealDamage = true;
	}
}
