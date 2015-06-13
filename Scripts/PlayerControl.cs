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

	public GameObject dCharge;
	public GameObject dChargeInstance1;
	public GameObject explosion;

	public AudioClip chop1;
	public AudioClip chop2;

	public bool exploded = false;

	public int timeLeft = 0;

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		anim = sprite.GetComponent<Animator>();

	}

	void Start()
	{

		GetComponent<Rigidbody2D> ().AddForce (Vector2.right * maxSpeed * moveForce);
		StartCoroutine(MyCoroutine2());

	}

	public void test()
	{

	}

	void Update()
	{

		if (Input.GetKeyDown (KeyCode.Space)) {


			if(timeLeft >= 0){
				dChargeInstance1 = null;
				if(facingRight){
					dChargeInstance1 = Instantiate (dCharge, new Vector3(transform.position.x + 6, transform.position.y ,transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
					dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f * 2);
				}else{
					dChargeInstance1 = Instantiate (dCharge, new Vector3(transform.position.x - 6, transform.position.y ,transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
					dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f * 2);
					
				}
				dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1.5f * 1000f * 2);
				anim.Play ("sharkIdle");

				switch (timeLeft) {
				case 9:
					dChargeInstance1.GetComponent<Charge>().anim.Play("countdown");
					dChargeInstance1.GetComponent<Charge>().timeLeft = timeLeft;
					timeLeft = -1;
					break;
				case 8:
					dChargeInstance1.GetComponent<Charge>().anim.Play ("count8");
					dChargeInstance1.GetComponent<Charge>().timeLeft = timeLeft;
					timeLeft = -1;
					break;
				case 7:
					dChargeInstance1.GetComponent<Charge>().anim.Play ("count7");
					dChargeInstance1.GetComponent<Charge>().timeLeft = timeLeft;
					timeLeft = -1;
					break;
				case 6:
					dChargeInstance1.GetComponent<Charge>().anim.Play ("count6");
					dChargeInstance1.GetComponent<Charge>().timeLeft = timeLeft;
					timeLeft = -1;
					break;
				case 5:
					dChargeInstance1.GetComponent<Charge>().anim.Play ("count5");
					dChargeInstance1.GetComponent<Charge>().timeLeft = timeLeft;
					timeLeft = -1;
					break;
				case 4:
					dChargeInstance1.GetComponent<Charge>().anim.Play ("count4");
					dChargeInstance1.GetComponent<Charge>().timeLeft = timeLeft;
					timeLeft = -1;
					break;
				case 3:
					dChargeInstance1.GetComponent<Charge>().anim.Play ("count3");
					dChargeInstance1.GetComponent<Charge>().timeLeft = timeLeft;
					timeLeft = -1;
					break;
				case 2:
					dChargeInstance1.GetComponent<Charge>().anim.Play ("count2");
					dChargeInstance1.GetComponent<Charge>().timeLeft = timeLeft;
					timeLeft = -1;
					break;
				case 1:
					dChargeInstance1.GetComponent<Charge>().anim.Play ("count1");
					dChargeInstance1.GetComponent<Charge>().timeLeft = timeLeft;
					timeLeft = -1;
					break;
				case 0:
					dChargeInstance1.GetComponent<Charge>().anim.Play ("count0");
					dChargeInstance1.GetComponent<Charge>().timeLeft = timeLeft;
					timeLeft = -1;
					break;
				//default:
				//	Debug.Log("WTF!?);
				//	break;
				}

			}
			else
				anim.SetTrigger("Bite");

		}

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("explode2")) {

			OnExplode();
			anim.Play ("sharkIdle");
				//Destroy(gameObject);

		}

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("flipFinished")) {
			anim.Play ("sharkIdle");
			Flip();
		}

	}

	IEnumerator MyCoroutine2()
	{
		//Debug.Log ("Test");
		while (timeLeft >= 0) {
			//Debug.Log(timeLeft);
			yield return new WaitForSeconds (1);
			timeLeft--;
		}
	}


	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis ("Vertical");

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed 
		   //&& !anim.GetCurrentAnimatorStateInfo(0).IsName("shareBite") 
		   && !anim.GetCurrentAnimatorStateInfo(0).IsName("sharkFlip") 
		   && !anim.GetCurrentAnimatorStateInfo(0).IsName("flipFinished"))
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
		if (h > 0 && !facingRight 
		    //&& !anim.GetCurrentAnimatorStateInfo(0).IsName("shareBite") 
		    && !anim.GetCurrentAnimatorStateInfo(0).IsName("sharkFlip")
		    && !anim.GetCurrentAnimatorStateInfo(0).IsName("flipFinished")) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 ((GetComponent<Rigidbody2D> ().velocity.x) * -1, GetComponent<Rigidbody2D> ().velocity.y);
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("sharkIdle"))
				anim.Play ("sharkFlip");
			else
				Flip();

		}

//		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && facingRight 
		         //&& !anim.GetCurrentAnimatorStateInfo(0).IsName("shareBite") 
		         && !anim.GetCurrentAnimatorStateInfo(0).IsName("sharkFlip")
		         && !anim.GetCurrentAnimatorStateInfo(0).IsName("flipFinished")) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 ((GetComponent<Rigidbody2D> ().velocity.x) * -1, GetComponent<Rigidbody2D> ().velocity.y);
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("sharkIdle"))
				anim.Play ("sharkFlip");
			else
				Flip();
		}



	}

	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		
		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("shareBite"))
		{
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();


			//ebug.Log ("CHOMP at " + col.gameObject.GetComponent<Charge>().timeLeft + " seconds");

			timeLeft = col.gameObject.GetComponent<Charge>().timeLeft - 1;
			StartCoroutine(MyCoroutine2());

			switch(col.gameObject.GetComponent<Charge>().timeLeft){
			case 9:
					anim.Play ("sharkBiteCharge");
					break;
			case 8:
				anim.Play ("sharkBiteCharge8");
					break;
			case 7:
				anim.Play ("sharkBiteCharge7");
					break;
			case 6:
				anim.Play ("sharkBiteCharge6");
					break;
			case 5:
				anim.Play ("sharkBiteCharge5");
					break;
			case 4:
				anim.Play ("sharkBiteCharge4");	
					break;
			case 3:
				anim.Play ("sharkBiteCharge3");
					break;
			case 2:
				anim.Play ("sharkBiteCharge2");
				break;
			case 1:
				anim.Play ("sharkBiteCharge1");
				break;
			case 0:
				anim.Play ("sharkBiteCharge0");
				break;
			}
			Destroy(col.gameObject);
		}
			//anim.SetTrigger("BiteCharge");

	}
	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		//GetComponent<Rigidbody2D> ().velocity = new Vector2 ((GetComponent<Rigidbody2D> ().velocity.x) * -1, GetComponent<Rigidbody2D> ().velocity.y);
		//anim.Play ("sharkIdle");
	}


}
