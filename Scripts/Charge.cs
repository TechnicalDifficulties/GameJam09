using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.
	public GameObject splash;
	public GameObject countSprite;
	public Animator anim;

	public int timeLeft;
	public bool exploded = false;

	public bool tutorial = false;
	public bool spat = false;
	public bool sank = true;

	void Awake()
	{
		if (!tutorial) {
			anim = countSprite.GetComponent<Animator> ();
			timeLeft = Random.Range (3, 9);
			switch (timeLeft) {
			case 9:
				anim.Play ("countdown");
				break;
			case 8:
				anim.Play ("count8");
				break;
			case 7:
				anim.Play ("count7");
				break;
			case 6:
				anim.Play ("count6");
				break;
			case 5:
				anim.Play ("count5");
				break;
			case 4:
				anim.Play ("count4");
				break;
			case 3:
				anim.Play ("count3");
				break;
			case 2:
				anim.Play ("count2");
				break;
			case 1:
				anim.Play ("count1");
				break;
			case 0:
				anim.Play ("count0");
				break;
			}
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

	void Update()
	{
//		if (Input.GetKeyDown (KeyCode.P)) {
//			GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f);
//		}
		if (!tutorial) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("explode")) {
				if (!exploded) {

					exploded = true;
					OnExplode ();

					Destroy (gameObject);
				}
			}
		}

	}

	void FixedUpdate(){
		if (!tutorial) {
			if(transform.position.y >= 3  && sank)
			{
				GetComponent<Rigidbody2D> ().gravityScale = 3f;
			}
			else{
				GetComponent<Rigidbody2D> ().gravityScale = Random.Range (.1f, .3f);
			}
		}
	}

	IEnumerator MyCoroutine2()
	{
		while (timeLeft >= 0) {
			//Debug.Log(timeLeft);
			yield return new WaitForSeconds (1);
			sank = true;

			timeLeft--;
		}
	}

	IEnumerator MyCoroutine()
	{
		yield return new WaitForSeconds (.65f);
		if (!spat) {

			GetComponent<Rigidbody2D> ().gravityScale = Random.Range (.1f, .3f);
			AudioSource audio = GetComponent<AudioSource> ();
			audio.Play ();	
			OnSplash ();
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
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		// If it hits an enemy...
		if(col.tag == "Player")
		{
			// ... find the Enemy script and call the Hurt function.
			//col.gameObject.GetComponent<Enemy>().Hurt();

			// Call the explosion instantiation.
			//OnExplode();

			// Destroy the rocket.
			//Destroy (gameObject);
		}
		// Otherwise if it hits a bomb crate...
		else if(col.tag == "BombPickup")
		{
			// ... find the Bomb script and call the Explode function.
			col.gameObject.GetComponent<Bomb>().Explode();

			// Destroy the bomb crate.
			Destroy (col.transform.root.gameObject);

			// Destroy the rocket.
			Destroy (gameObject);
		}
		// Otherwise if the player manages to shoot himself...
		else if(col.gameObject.tag != "Charge")
		{
			// Instantiate the explosion and destroy the rocket.
			OnExplode();
			Destroy (gameObject);
		}
	}
}
