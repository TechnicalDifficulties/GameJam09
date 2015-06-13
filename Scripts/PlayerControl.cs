using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.


	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 40f;				// The fastest the player can travel in the x axis.
	public float maxSpeedy = 10.5f;
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.


	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.
	public GameObject sprite;
	

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		anim = sprite.GetComponent<Animator>();

	}

	void Start()
	{
		GetComponent<Rigidbody2D> ().AddForce (Vector2.right * maxSpeed * moveForce);
	}


	void Update()
	{

		if (Input.GetKeyDown (KeyCode.Space)) {
			anim.SetTrigger("Bite");
		}

	}


	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis ("Vertical");

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			// ... add a force to the player.
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed , GetComponent<Rigidbody2D>().velocity.y );

//		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(v * GetComponent<Rigidbody2D>().velocity.y < maxSpeedy)
			// ... add a force to the player.
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * v * moveForce);
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > maxSpeedy)
			// ... set the player's velocity to the maxSpeed in the x axis.
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x , Mathf.Sign(GetComponent<Rigidbody2D>().velocity.y) * maxSpeedy);

		// If the input is moving the player right and the player is facing left...
		if (h > 0 && !facingRight) {
			Flip ();
			GetComponent<Rigidbody2D> ().velocity = new Vector2 ((GetComponent<Rigidbody2D> ().velocity.x) * -1, GetComponent<Rigidbody2D> ().velocity.y);
		}

//		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && facingRight) {
			Flip ();
			GetComponent<Rigidbody2D> ().velocity = new Vector2 ((GetComponent<Rigidbody2D> ().velocity.x) * -1, GetComponent<Rigidbody2D> ().velocity.y);
		}

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("shareBite"))
		{
			Debug.Log ("CHOMP!");
			Destroy(col.gameObject);
			anim.SetTrigger("BiteCharge");
		}
	}
	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


}
