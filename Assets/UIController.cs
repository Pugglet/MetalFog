using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	[SerializeField]
	private Text title;
	[SerializeField]
	private Text credits1;
	[SerializeField]
	private Text credits2;
	[SerializeField]
	private Text credits3;

	private float fadeSpeed = 0.5f;
	private float titleAlpha;
	private bool titleFadingOut = false;

	private float credits1alpha = -1.0f;
	private float credits2alpha = -1.3f;
	private float credits3alpha = -1.6f;

	private bool credits1fade = false;
	private bool credits2fade = false;
	private bool credits3fade = false;

	string[] creditsnames = { "MARK PEARCE", "DANIEL LANGSFORD", "SEAN FLANNIGAN" };

	// Use this for initialization
	void Start () {
	
		titleAlpha = 0.0f;

		// randomise credits
		for (int t = 0; t < creditsnames.Length; t++ )
		{
			string tmp = creditsnames[t];
			int r = Random.Range(t, creditsnames.Length);
			creditsnames[t] = creditsnames[r];
			creditsnames[r] = tmp;
		}

		credits1.text = creditsnames [0];
		credits2.text = creditsnames [1];
		credits3.text = creditsnames [2];
	}
	
	// Update is called once per frame
	void Update () {
		if (titleFadingOut)
			titleAlpha -= fadeSpeed * Time.deltaTime;
		else 
			titleAlpha += fadeSpeed * Time.deltaTime;

		if (credits1fade)
			credits1alpha -= fadeSpeed * Time.deltaTime;
		else 
			credits1alpha += fadeSpeed * Time.deltaTime;
		
		if (credits2fade)
			credits2alpha -= fadeSpeed * Time.deltaTime;
		else 
			credits2alpha += fadeSpeed * Time.deltaTime;
		
		if (credits3fade)
			credits3alpha -= fadeSpeed * Time.deltaTime;
		else 
			credits3alpha += fadeSpeed * Time.deltaTime;

		if (titleAlpha > 2.5f)
			titleFadingOut = true;

		if (credits1alpha > 1.5f)
			credits1fade = true;

		if (credits2alpha > 1.5f)
			credits2fade = true;

		if (credits3alpha > 1.5f)
			credits3fade = true;

		// title
		float alphaCap = Cap (titleAlpha);
		Color titleColor = title.color;
		titleColor.a = alphaCap;
		title.color = titleColor;

		// credits

		alphaCap = Cap (credits1alpha);
		titleColor = credits1.color;
		titleColor.a = alphaCap;
		credits1.color = titleColor;

		alphaCap = Cap (credits2alpha);
		titleColor = credits2.color;
		titleColor.a = alphaCap;
		credits2.color = titleColor;

		alphaCap = Cap (credits3alpha);
		titleColor = credits3.color;
		titleColor.a = alphaCap;
		credits3.color = titleColor;
	}

	float Cap(float val)
	{
		if (val < 0.0f)
			return 0.0f;
		if (val > 1.0f)
			return 1.0f;
		return val;
	}
}
