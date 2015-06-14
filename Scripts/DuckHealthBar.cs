using UnityEngine;
using UnityEngine;
using System.Collections;

public class DuckHealthBar : MonoBehaviour {
	
	public GameObject duck;
	public GameObject bar;
	public float scale = 1;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scale = duck.GetComponent<boat>().health * .01f;
		bar.GetComponent<Transform>().localScale = new Vector3(scale * 2, bar.GetComponent<Transform>().localScale.y, bar.GetComponent<Transform>().localScale.z);
		//bar.GetComponent<Transform> ().position = new Vector3 (GetComponent<Transform> ().position.x * scale / 4, bar.GetComponent<Transform> ().position.y, bar.GetComponent<Transform> ().position.z);
		
	}
}