using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;


	[SerializeField] private float speed = 5;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		rb.velocity = Vector2.left * speed;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Destroy(gameObject);

		if(other.tag == "Player")
		{
			other.GetComponent<PlayerController>().TakeDamage();
		}
	}
}
