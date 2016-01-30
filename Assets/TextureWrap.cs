using UnityEngine;
using System.Collections;

public class TextureWrap : MonoBehaviour {


	[SerializeField]
	private LimbController limbController;

	private Renderer renderer;
	private Vector2 offset;
	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (limbController.GetIsDead ())
			return;
		offset.y += 0.6f * Time.deltaTime;
		//renderer.material.SetTextureOffset ("brickwall", offset);
		//print("tex name: " + renderer.materials[ 0 ].GetTexture(0).name);
		renderer.materials[ 0 ].SetTextureOffset ("_MainTex", offset);

	}
}
