using UnityEngine;

public class Gem : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			PlayerController player = other.GetComponent<PlayerController>();
			player?.AddGem();
			Destroy(gameObject);
		}
	}
}
