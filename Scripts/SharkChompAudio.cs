using UnityEngine;
using System.Collections;

public class SharkChompAudio : MonoBehaviour {


	public GameObject boat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playChomp1()
	{
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
	}

	public void throwCharge()
	{
		boat.GetComponent<boat> ().throwCharge ();
	}
}
