using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointToGo;
	private bool isOnPlatform = false;

	private void Update()
	{
		if (isOnPlatform)
		{
			transform.position = Vector2.MoveTowards(transform.position, pointToGo.position, 2f * Time.deltaTime);
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			isOnPlatform = true;
			other.transform.SetParent(transform);
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.transform.SetParent(null);
		}
	}
}
