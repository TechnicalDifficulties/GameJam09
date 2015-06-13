using UnityEngine;
using System.Collections;

public class clouds : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z);

		if(transform.position.x < -56)
			transform.position = new Vector3 (125, transform.position.y, transform.position.z);
	
	}
}
