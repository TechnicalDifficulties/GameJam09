using UnityEngine;
using System.Collections;

public class mine : MonoBehaviour {
	float rot;
	public GameObject explosion;
	public GameObject splash;	
	public float sinkTime;
	public float rDir;
	public bool tutorialMine = false;

	// Use this for initialization
	void Start () {
		if (!tutorialMine) {
			//GetComponent<Rigidbody2D> ().gravityScale = .1f;
			sinkTime = Random.Range (2f, 4f);


			StartCoroutine (MyCoroutine ());
			StartCoroutine (MyCoroutine2 ());
		}
		if (Random.Range (0, 2) == 1)
			rDir = -.1f;
		else
			rDir = .1f;

		rot = transform.rotation.z;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rot = rot + rDir;
		transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.x, transform.rotation.y, rot)); 
	}

	IEnumerator MyCoroutine()
	{
		yield return new WaitForSeconds (.65f);
		GetComponent<Rigidbody2D> ().gravityScale = Random.Range(.1f, .5f);
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
		OnSplash ();
	}

	IEnumerator MyCoroutine2()
	{
		yield return new WaitForSeconds (sinkTime);
		GetComponent<Rigidbody2D> ().gravityScale = 0;
		GetComponent<Rigidbody2D> ().velocity = Vector3.zero;

	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		//Debug.Log (col);
		OnExplode ();
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

		Destroy (gameObject);
	}
}
