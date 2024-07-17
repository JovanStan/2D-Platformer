using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
	public static PlayerController instance;

    private Rigidbody2D rb;
	private Animator anim;

	private Vector2 moveDir;
	[SerializeField] private float speed = 5f;
	[SerializeField] private float climbSpeed = 3f;

	[SerializeField] private float jumpForce = 10f;

	private BoxCollider2D boxCollider;
	private float gravityScale;

	[SerializeField]private int gems;
	private const string GEMS_COLLECTED = "GEMS";

	public int currentHealth = 3;
	public bool canHitPlayer = true;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
		boxCollider = GetComponent<BoxCollider2D>();
		gems = PlayerPrefs.GetInt(GEMS_COLLECTED);
		gravityScale = rb.gravityScale;
	}

	private void Start()
	{
		UIManager.instance.gemsText.text = "x " + gems;
	}

	void Update()
	{
		Movement();
		Jump();
		Flip();
		ClimbLadder();
	}

	private void ClimbLadder()
	{
		if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
		{
			//float ver = CrossPlatformInputManager.GetAxisRaw("Vertical");
			float ver = Input.GetAxisRaw("Vertical");
			moveDir = new Vector2(rb.velocity.x, ver * climbSpeed);
			rb.velocity = moveDir;
			rb.gravityScale = 0;
			anim.SetBool("isClimbing", true);
		}
		else
		{
			rb.gravityScale = gravityScale;
			anim.SetBool("isClimbing", false);
			return;
		}
	}

	private void Movement()
	{
		//float hor = CrossPlatformInputManager.GetAxisRaw("Horizontal") * speed;
		float hor = Input.GetAxisRaw("Horizontal") * speed;
		moveDir = new Vector2(hor, rb.velocity.y);
		rb.velocity = moveDir;
		if(rb.velocity.x != 0)
		{
			anim.SetBool("isMoving", true);
		}
		else
		{
			anim.SetBool("isMoving", false);
		}
	}

	private void Jump()
	{
		if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			if ((CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)))
			{
				rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			}
			anim.SetBool("isGrounded", true);
		}
		else
		{
			anim.SetBool("isGrounded", false);
			return;
		}	
	}

	private void Flip()
	{
		Vector2 temp = transform.localScale;
		if(rb.velocity.x > 1f)
		{
			temp.x = 1f;
		}else if(rb.velocity.x < -1f)
		{
			temp.x = -1f;
		}
		transform.localScale = temp;
	}


	public void AddGem()
	{
		gems++;
		PlayerPrefs.SetInt(GEMS_COLLECTED, gems);
		UIManager.instance.gemsText.text = "x " + gems;
	}

	public void TakeDamage()
	{
		if (!canHitPlayer) return;

		currentHealth--;
		StartCoroutine(ResetCanHitPlayer());
		anim.SetTrigger("hurt");
		UIManager.instance.UpdateHealth();

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			gameObject.SetActive(false);
		}
	}

	private IEnumerator ResetCanHitPlayer()
	{
		canHitPlayer = false;
		yield return new WaitForSeconds(2f);
		canHitPlayer = true;
	}

	public void HealPlayer()
	{
		currentHealth++;
		UIManager.instance.UpdateHealth();
		if (currentHealth > 3)
		{
			currentHealth = 3;
		}
	}
}
