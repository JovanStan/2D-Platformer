using UnityEngine;

public class DoorOpener : MonoBehaviour
{
	[SerializeField] private GameObject doorToOpen;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Box")
		{
			Destroy(doorToOpen);
		}
	}
}
