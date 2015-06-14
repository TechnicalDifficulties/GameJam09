	using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	public GameObject MusicManager;

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
	public Animator anim;					// Reference to the player's animator component.
	public GameObject sprite;

	public GameObject dCharge;
	public GameObject dChargeInstance1;
	public GameObject explosion;

	public GameObject dChargeTutorial;

	public AudioClip chop1;
	public AudioClip chop2;
	public AudioClip spit;
	public AudioClip hurt;
	public AudioClip squeek;

	public bool exploded = false;

	public int timeLeft = 0;
	public float rot;

	public bool gotHit = false;
	public bool flickering = false;

	public int damage = 0;
	public int health = 100;

	public bool tutorial = false;
	public bool spikeZone = false;

	public bool dead = false;

	AudioSource audio;
	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		anim = sprite.GetComponent<Animator>();
		audio = GetComponent<AudioSource> ();

	}

	void Start()
	{

		GetComponent<Rigidbody2D> ().AddForce (Vector2.right * maxSpeed * moveForce);
		StartCoroutine(MyCoroutine2());
		rot = transform.rotation.z;

//		if (tutorial) {
//			health = 10;
//		}
	}



	IEnumerator flicker()
	{
		//Debug.Log ("flickering");
		for (int i = 0; i < 15; i++) {
			if(!dead){
				sprite.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
				yield return new WaitForSeconds (.1f);
				sprite.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
				yield return new WaitForSeconds (.1f);
			}
			else
				break;
		}
		sprite.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		flickering = false;
	}

	void Update()
	{
		if (health > 100)
			health = 100;

		if (health <= 0) {
			health = 0;

			//Destroy (gameObject);
			//spawn dead shark
			if(!dead)
				anim.Play("sharkDie");
			dead = true;
			MusicManager.GetComponent<musicManagerScript>().source1.GetComponent<audioSourceScript>().targetPitch = 0f;
			MusicManager.GetComponent<musicManagerScript>().source2.GetComponent<audioSourceScript>().targetPitch = 0f;

			GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			GetComponent<Rigidbody2D>().isKinematic = true;
		}
		if (!dead) {
			if (gotHit) {
				if (!flickering) {
					flickering = true;
					StartCoroutine (flicker ());
				}
			}
			if (Input.GetKeyDown (KeyCode.Space)) {



				if (timeLeft >= 0 || anim.GetCurrentAnimatorStateInfo (0).IsName ("sharkBiteTutorial")) {
					audio.clip = spit;
					audio.Play ();
					anim.Play ("sharkSpit");
					if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("sharkBiteTutorial")) {
						dChargeInstance1 = null;
						if (facingRight) {
							dChargeInstance1 = Instantiate (dCharge, new Vector3 (transform.position.x + 6, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
							dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f * 2);
						} else {
							dChargeInstance1 = Instantiate (dCharge, new Vector3 (transform.position.x - 6, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
							dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f * 2);

						
						}
						dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * GetComponent<Rigidbody2D> ().velocity.y * 100f * 3);
				



						switch (timeLeft) {
						case 9:
							dChargeInstance1.GetComponent<Charge> ().anim.Play ("countdown");
							dChargeInstance1.GetComponent<Charge> ().timeLeft = timeLeft;
							timeLeft = -1;
							break;
						case 8:
							dChargeInstance1.GetComponent<Charge> ().anim.Play ("count8");
							dChargeInstance1.GetComponent<Charge> ().timeLeft = timeLeft;
							timeLeft = -1;
							break;
						case 7:
							dChargeInstance1.GetComponent<Charge> ().anim.Play ("count7");
							dChargeInstance1.GetComponent<Charge> ().timeLeft = timeLeft;
							timeLeft = -1;
							break;
						case 6:
							dChargeInstance1.GetComponent<Charge> ().anim.Play ("count6");
							dChargeInstance1.GetComponent<Charge> ().timeLeft = timeLeft;
							timeLeft = -1;
							break;
						case 5:
							dChargeInstance1.GetComponent<Charge> ().anim.Play ("count5");
							dChargeInstance1.GetComponent<Charge> ().timeLeft = timeLeft;
							timeLeft = -1;
							break;
						case 4:
							dChargeInstance1.GetComponent<Charge> ().anim.Play ("count4");
							dChargeInstance1.GetComponent<Charge> ().timeLeft = timeLeft;
							timeLeft = -1;
							break;
						case 3:
							dChargeInstance1.GetComponent<Charge> ().anim.Play ("count3");
							dChargeInstance1.GetComponent<Charge> ().timeLeft = timeLeft;
							timeLeft = -1;
							break;
						case 2:
							dChargeInstance1.GetComponent<Charge> ().anim.Play ("count2");
							dChargeInstance1.GetComponent<Charge> ().timeLeft = timeLeft;
							timeLeft = -1;
							break;
						case 1:
							dChargeInstance1.GetComponent<Charge> ().anim.Play ("count1");
							dChargeInstance1.GetComponent<Charge> ().timeLeft = timeLeft;
							timeLeft = -1;
							break;
						case 0:
							dChargeInstance1.GetComponent<Charge> ().anim.Play ("count0");
							dChargeInstance1.GetComponent<Charge> ().timeLeft = timeLeft;
							timeLeft = -1;
							break;
						case -1:
							dChargeInstance1.GetComponent<Charge> ().anim.Play ("count0");
							dChargeInstance1.GetComponent<Charge> ().timeLeft = timeLeft;
							timeLeft = -1;
							break;
						//default:
						//	Debug.Log("WTF!?);
						//	break;
						}
					} else {
						dChargeInstance1 = null;
						if (facingRight) {
							dChargeInstance1 = Instantiate (dChargeTutorial, new Vector3 (transform.position.x + 6, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
							dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f * 2);
						} else {
							dChargeInstance1 = Instantiate (dChargeTutorial, new Vector3 (transform.position.x - 6, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
							dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f * 2);
						
						
						}
						dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * GetComponent<Rigidbody2D> ().velocity.y * 100f * 3);
						dChargeInstance1.GetComponent<Charge> ().spat = true;
					}

				} else
					anim.Play ("shareBite");

			}

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("explode2")) {

				OnExplode ();
				anim.Play ("sharkIdle");
				//Destroy(gameObject);

			}

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("flipFinished")) {
				anim.Play ("sharkIdle");
				Flip ();
			}

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("spitFinished")) {
				anim.Play ("sharkIdle");
			}
		}

	}

	IEnumerator MyCoroutine2()
	{
		if (!dead) {
			//Debug.Log ("Test");
			while (timeLeft >= 0) {
				//Debug.Log(timeLeft);
				yield return new WaitForSeconds (1);
				timeLeft--;
			}
		}
	}


	void FixedUpdate ()
	{
		if (!dead) {

			// Cache the horizontal input.
			float hs;
			if (facingRight)
				hs = 1;
			else
				hs = -1;

			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");

			//Debug.Log (GetComponent<Rigidbody2D>().velocity.y);

			// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
			if (hs * GetComponent<Rigidbody2D> ().velocity.x < maxSpeed 
			//&& !anim.GetCurrentAnimatorStateInfo(0).IsName("shareBite") 
				&& !anim.GetCurrentAnimatorStateInfo (0).IsName ("sharkFlip") 
				&& !anim.GetCurrentAnimatorStateInfo (0).IsName ("flipFinished"))
			// ... add a force to the player.
				GetComponent<Rigidbody2D> ().AddForce (Vector2.right * hs * moveForce);
		
			// If the player's horizontal velocity is greater than the maxSpeed...
			if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (Mathf.Sign (GetComponent<Rigidbody2D> ().velocity.x) * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);

//		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
			if (v * GetComponent<Rigidbody2D> ().velocity.y < maxSpeedy)
			// ... add a force to the player.
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * v * moveForce);
		
			// If the player's horizontal velocity is greater than the maxSpeed...
			if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.y) > maxSpeedy)
			// ... set the player's velocity to the maxSpeed in the x axis.
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, Mathf.Sign (GetComponent<Rigidbody2D> ().velocity.y) * maxSpeedy);

			// If the input is moving the player right and the player is facing left...
			if (h > 0 && !facingRight 
			//&& !anim.GetCurrentAnimatorStateInfo(0).IsName("shareBite") 
				&& !anim.GetCurrentAnimatorStateInfo (0).IsName ("sharkFlip")
				&& !anim.GetCurrentAnimatorStateInfo (0).IsName ("flipFinished")) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 ((GetComponent<Rigidbody2D> ().velocity.x) * -1, GetComponent<Rigidbody2D> ().velocity.y);
				if (anim.GetCurrentAnimatorStateInfo (0).IsName ("sharkIdle"))
					anim.Play ("sharkFlip");
				else
					Flip ();

			}

