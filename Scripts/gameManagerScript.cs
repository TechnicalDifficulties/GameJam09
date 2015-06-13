using UnityEngine;
using System.Collections;

public class gameManagerScript : MonoBehaviour {

	public GameObject musicManager;
	public GameObject player;
	
	public bool repeat1 = false;
	public bool repeat2 = false;
	public bool repeat3 = false;

	public musicManagerScript mMscript;

	// Use this for initialization
	void Start () {
		mMscript = musicManager.GetComponent<musicManagerScript> ();
		mMscript.changeTrack (mMscript.track1);

	}
	
	// Update is called once per frame
	void Update () {
	}
}
