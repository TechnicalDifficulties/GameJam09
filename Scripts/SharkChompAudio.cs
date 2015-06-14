using UnityEngine;
using System.Collections;

public class SharkChompAudio : MonoBehaviour {

	public AudioClip track1;
	public AudioClip track2;

	public GameObject boat;

	AudioSource audio;


	void Awake(){
		audio = GetComponent<AudioSource> ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playChomp1()
	{
		audio.clip = track1;
		audio.Play();
	}

	public void playChomp2()
	{
		audio.clip = track2;
		audio.Play();
	}

	public void throwCharge()
	{
		boat.GetComponent<boat> ().throwCharge ();
	}

	public void throwMine()
	{
		boat.GetComponent<boat> ().throwMine ();
	}
}
