using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	[SerializeField] private Transform[] points;
	private int index;

	void Update()
	{
		if(Vector2.Distance(transform.position, points[index].position) < .1f)
		{
			index++;
			if(index >= points.Length)
			{
				index = 0;
			}
		}
		transform.position = Vector2.MoveTowards(transform.position, points[index].position, 2f * Time.deltaTime);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
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
