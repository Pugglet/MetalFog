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
		transform.position += new Vector3(0, YSpeed, 0) * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other) {
		limbController.NotifyCollision (other);
		//Destroy(gameObject);
	}
}
