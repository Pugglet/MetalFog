using UnityEngine;
using System.Collections;

public class CameraFollowWhenDead : MonoBehaviour {

	[SerializeField]
	private LimbController limbController;

	float initialYOffset = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (limbController.GetIsDead ()) {
			Vector3 pos = limbController.GetCorePos ();

			//print ("Corpse Y:" + pos.y);
			if (initialYOffset == 0.0f) 
			{
				initialYOffset = transform.position.y - pos.y;
			}
			if (pos.y > -15) 
			{
				if (pos.y + initialYOffset < transform.position.y) {
					Vector3 newPos = new Vector3 (transform.position.x, pos.y + initialYOffset, transform.position.z);
					transform.position = newPos;
				}
			}

			return;
		}
	}
}
