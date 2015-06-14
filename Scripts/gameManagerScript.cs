using UnityEngine;
using System.Collections;

public class gameManagerScript : MonoBehaviour {

	public GameObject musicManager;
	public GameObject player;
	public GameObject boat;
	
	public bool repeat1 = false;
	public bool repeat2 = false;
	public bool repeat3 = false;

	public GameObject fish;
	public GameObject fishInstance1;

	public musicManagerScript mMscript;

	// Use this for initialization
	void Start () {
		mMscript = musicManager.GetComponent<musicManagerScript> ();
		mMscript.changeTrack (mMscript.track1);
		StartCoroutine(repeatSummoning());

	}
	
	// Update is called once per frame
	void Update () {
//		InvokeRepeating("summonSchool", 30, 1f);
//		if (Input.GetKeyDown (KeyCode.F)) {
//			summonSchool(30, );
//		}
	}

	IEnumerator repeatSummoning()
	{
		while(true)
		{
			summonSchool();
			yield return new WaitForSeconds(30);
		}
	}

	void summonSchool()
	{
		int num = 30;
		if (!boat.GetComponent<boat>().facingRight) {
			for (int i = 0; i < num; i++) {
				spawnFish (Random.Range (-40, -60), Random.Range (0, -15), true);
			}
		} else {
			for (int i = 0; i < num; i++) {
				spawnFish (Random.Range (40, 60), Random.Range (0, -15), false);
			}
		}
	}

	void spawnFish(float x, float y, bool direction)
	{
		fishInstance1 = Instantiate (fish, new Vector3(transform.position.x + x, transform.position.y + y ,transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		fishInstance1.GetComponent<Fish> ().facingRight = direction;
	}
}
