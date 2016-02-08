using UnityEngine;
using System.Collections;

public class LimbDestruction : MonoBehaviour {

    [SerializeField]
    GameObject relatedLimb;

    [SerializeField]
    GameObject spawnedLimb;

    [SerializeField]
    AudioClip[] clips;

    AudioSource source;
    bool activated = false;

    // Use this for initialization
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void CollisionCallback() {

        if (activated == false)
            activated = true;
        else
            return;

        relatedLimb.GetComponent<SkinnedMeshRenderer>().enabled = false;
        SpawnLimb();
        PlaySFX();

        int childCount = transform.childCount;

        Debug.Log(relatedLimb.name + " has " + childCount.ToString() + " children");

		for (int i = 0; i < childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            LimbDestruction destructionScript = child.GetComponent<LimbDestruction>();
            destructionScript.CollisionCallback();
            Debug.Log("Child limb destryoed");
        }
    }

    public void SpawnLimb()
    {
        if (spawnedLimb != null)
            Instantiate(spawnedLimb, transform.position, transform.rotation);
    }

    public void PlaySFX()
    {
        int rand = Random.Range(0, clips.Length);
        source.clip = clips[rand];
        source.Play();
    }
}
