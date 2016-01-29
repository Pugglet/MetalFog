using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class LimbController : MonoBehaviour
{
    // The target we are following
    [SerializeField]
    private Transform leftHandTarget;

    [SerializeField]
    private Transform rightHandTarget;

    [SerializeField]
    float limitX = 0.75f;

    [SerializeField]
    float limitZ = 0.75f;
    private Animator animator;

    private Vector3 rightHandPos;
    private Vector3 leftHandPos;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        //leftHandTarget.localPosition = new Vector3(0, 0, 0);
        //rightHandTarget.localPosition = new Vector3(0, 0, 0);

        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            //collider.isTrigger = true;
        }

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.useGravity = false;
        }             
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            rightHandTarget.position += new Vector3(0, 0, 0.5f) * Time.deltaTime;
        }

        if (Input.GetKey("down"))
        {
            rightHandTarget.position += new Vector3(0, 0, -0.5f) * Time.deltaTime;
        }

        if (Input.GetKey("right"))
        {
            rightHandTarget.position += new Vector3(0.5f, 0, 0) * Time.deltaTime;
        }

        if (Input.GetKey("left"))
        {
            rightHandTarget.position += new Vector3(-0.5f, 0, 0) * Time.deltaTime;
        }

        float rightHandHorizontal = Input.GetAxis("RightStickX");        
        float rightHandVertical = Input.GetAxis("RightStickY");
        rightHandPos = new Vector3(rightHandTarget.position.x + rightHandHorizontal, rightHandTarget.position.y, rightHandTarget.position.z + rightHandVertical);

        float leftHandHorizontal = Input.GetAxis("LeftStickX");
        float leftHandVertical = Input.GetAxis("LeftStickY");
        leftHandPos = new Vector3(leftHandTarget.position.x + leftHandHorizontal, leftHandTarget.position.y, leftHandTarget.position.z + leftHandVertical);
    }

    void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
        
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandPos);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
    }
}

internal class auto
{
}