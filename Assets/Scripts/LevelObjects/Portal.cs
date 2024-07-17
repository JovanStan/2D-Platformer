using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
	public float time;
	private bool isInPortal;

	private void Update()
	{
		if (isInPortal)
		{
			time += Time.deltaTime;

			if(time > 3)
			{
				SceneManager.LoadScene("Level2");
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			isInPortal = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		isInPortal = false;
		time = 0;
	}

}
