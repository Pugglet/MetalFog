using UnityEngine;
using System.Collections;

public class LimbDestruction : MonoBehaviour {

    [SerializeField]
    GameObject relatedLimb;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void CollisionCallback() {
        relatedLimb.GetComponent<SkinnedMeshRenderer>().enabled = false;

        int childCount = transform.childCount;

        Debug.Log(relatedLimb.name + " has " + childCount.ToString() +  " children");

        for (int i = 0; i < 0; i++)
        {
            Transform child = transform.GetChild(i);
            LimbDestruction destructionScript = child.GetComponent<LimbDestruction>();
            destructionScript.CollisionCallback();
            Debug.Log("Child limb destryoed");
        }       
    }
}
