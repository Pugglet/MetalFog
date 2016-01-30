using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGameTrigger : MonoBehaviour {
	private Transform core;

	// Use this for initialization
	void Start () {
		core = FindChild (transform, "Root_M");
		if (core == null) {
			print ("Root_M doesn't exist!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		//print ("Pos:" + core.transform.position.y);
		if (Input.GetButtonUp ("Submit")
			|| (Input.GetButtonUp ("Cancel"))
			|| (core.transform.position.y < -2) ) 
		{
			print ("OK start the game!");
			SceneManager.LoadScene ("Temp"); 
		}
	}

	Transform FindChild( Transform target, string name)
	{
		if (target.name == name)
			return target;

		for (int i = 0; i < target.GetChildCount(); ++i)
		{
			return FindChild(target.GetChild(i), name);
		}

		return null;
	}
}
