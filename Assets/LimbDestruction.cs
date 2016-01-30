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

        foreach (SkinnedMeshRenderer mesh in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            mesh.enabled = false;
        }
    }
}
