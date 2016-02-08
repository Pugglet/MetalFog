using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

    [SerializeField]
    AudioClip[] clip;

	AudioSource source;
    
    // Use this for initialization
    void Start ()
    {
		source = GetComponent<AudioSource>();
    }

	void PlayRandom()
	{
		int clipCount = clip.Length;
		int AynRand = Random.Range(0, clipCount);

		source.clip = clip[AynRand];

		if(source.isPlaying == false)
			source.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {		
		if(source.isPlaying == false)
			PlayRandom();
	}
}
