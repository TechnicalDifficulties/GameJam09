using UnityEngine;
using System.Collections;

public class buttonScript2 : MonoBehaviour {
	
	public GameObject gameManager;
	public GameObject musicManager;
	
	void Awake(){
		
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (gameManager == null) {
			gameManager = GameObject.FindGameObjectWithTag("GameManager");
		}
		
		if (musicManager == null) {
			musicManager = GameObject.FindGameObjectWithTag("MusicManager");
		}
		
	}
	
	void OnMouseDown(){
		//Debug.Log ("Clicked");
//		if (!gameManager.GetComponent<gameManagerScript> ().tutorial) {
//			musicManager.GetComponent<musicManagerScript>().playBossMusic();
//			gameManager.GetComponent<gameManagerScript> ().tutorial = true;
//		}
		
		Application.LoadLevel(4);
	}
}
