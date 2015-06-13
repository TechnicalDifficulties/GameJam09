using UnityEngine;
using System.Collections;

public class tempPlayerScript : MonoBehaviour {


	public bool inZone1 = false;
	public bool inZone2 = false;
	public bool inZone3 = false;
	
	public AudioClip sound1;
	public AudioClip sound2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			GetComponent<AudioSource>().clip = sound1;
			GetComponent<AudioSource>().Play();
		}
		if(Input.GetKeyDown(KeyCode.Return))
		{
			GetComponent<AudioSource>().clip = sound2;
			GetComponent<AudioSource>().Play();
		}
	}

	void OnTriggerStay(Collider col)
	{
		if (col.tag == "zone1") {
			inZone1 = true;
			inZone2 = false;
			inZone3 = false;
		}
		else if (col.tag == "zone2") {
			inZone2 = true;
			inZone1 = false;
			inZone3 = false;
		}
		else if (col.tag == "zone3") {
			inZone3 = true;
			inZone1 = false;
			inZone2 = false;
		}
	}
}