//		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && facingRight 
			//&& !anim.GetCurrentAnimatorStateInfo(0).IsName("shareBite") 
				&& !anim.GetCurrentAnimatorStateInfo (0).IsName ("sharkFlip")
				&& !anim.GetCurrentAnimatorStateInfo (0).IsName ("flipFinished")) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 ((GetComponent<Rigidbody2D> ().velocity.x) * -1, GetComponent<Rigidbody2D> ().velocity.y);
				if (anim.GetCurrentAnimatorStateInfo (0).IsName ("sharkIdle"))
					anim.Play ("sharkFlip");
				else
					Flip ();
			}
//		float zTarget =  Mathf.Rad2Deg * Mathf.Atan((GetComponent<Rigidbody2D> ().velocity.y)/(GetComponent<Rigidbody2D> ().velocity.x));
//		rot = Mathf.Round((transform.rotation.z + zTarget) / 3);
//		transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y,rot));                                    

			if (!tutorial) {
				if (transform.position.y > 4.3f) {
					transform.position = new Vector3 (transform.position.x, 4.3f, transform.position.z);
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, 0);
				}

		
				if (transform.position.y < -12.2f) {
					transform.position = new Vector3 (transform.position.x, -12.2f, transform.position.z);
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, 0);
				}

				if (transform.position.x > 34f) {
					transform.position = new Vector3 (34f, transform.position.y, transform.position.z);
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
				}

				if (transform.position.x < -25f) {
					transform.position = new Vector3 (-25f, transform.position.y, transform.position.z);
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
				}
			}
		}
	}

	void OnExplode()
	{
		if (!dead) {
			// Create a quaternion with a random rotation in the z-axis.
			Quaternion randomRotation = Quaternion.Euler (0f, 0f, Random.Range (0f, 360f));

			// Instantiate the explosion where the rocket is with the random rotation.
			Instantiate (explosion, transform.position, randomRotation);
		}
	}
	void OnTriggerEnter2D (Collider2D col) {
		if (!dead) {
			if (col.tag == "Explosion") {
				if (!gotHit) {

					StartCoroutine (handleDamage (20));
				}
			}

			if (col.tag == "Harpoon") {
				if (!gotHit) {
					
					StartCoroutine (handleDamage (50));
				}
			}

			if (col.tag == "Spikes") {
				spikeZone = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (!dead) {
			if (col.tag == "Spikes") {
				spikeZone = false;
			}
		}
	}

	IEnumerator handleDamage(int dmg){
		if (!dead) {
			gotHit = true;
			damage++;
			if (!spikeZone)
				health = health - dmg;
			audio.clip = hurt;
			audio.Play ();
			if (!spikeZone) {
				GetComponent<CircleCollider2D> ().enabled = false;
			}
			yield return new WaitForSeconds (3);
			if (!spikeZone) {
				GetComponent<CircleCollider2D> ().enabled = true;
			}

			gotHit = false;
		}

	}

	public void ateDuck()
	{
		if (!dead) {
			if (!tutorial)
				health++;
			else
				health += 100;
			audio.clip = squeek;
			audio.Play ();
		}
	}

	public void ateFish()
	{
		if (!dead) {
			if (!tutorial)
				health++;
			else
				health += 20;
			audio.clip = chop2;
			audio.Play ();
		}
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (!dead) {
			if (col.gameObject.tag == "Surface") {
				if (spikeZone) {
					//if(!gotHit)
					StartCoroutine (handleDamage (0));
				}
			}

			if (col.gameObject.tag == "Charge") {
				if (anim.GetCurrentAnimatorStateInfo (0).IsName ("shareBite")) {
					audio.clip = chop2;
					audio.Play ();


					//ebug.Log ("CHOMP at " + col.gameObject.GetComponent<Charge>().timeLeft + " seconds");
					if (!col.gameObject.GetComponent<Charge> ().tutorial) {
						timeLeft = col.gameObject.GetComponent<Charge> ().timeLeft;
						StartCoroutine (MyCoroutine2 ());

						switch (col.gameObject.GetComponent<Charge> ().timeLeft) {
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
					} else {
						anim.Play ("sharkBiteTutorial");
					}
					Destroy (col.gameObject);
				}
				//anim.SetTrigger("BiteCharge");
			}
		}
	}
	
	
	void Flip ()
	{
		if (!dead) {
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


}
