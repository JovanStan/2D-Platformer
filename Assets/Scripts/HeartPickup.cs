using UnityEngine;

public class HeartPickup : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			PlayerController player = other.GetComponent<PlayerController>();
			player?.HealPlayer();
			Destroy(gameObject);
		}
	}
}
