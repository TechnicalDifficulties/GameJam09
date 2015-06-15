using UnityEngine;
using System.Collections;

public class comicManagerScript : MonoBehaviour {

	public AudioClip boom;
	public AudioClip beep;
	public AudioClip alert;
	public AudioClip duck;

	public AudioSource audio;

	public GameObject intro1;
	public GameObject intro2;
	public GameObject intro3;
	public GameObject intro4;
	public GameObject intro5;

	public GameObject cam;


	void Awake(){
		audio = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (doComic());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator doComic(){
		intro1.gameObject.SetActive (true);
		int timeLeft = 3;
		while (timeLeft >= 0) {
			//Debug.Log(timeLeft);
			yield return new WaitForSeconds (1);
			timeLeft--;
			audio.clip = beep;
			audio.Play();

		}
		intro2.gameObject.SetActive (true);
		audio.clip = boom;
		audio.pitch = .25f;
		audio.Play();
		audio.pitch = 1f;
		yield return new WaitForSeconds (1.5f);
		intro3.gameObject.SetActive (true);
		audio.clip = alert;
		audio.Play ();

		yield return new WaitForSeconds (1f);
		cam.GetComponent<ComicCamera> ().stop = true;
		yield return new WaitForSeconds (1f);
		intro1.gameObject.SetActive (false);
		intro2.gameObject.SetActive (false);
		intro3.gameObject.SetActive (false);
		intro4.gameObject.SetActive (true);
		audio.clip = alert;
		audio.Play ();
		yield return new WaitForSeconds (1.5f);
		intro5.gameObject.SetActive (true);
		audio.clip = duck;
		audio.Play ();
		yield return new WaitForSeconds (4f);
		intro4.gameObject.SetActive (false);
		intro5.gameObject.SetActive (false);
		yield return new WaitForSeconds (1.5f);
		Application.LoadLevel(2);
	}

}
