using UnityEngine;
using System.Collections;

public class TextureWrap : MonoBehaviour {

	private Renderer renderer;
	private Vector2 offset;
	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		offset.y += 0.2f * Time.deltaTime;
		//renderer.material.SetTextureOffset ("brickwall", offset);
		//print("tex name: " + renderer.materials[ 0 ].GetTexture(0).name);
		renderer.materials[ 0 ].SetTextureOffset ("_MainTex", offset);

	}
}
