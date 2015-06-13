using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class boat : MonoBehaviour {

	public GameObject torpedo;
	public GameObject dCharge;

	public int difficulty = 1;

	public GameObject torpedoInstance1;
	public GameObject torpedoInstance2;
	public GameObject torpedoInstance3;
	public GameObject torpedoInstance4;
	public GameObject torpedoInstance5;

	public GameObject dChargeInstance1;

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

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			launchTorpedoes();
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			dChargeInstance1 = null;
			if(facingRight){
				dChargeInstance1 = Instantiate (dCharge, new Vector3(transform.position.x + 6, transform.position.y ,transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
				dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f * Random.Range(.1f, 1f));
			}else{
				dChargeInstance1 = Instantiate (dCharge, new Vector3(transform.position.x - 6, transform.position.y ,transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
				dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f * Random.Range(.1f, 1f));

			}
			dChargeInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1.5f * 1000f);
		}

		if (Input.GetKeyDown (KeyCode.I)) {
			moveRightB = true;
			moveLeftB = false;
		}

		if (Input.GetKeyDown (KeyCode.U)) {
			moveRightB = false;
			moveLeftB = true;
		}

	}

	void FixedUpdate(){
		if (GetComponent<Transform> ().localScale.x < 0)
			facingRight = false;
		else
			facingRight = true;

		handleTorpedoLogic ();

		handleMovement ();

	}

	void moveRight(){
		GetComponent<Transform> ().localScale = new Vector3 (-1, GetComponent<Transform> ().localScale.y ,GetComponent<Transform> ().localScale.z);
		if (GetComponent<Transform> ().position != moveTargetR.GetComponent<Transform> ().position)
			GetComponent<Transform> ().position = Vector3.MoveTowards (GetComponent<Transform> ().position,
			                                                           moveTargetR.GetComponent<Transform> ().position,
			                                                           .2f);
		else
			moveRightB = false;
	}

	void moveLeft(){
		GetComponent<Transform> ().localScale = new Vector3 (1, GetComponent<Transform> ().localScale.y ,GetComponent<Transform> ().localScale.z);
		if (GetComponent<Transform> ().position != moveTargetL.GetComponent<Transform> ().position)
			GetComponent<Transform> ().position = Vector3.MoveTowards (GetComponent<Transform> ().position,
			                                                           moveTargetL.GetComponent<Transform> ().position,
			                                                           .2f);
		else
			moveLeftB = false;
	}

	void handleMovement(){
		if (moveRightB)
			moveRight ();
		if (moveLeftB)
			moveLeft ();
	}

	void handleTorpedoLogic()
	{
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
		}else {
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
		}else {
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
		}else {
			inPosition5 = true;
		}
		
		if (inPosition1 == true && inPosition2 == true && inPosition3 == true && inPosition4 == true && inPosition5 == true && launched == false) {
			if(facingRight){
				if(torpedoInstance1 != null)
					torpedoInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f);
				if(torpedoInstance2 != null)
					torpedoInstance2.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f);
				if(torpedoInstance3 != null)
					torpedoInstance3.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f);
				if(torpedoInstance4 != null)
					torpedoInstance4.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f);
				if(torpedoInstance5 != null)
					torpedoInstance5.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 1.5f * 1000f);
			} else{
				if(torpedoInstance1 != null)
					torpedoInstance1.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f);
				if(torpedoInstance2 != null)
					torpedoInstance2.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f);
				if(torpedoInstance3 != null)
					torpedoInstance3.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f);
				if(torpedoInstance4 != null)
					torpedoInstance4.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f);
				if(torpedoInstance5 != null)
					torpedoInstance5.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -1.5f * 1000f);
			}
			launched = true;
			
		}
		inPosition1 = false;
		inPosition2 = false;
		inPosition3 = false;
		inPosition4 = false;
		inPosition5 = false;
	}

	void randomizeTargetLocations()
	{
		target1.GetComponent<Transform> ().localPosition = new Vector3 (target1.GetComponent<Transform> ().localPosition.x + Random.Range(-3, 3),
		                                                              target1.GetComponent<Transform> ().localPosition.y,
		                                                              target1.GetComponent<Transform> ().localPosition.z);
		target2.GetComponent<Transform> ().localPosition = new Vector3 (target2.GetComponent<Transform> ().localPosition.x + Random.Range(-3, 3),
		                                                                target2.GetComponent<Transform> ().localPosition.y,
		                                                                target2.GetComponent<Transform> ().localPosition.z);
		target3.GetComponent<Transform> ().localPosition = new Vector3 (target3.GetComponent<Transform> ().localPosition.x + Random.Range(-3, 3),
		                                                                target3.GetComponent<Transform> ().localPosition.y,
		                                                                target3.GetComponent<Transform> ().localPosition.z);
		target4.GetComponent<Transform> ().localPosition = new Vector3 (target4.GetComponent<Transform> ().localPosition.x + Random.Range(-3, 3),
		                                                                target4.GetComponent<Transform> ().localPosition.y,
		                                                                target4.GetComponent<Transform> ().localPosition.z);
		target5.GetComponent<Transform> ().localPosition = new Vector3 (target5.GetComponent<Transform> ().localPosition.x + Random.Range(-3, 3),
		                                                                target5.GetComponent<Transform> ().localPosition.y,
		                                                                target5.GetComponent<Transform> ().localPosition.z);

	}

	void launchTorpedoes()
	{
		spawnRandomTorpedoes();
		randomizeTargetLocations();
		
		
		if(!facingRight){
			if(torpedoInstance1 != null)
				torpedoInstance1.GetComponent<Transform>().localScale = new Vector3((torpedoInstance1.GetComponent<Transform>().localScale.x) * -1,
				                                                                    torpedoInstance1.GetComponent<Transform>().localScale.y,
				                                                                    torpedoInstance1.GetComponent<Transform>().localScale.z);
			if(torpedoInstance2 != null)
				torpedoInstance2.GetComponent<Transform>().localScale = new Vector3((torpedoInstance2.GetComponent<Transform>().localScale.x) * -1,
				                                                                    torpedoInstance2.GetComponent<Transform>().localScale.y,
				                                                                    torpedoInstance2.GetComponent<Transform>().localScale.z);
			if(torpedoInstance3 != null)
				torpedoInstance3.GetComponent<Transform>().localScale = new Vector3((torpedoInstance3.GetComponent<Transform>().localScale.x) * -1,
				                                                                    torpedoInstance3.GetComponent<Transform>().localScale.y,
				                                                                    torpedoInstance3.GetComponent<Transform>().localScale.z);
			if(torpedoInstance4 != null)
				torpedoInstance4.GetComponent<Transform>().localScale = new Vector3((torpedoInstance4.GetComponent<Transform>().localScale.x) * -1,
				                                                                    torpedoInstance4.GetComponent<Transform>().localScale.y,
				                                                                    torpedoInstance4.GetComponent<Transform>().localScale.z);
			if(torpedoInstance5 != null)
				torpedoInstance5.GetComponent<Transform>().localScale = new Vector3((torpedoInstance5.GetComponent<Transform>().localScale.x) * -1,
				                                                                    torpedoInstance5.GetComponent<Transform>().localScale.y,
				                                                                    torpedoInstance5.GetComponent<Transform>().localScale.z);
		}
		
		
		launched = false;

	}

	void spawnRandomTorpedoes ()
	{
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
			randomTorpedo = torpedos[Random.Range(0,(torpedos.Count - 1))];
			torpedos.Remove (randomTorpedo);
			Destroy (randomTorpedo);
		}





		//put each torpedo in an array
	}
}
