using UnityEngine;
using System.Collections;

public class ComicCamera : MonoBehaviour {


	public bool stop = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!stop)
			transform.position = new Vector3 (transform.position.x, transform.position.y - 1.3f * Time.deltaTime, transform.position.z);
	
	}
}
