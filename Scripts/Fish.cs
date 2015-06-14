using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {

	public Sprite fish1;
	public Sprite fish2;
	public Sprite fish3;
	public Sprite fish4;
	public Sprite fish5;

	public GameObject sprite;

	public float speed = 3;
	public bool facingRight = true;


	void Awake()
	{

	}

	// Use this for initialization
	void Start () {
		getRandomFish ();
		getRandomColor ();
		speed = Random.Range (5, 8);
		Destroy(gameObject, 20);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") {
			if (col.gameObject.GetComponent<PlayerControl> ().anim.GetCurrentAnimatorStateInfo (0).IsName ("shareBite")) {
				col.gameObject.GetComponent<PlayerControl> ().ateFish ();
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "Explosion") {
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (facingRight) {
			transform.position = new Vector3 (transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		} else {
			transform.position = new Vector3 (transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
		}
	}

	void getRandomFish()
	{
		int choice = Random.Range (0,5);
		switch (choice) {
		case 0:
			sprite.GetComponent<SpriteRenderer>().sprite = fish1;
			break;
		case 1:
			sprite.GetComponent<SpriteRenderer>().sprite = fish2;
			break;
		case 2:
			sprite.GetComponent<SpriteRenderer>().sprite = fish3;
			break;
		case 3:
			sprite.GetComponent<SpriteRenderer>().sprite = fish4;
			break;
		case 4:
			sprite.GetComponent<SpriteRenderer>().sprite = fish5;
			break;
			
		}
	}

	void getRandomColor()
	{
//		Pink: 202 107 125
//		Blue 50 62 165
//		LGreen 75 202 160
//		LMagenta 156 73 118
//		Violet 131 61 189
//		LYellow 222 222 125
//		Turquoise 47 214 222
//		Red 173 45 31
		int choice = Random.Range (0, 8);
		switch (choice) {
		case 0:
			sprite.GetComponent<SpriteRenderer>().color = new Color(200f/255f, 107f/255f, 125f/255f);
			break;
		case 1:
			sprite.GetComponent<SpriteRenderer>().color = new Color(50f/255f, 62f/255f, 165f/255f);
			break;
		case 2:
			sprite.GetComponent<SpriteRenderer>().color = new Color(75f/255f, 202f/255f, 160f/255f);
			break;
		case 3:
			sprite.GetComponent<SpriteRenderer>().color = new Color(156f/255f, 73f/255f, 118f/255f);
			break;
		case 4:
			sprite.GetComponent<SpriteRenderer>().color = new Color(131f/255f, 61f/255f, 189f/255f);
			break;
		case 5:
			sprite.GetComponent<SpriteRenderer>().color = new Color(222f/255f, 222f/255f, 125f/255f);
			break;
		case 6:
			sprite.GetComponent<SpriteRenderer>().color = new Color(47f/255f, 214f/255f, 222f/255f);
			break;
		case 7:
			sprite.GetComponent<SpriteRenderer>().color = new Color(173f/255f, 45f/255f, 31f/255f);
			break;
		}
	}
}
