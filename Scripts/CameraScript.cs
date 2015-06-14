using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	public int level;
	
	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			Vector3 point = Camera.main.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

			if(level == 0){
				transform.position = new  Vector3(4.6f,transform.position.y,transform.position.z);
				if(transform.position.y < 3)
					transform.position = new  Vector3(transform.position.x,3,transform.position.z);
			}

			if(level == 1){
				transform.position = new  Vector3(-4.77f,transform.position.y,transform.position.z);
				if(transform.position.y < 8.14f)
					transform.position = new  Vector3(transform.position.x,8.14f,transform.position.z);
			}
		}
		
	}
}