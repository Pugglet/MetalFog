using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	[SerializeField]
	private float YSpeed;

	[SerializeField]
	private LimbController limbController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (limbController.GetIsDead ()) {
			YSpeed = 0;
			return;
		}
		transform.position += new Vector3(0, YSpeed, 0) * Time.deltaTime;

		if (transform.position.y > 2) {
			Vector3 newPos = new Vector3(0, -120, 0);
			transform.position = newPos;
		}
	}

	void OnTriggerEnter(Collider other) {
		limbController.NotifyCollision (other);

		//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//cube.transform.position = other.//new Vector3(0, 0.5F, 0);
		//Destroy(gameObject); 
	}

	void OnCollisionEnter(Collision collision) {
		limbController.NotifyCollision (collision.collider);

		// If you want collision cubes...:
		
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		cube.transform.position = collision.contacts [0].point;//new Vector3(0, 0.5F, 0);
		
	}
}
