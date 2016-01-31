using UnityEngine;
using System.Collections;

public class LimbDestruction : MonoBehaviour {

    [SerializeField]
    GameObject relatedLimb;

    [SerializeField]
    GameObject spawnedLimb;

    bool activated = false;

    // Use this for initialization
    void Start() {

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

        int childCount = transform.childCount;

        Debug.Log(relatedLimb.name + " has " + childCount.ToString() + " children");

        for (int i = 0; i < 0; i++)
        {
            Transform child = transform.GetChild(i);
            LimbDestruction destructionScript = child.GetComponent<LimbDestruction>();
            destructionScript.CollisionCallback();
            Debug.Log("Child limb destryoed");
        }
    }

    public void SpawnLimb()
    {
        if(spawnedLimb != null)
            Instantiate(spawnedLimb, transform.position, transform.rotation);
    }
}
