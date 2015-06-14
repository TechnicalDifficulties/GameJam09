using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class boat : MonoBehaviour {

	public GameObject torpedo;
	public GameObject dCharge;
	public GameObject mine;
	public GameObject deadDuck;
	public GameObject harpoon;

	public int difficulty = 3;

	public GameObject torpedoInstance1;
	public GameObject torpedoInstance2;
	public GameObject torpedoInstance3;
	public GameObject torpedoInstance4;
	public GameObject torpedoInstance5;

	public GameObject dChargeInstance1;
	public GameObject mineInstance1;
	public GameObject deadDuckInstance1;
	public GameObject harpoonInstance1;

	public List<GameObject> torpedos;

	public GameObject target1;
	public GameObject target2;
	public GameObject target3;
	public GameObject target4;
	public GameObject target5;

	public GameObject moveTargetR;
	public GameObject moveTargetL;

	public bool facingRight = true;

	public bool launched = true;
	public bool thrown = true;

	public bool inPosition1 = false;
	public bool inPosition2 = false;
	public bool inPosition3 = false;
	public bool inPosition4 = false;
	public bool inPosition5 = false;

	public bool dInPosition1 = false;

	public bool moveRightB = false;
	public bool moveLeftB = false;

	public bool wentLeft = true;

	public GameObject duckSprite;
	public Animator duckanim;

	public GameObject boatSprite;
	public Animator boatanim;

	public bool ready = true;

	public bool gotHit = false;

	public bool flickering = false;

	public bool dead = false;

	public AudioClip track1;
	public AudioClip track2;
	public AudioClip track3;
	public AudioClip track4;

	public AudioSource audio;

	public Vector3 baseLocalPosition1;
	public Vector3 baseLocalPosition2;
	public Vector3 baseLocalPosition3;
	public Vector3 baseLocalPosition4;
	public Vector3 baseLocalPosition5;

	public bool shot = false;

	public int damage = 0;

	public int health = 100;

	public GameObject player;

	public GameObject wavesSprite;

	public GameObject duckHud;

	void Awake(){
		audio = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		duckanim = duckSprite.GetComponent<Animator>();
		boatanim = boatSprite.GetComponent<Animator>();
		baseLocalPosition1 = target1.GetComponent<Transform> ().localPosition;
		baseLocalPosition2 = target2.GetComponent<Transform> ().localPosition;
		baseLocalPosition3 = target3.GetComponent<Transform> ().localPosition;
		baseLocalPosition4 = target4.GetComponent<Transform> ().localPosition;
		baseLocalPosition5 = target5.GetComponent<Transform> ().localPosition;
	}

	IEnumerator flicker()
	{
		Debug.Log ("flickering");
		for (int i = 0; i < 15; i++) {
			duckSprite.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			yield return new WaitForSeconds (.1f);
			duckSprite.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
			yield return new WaitForSeconds (.1f);
		}
		duckSprite.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		flickering = false;
	}

	// Update is called once per frame
	void Update () {

		if (health <= 70 && health >= 40)
			difficulty = 2;

		if (health < 40)
			difficulty = 1;


		if (health <= 0) {
			if(!dead)
			{
				boatanim.Play("BoatDie");
				audio.pitch = .25f;
				audio.clip = track4;
				audio.Play ();
				deadDuckInstance1 = null;
				
				if (facingRight) {
					deadDuckInstance1 = Instantiate (deadDuck, new Vector3 (transform.position.x + 6, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
					deadDuckInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f * Random.Range (.1f, 1f));
				} else {
					deadDuckInstance1 = Instantiate (deadDuck, new Vector3 (transform.position.x - 6, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
					deadDuckInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f * Random.Range (.1f, 1f));
					
				}
				deadDuckInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1.5f * 1000f);

			}
			dead = true;
			Destroy(duckSprite);
			Destroy(wavesSprite);
			Destroy(duckHud);
			StartCoroutine(Victory());

		}

		if (!dead) {
			if (gotHit) {
				if (!flickering) {
					flickering = true;
					StartCoroutine (flicker ());
				}
			}

//		if (Input.GetKeyDown (KeyCode.P)) {
//			duckanim.SetTrigger("Launch");
//			StartCoroutine(shootXTorpedos(3));
//			//launchTorpedoes();
//		}
//		if (Input.GetKeyDown (KeyCode.O)) {
//			duckanim.SetTrigger("Throw");
//		}
//		if (Input.GetKeyDown (KeyCode.L)) {
//			StartCoroutine(throwXMines(3));
//			//duckanim.SetTrigger("ThrowMine");
//		}
//
//		if (Input.GetKeyDown (KeyCode.I)) {
//
//			StartCoroutine(throwXCharges(6));
//			moveRightB = true;
//			moveLeftB = false;
//		}
//
//		if (Input.GetKeyDown (KeyCode.U)) {
//
//			StartCoroutine(throwXCharges(6));
//			moveRightB = false;
//			moveLeftB = true;
//
//		}

			if (ready) {
				ready = false;
				int choice = (Random.Range (0, 4));
				switch (choice) {
				case 0:
					//shot = false;
					StartCoroutine (throwXMines (6 - difficulty));
					break;
				case 1:
					if (wentLeft) {
						StartCoroutine (throwXCharges (8 - difficulty));
						moveRightB = true;
						moveLeftB = false;
					} else {
						StartCoroutine (throwXCharges (6));
						moveRightB = false;
						moveLeftB = true;
					}
					wentLeft = !wentLeft;
					//shot = false;
					break;
				case 2:
					duckanim.SetTrigger ("Launch");
					StartCoroutine (shootXTorpedos (6 - difficulty));
					//shot = false;
					break;

				case 3:
					if(difficulty == 1)
					{
						duckanim.Play("duckHarpoon");
						ready = true;
						break;

					}
					else{
						if (wentLeft) {
							StartCoroutine (throwXCharges (8 - difficulty));
							moveRightB = true;
							moveLeftB = false;
						} else {
							StartCoroutine (throwXCharges (6));
							moveRightB = false;
							moveLeftB = true;
						}
						wentLeft = !wentLeft;
						//shot = false;
						break;
					}
				}
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (!dead) {
			if (col.tag == "Explosion") {
				if (!gotHit) {
				
					StartCoroutine (handleDamage ());
				}
			}
		}
	}

	IEnumerator Victory()
	{
		yield return new WaitForSeconds (5f);
		//diplay victory window here
		Debug.Log ("You Win!");
	}

	IEnumerator handleDamage(){
		if (!dead) {
			gotHit = true;
			damage++;
			duckanim.SetTrigger ("Hurt");
			health = health - 10;
			audio.clip = track3;
			audio.Play ();
			GetComponent<BoxCollider2D> ().enabled = false;
			yield return new WaitForSeconds (3);
			GetComponent<BoxCollider2D> ().enabled = true;
		
			gotHit = false;
		}
	}

	IEnumerator throwXCharges(int x)
	{
		if (!dead) {
			for (int i = 0; i < x; i ++) {
				yield return new WaitForSeconds (.75f);
				duckanim.SetTrigger ("Throw");
			}
			ready = true;
		}
	}

	IEnumerator shootXTorpedos(int x)
	{
		if (!dead) {
			yield return new WaitForSeconds (2);
			for (int i = 0; i < x; i++) {
				launchTorpedoes ();
				yield return new WaitForSeconds (1.3f);
			}
			ready = true;
		}

	}
	IEnumerator throwXMines(int x)
	{
		for (int i = 0; i < x; i ++) {
			duckanim.SetTrigger ("ThrowMine");
			yield return new WaitForSeconds (2f);

		}
		ready = true;
	}

	public void shootHarpoon()
	{
		if (!dead) {
			harpoonInstance1 = null;
			
			if (facingRight) {
				harpoonInstance1 = Instantiate (harpoon, new Vector3 (transform.position.x + 6, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
				//harpoonInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f * Random.Range (.1f, 1f));
			} else {
				harpoonInstance1 = Instantiate (harpoon, new Vector3 (transform.position.x - 6, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
				//harpoonInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f * Random.Range (.1f, 1f));
				
			}
			//harpoonInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * -1.5f * 1000f);
			float speed = 100f;
			float xDis = harpoonInstance1.GetComponent<Transform>().position.x - player.GetComponent<Transform>().position.x;
			float yDis = harpoonInstance1.GetComponent<Transform>().position.y - player.GetComponent<Transform>().position.y;
			float theta = Mathf.Atan(xDis/yDis);
			float theta2 = Mathf.Atan (yDis/xDis);
			//Debug.Log (xDis);
			Debug.Log(Mathf.Rad2Deg * theta);
			harpoonInstance1.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sin(theta) * -speed,
			                                                                    Mathf.Cos (theta) * -speed);
			harpoonInstance1.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Rad2Deg * theta2));

		}
	}
	
	
	public void throwCharge()
	{
		if (!dead) {
			dChargeInstance1 = null;

			if (facingRight) {
				dChargeInstance1 = Instantiate (dCharge, new Vector3 (transform.position.x + 6, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
				dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f * Random.Range (.1f, 1f));
			} else {
				dChargeInstance1 = Instantiate (dCharge, new Vector3 (transform.position.x - 6, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
				dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f * Random.Range (.1f, 1f));
			
			}
			dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1.5f * 1000f);
		}
	}

	public void throwMine()
	{
		if (!dead) {
			mineInstance1 = null;
			if (facingRight) {
				mineInstance1 = Instantiate (mine, new Vector3 (transform.position.x + 6, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
				mineInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f * Random.Range (.1f, 4f));
			} else {
				mineInstance1 = Instantiate (mine, new Vector3 (transform.position.x - 6, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
				mineInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f * Random.Range (.1f, 4f));
			
			}
			mineInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1.5f * 1000f);
		}
	}


	void FixedUpdate(){
		if (!dead) {
			if (GetComponent<Transform> ().localScale.x < 0)
				facingRight = false;
			else
				facingRight = true;

			handleTorpedoLogic ();

			handleMovement ();
		}

	}

	void moveRight(){
		if (!dead) {
			GetComponent<Transform> ().localScale = new Vector3 (-1, GetComponent<Transform> ().localScale.y, GetComponent<Transform> ().localScale.z);
			if (GetComponent<Transform> ().position != moveTargetR.GetComponent<Transform> ().position)
				GetComponent<Transform> ().position = Vector3.MoveTowards (GetComponent<Transform> ().position,
			                                                           moveTargetR.GetComponent<Transform> ().position,
			                                                           .2f);
			else
				moveRightB = false;
		}
	}

	void moveLeft(){
		if (!dead) {
			GetComponent<Transform> ().localScale = new Vector3 (1, GetComponent<Transform> ().localScale.y, GetComponent<Transform> ().localScale.z);
			if (GetComponent<Transform> ().position != moveTargetL.GetComponent<Transform> ().position)
				GetComponent<Transform> ().position = Vector3.MoveTowards (GetComponent<Transform> ().position,
			                                                           moveTargetL.GetComponent<Transform> ().position,
			                                                           .2f);
			else
				moveLeftB = false;
		}
	}

	void handleMovement(){
		if (!dead) {
			if (moveRightB)
				moveRight ();
			if (moveLeftB)
				moveLeft ();
		}
	}

	void handleTorpedoLogic()
	{
		if (!dead) {
			if (torpedoInstance1 != null) {
				if (torpedoInstance1.GetComponent<Transform> ().position != target1.GetComponent<Transform> ().position && launched == false) {
					torpedoInstance1.GetComponent<Transform> ().position = Vector3.MoveTowards (torpedoInstance1.GetComponent<Transform> ().position,
				                                                                            target1.GetComponent<Transform> ().position,
				                                                                            .5f);
				} else {
					inPosition1 = true;
				}
			} else {
				inPosition1 = true;
			}
			if (torpedoInstance2 != null) {
				if (torpedoInstance2.GetComponent<Transform> ().position != target2.GetComponent<Transform> ().position && launched == false) {
					torpedoInstance2.GetComponent<Transform> ().position = Vector3.MoveTowards (torpedoInstance2.GetComponent<Transform> ().position,
				                                                                            target2.GetComponent<Transform> ().position,
				                                                                            .5f);
				} else {
					inPosition2 = true;
				}
			} else {
				inPosition2 = true;
			}
			if (torpedoInstance3 != null) {
				if (torpedoInstance3.GetComponent<Transform> ().position != target3.GetComponent<Transform> ().position && launched == false) {
					torpedoInstance3.GetComponent<Transform> ().position = Vector3.MoveTowards (torpedoInstance3.GetComponent<Transform> ().position,
				                                                                            target3.GetComponent<Transform> ().position,
				                                                                            .5f);
				} else {
					inPosition3 = true;
				}
			} else {
				inPosition3 = true;
			}
			if (torpedoInstance4 != null) {
				if (torpedoInstance4.GetComponent<Transform> ().position != target4.GetComponent<Transform> ().position && launched == false) {
					torpedoInstance4.GetComponent<Transform> ().position = Vector3.MoveTowards (torpedoInstance4.GetComponent<Transform> ().position,
				                                                                            target4.GetComponent<Transform> ().position,
				                                                                            .5f);
				} else {
					inPosition4 = true;
				}
			} else {
				inPosition4 = true;
			}
			if (torpedoInstance5 != null) {
				if (torpedoInstance5.GetComponent<Transform> ().position != target5.GetComponent<Transform> ().position && launched == false) {
					torpedoInstance5.GetComponent<Transform> ().position = Vector3.MoveTowards (torpedoInstance5.GetComponent<Transform> ().position,
				                                                                            target5.GetComponent<Transform> ().position,
				                                                                            .5f);
				} else {
					inPosition5 = true;
				}
			} else {
				inPosition5 = true;
			}
		
			if (inPosition1 == true && inPosition2 == true && inPosition3 == true && inPosition4 == true && inPosition5 == true && launched == false) {
				if (facingRight) {
					if (torpedoInstance1 != null)
						torpedoInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f);
					if (torpedoInstance2 != null)
						torpedoInstance2.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f);
					if (torpedoInstance3 != null)
						torpedoInstance3.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f);
					if (torpedoInstance4 != null)
						torpedoInstance4.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f);
					if (torpedoInstance5 != null)
						torpedoInstance5.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f);
				} else {
					if (torpedoInstance1 != null)
						torpedoInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f);
					if (torpedoInstance2 != null)
						torpedoInstance2.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f);
					if (torpedoInstance3 != null)
						torpedoInstance3.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f);
					if (torpedoInstance4 != null)
						torpedoInstance4.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f);
					if (torpedoInstance5 != null)
						torpedoInstance5.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f);
				}
				launched = true;
				audio.clip = track1;
				audio.Play ();
			
			}
			inPosition1 = false;
			inPosition2 = false;
			inPosition3 = false;
			inPosition4 = false;
			inPosition5 = false;
		}
	}

	void randomizeTargetLocations()
	{
		if (!dead) {
			target1.GetComponent<Transform> ().localPosition = new Vector3 (baseLocalPosition1.x + Random.Range (-3, 3),
		                                                                baseLocalPosition1.y,
		                                                                baseLocalPosition1.z);
			target2.GetComponent<Transform> ().localPosition = new Vector3 (baseLocalPosition2.x + Random.Range (-3, 3),
		                                                                baseLocalPosition2.y,
		                                                                baseLocalPosition2.z);
			target3.GetComponent<Transform> ().localPosition = new Vector3 (baseLocalPosition3.x + Random.Range (-3, 3),
		                                                                baseLocalPosition3.y,
		                                                                baseLocalPosition3.z);
			target4.GetComponent<Transform> ().localPosition = new Vector3 (baseLocalPosition4.x + Random.Range (-3, 3),
		                                                                baseLocalPosition4.y,
		                                                                baseLocalPosition4.z);
			target5.GetComponent<Transform> ().localPosition = new Vector3 (baseLocalPosition5.x + Random.Range (-3, 3),
		                                                                baseLocalPosition5.y,
		                                                                baseLocalPosition5.z);
		}
	}

	void launchTorpedoes()
	{
		if (!dead) {
			spawnRandomTorpedoes ();
			randomizeTargetLocations ();
		
		
			if (!facingRight) {
				if (torpedoInstance1 != null)
					torpedoInstance1.GetComponent<Transform> ().localScale = new Vector3 ((torpedoInstance1.GetComponent<Transform> ().localScale.x) * -1,
				                                                                    torpedoInstance1.GetComponent<Transform> ().localScale.y,
				                                                                    torpedoInstance1.GetComponent<Transform> ().localScale.z);
				if (torpedoInstance2 != null)
					torpedoInstance2.GetComponent<Transform> ().localScale = new Vector3 ((torpedoInstance2.GetComponent<Transform> ().localScale.x) * -1,
				                                                                    torpedoInstance2.GetComponent<Transform> ().localScale.y,
				                                                                    torpedoInstance2.GetComponent<Transform> ().localScale.z);
				if (torpedoInstance3 != null)
					torpedoInstance3.GetComponent<Transform> ().localScale = new Vector3 ((torpedoInstance3.GetComponent<Transform> ().localScale.x) * -1,
				                                                                    torpedoInstance3.GetComponent<Transform> ().localScale.y,
				                                                                    torpedoInstance3.GetComponent<Transform> ().localScale.z);
				if (torpedoInstance4 != null)
					torpedoInstance4.GetComponent<Transform> ().localScale = new Vector3 ((torpedoInstance4.GetComponent<Transform> ().localScale.x) * -1,
				                                                                    torpedoInstance4.GetComponent<Transform> ().localScale.y,
				                                                                    torpedoInstance4.GetComponent<Transform> ().localScale.z);
				if (torpedoInstance5 != null)
					torpedoInstance5.GetComponent<Transform> ().localScale = new Vector3 ((torpedoInstance5.GetComponent<Transform> ().localScale.x) * -1,
				                                                                    torpedoInstance5.GetComponent<Transform> ().localScale.y,
				                                                                    torpedoInstance5.GetComponent<Transform> ().localScale.z);
			}
		
		
			launched = false;
		}
	}

	void spawnRandomTorpedoes ()
	{
		if (!dead) {
			int start = 0;
			int chance = 1;
			int total = 5;

			torpedoInstance1 = null;
			torpedoInstance2 = null;
			torpedoInstance3 = null;
			torpedoInstance4 = null;
			torpedoInstance5 = null;



			torpedoInstance1 = Instantiate (torpedo, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
			torpedoInstance2 = Instantiate (torpedo, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
			torpedoInstance3 = Instantiate (torpedo, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
			torpedoInstance4 = Instantiate (torpedo, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
			torpedoInstance5 = Instantiate (torpedo, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;

			torpedos.Clear ();

			torpedos.Add (torpedoInstance1);
			torpedos.Add (torpedoInstance2);
			torpedos.Add (torpedoInstance3);
			torpedos.Add (torpedoInstance4);
			torpedos.Add (torpedoInstance5);

			GameObject randomTorpedo;

			for (int i = 0; i < difficulty; i++) {
				randomTorpedo = torpedos [Random.Range (0, (torpedos.Count - 1))];
				torpedos.Remove (randomTorpedo);
				Destroy (randomTorpedo);
			}
		}
	}
}
