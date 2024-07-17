using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	public float bounceForce = 10f;

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
			if (rb != null)
			{
				Vector2 direction = (other.transform.position - transform.position).normalized;
				rb.AddForce(direction * bounceForce, ForceMode2D.Impulse);
			}
			PlayerController player = other.gameObject.GetComponent<PlayerController>();
			player.TakeDamage();
		}
	}
}
