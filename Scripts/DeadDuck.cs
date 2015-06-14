using UnityEngine;
using UnityEngine;
using System.Collections;

public class DeadDuck : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.
	public GameObject splash;
	//public GameObject countSprite;
	public Animator anim;

	public AudioClip splash2;
	public AudioClip squeek;

	public AudioSource audio;

	public int timeLeft;
	public bool exploded = false;
	
	public bool tutorial = false;
	public bool spat = false;
	public bool sunked = false;

	void Awake()
	{
		audio = GetComponent<AudioSource> ();
		if (!tutorial) {
			//anim = countSprite.GetComponent<Animator> ();
		}
		
	}
	
	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		
		if (!tutorial) {
			//Destroy(gameObject, timeLeft + 1);
			StartCoroutine (MyCoroutine ());
			StartCoroutine (MyCoroutine2 ());
			//GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f);
		}
	}
	
	void FixedUpdate()
	{
		//		if (Input.GetKeyDown (KeyCode.P)) {
		//			GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f);
		//		}

		if (sunked) {
			if (transform.position.y > 4.3f) {
				transform.position = new Vector3 (transform.position.x, 4.3f, transform.position.z);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, 0);
			}
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
	
	IEnumerator MyCoroutine2()
	{
		while (timeLeft >= 0) {
			//Debug.Log(timeLeft);
			yield return new WaitForSeconds (1);
			timeLeft--;
		}
	}
	
	IEnumerator MyCoroutine()
	{
		yield return new WaitForSeconds (.7f);
		if (!spat) {
			GetComponent<Rigidbody2D> ().gravityScale = .1f;
			audio.clip = splash2;
			audio.Play ();	
			OnSplash ();
			sunked = true;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") {
			if (col.gameObject.GetComponent<PlayerControl> ().anim.GetCurrentAnimatorStateInfo (0).IsName ("shareBite")) {
				col.gameObject.GetComponent<PlayerControl> ().ateDuck ();
				audio.clip = squeek;
				audio.Play ();

				Destroy (gameObject);

				//Debug.Log("HOLY FUCK YOU JUST ATE THE DUCK!");
			}
		}
	}
	
	void OnSplash()
	{
		// Create a quaternion with a random rotation in the z-axis.
		//Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		
		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(splash, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
	}
	
	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		
		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}
	

}