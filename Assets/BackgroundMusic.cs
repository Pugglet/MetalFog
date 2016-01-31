using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

    [SerializeField]
    AudioClip[] clip;
    
    // Use this for initialization
    void Start ()
    {
        int clipCount = clip.Length;
        int AynRand = Random.Range(0, clipCount);

        AudioSource source = GetComponent<AudioSource>();
        source.clip = clip[AynRand];
        source.Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
