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
	private Transform leftFootTarget;

	[SerializeField]
	private Transform rightFootTarget;

	[SerializeField]
	private Transform leftKneeHint;

	[SerializeField]
	private Transform rightKneeHint;

    [SerializeField]
    float limitX = 0.75f;

    [SerializeField]
    float limitZ = 0.75f;
    private Animator animator;

    private Vector3 rightHandPos;
	private Vector3 leftHandPos;
	private Vector3 rightFootPos;
	private Vector3 leftFootPos;

	private float leftFootExtension;
	private float rightFootExtension;

	private Quaternion leftHandRotation;
	private Quaternion rightHandRotation;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        //leftHandTarget.localPosition = new Vector3(0, 0, 0);
        //rightHandTarget.localPosition = new Vector3(0, 0, 0);

        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
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
            rightFootTarget.position += new Vector3(0, 0, 0.5f) * Time.deltaTime;
        }

        if (Input.GetKey("down"))
        {
			rightFootTarget.position += new Vector3(0, 0, -0.5f) * Time.deltaTime;
        }

        if (Input.GetKey("right"))
        {
			rightFootTarget.position += new Vector3(0.5f, 0, 0) * Time.deltaTime;
        }

        if (Input.GetKey("left"))
        {
			rightFootTarget.position += new Vector3(-0.5f, 0, 0) * Time.deltaTime;
        }

		float handRadiusHMultiplier = 0.3f;
		float handRadiusVMultiplier = 0.7f;

		float rightHandHorizontal = Input.GetAxis("RightStickX") * handRadiusHMultiplier;        
		float rightHandVertical = Input.GetAxis("RightStickY") * handRadiusVMultiplier;
        rightHandPos = new Vector3(rightHandTarget.position.x + rightHandHorizontal, rightHandTarget.position.y, rightHandTarget.position.z + rightHandVertical);

		float leftHandHorizontal = Input.GetAxis("LeftStickX") * handRadiusHMultiplier;
		float leftHandVertical = Input.GetAxis("LeftStickY") * handRadiusVMultiplier;
        leftHandPos = new Vector3(leftHandTarget.position.x + leftHandHorizontal, leftHandTarget.position.y, leftHandTarget.position.z + leftHandVertical);


		rightFootExtension = Input.GetAxis("RightTrigger") * 0.5f;
		rightFootPos = new Vector3(rightFootTarget.position.x, rightFootTarget.position.y, rightFootTarget.position.z + rightFootExtension);

		leftFootExtension = Input.GetAxis("LeftTrigger") * 0.5f;
		leftFootPos = new Vector3(leftFootTarget.position.x, leftFootTarget.position.y, leftFootTarget.position.z + leftFootExtension);

		//Transform leftWristTransform = 
		leftHandRotation = Quaternion.identity;//animator.GetBoneTransform (HumanBodyBones.LeftLowerArm).localRotation;
		rightHandRotation = Quaternion.identity;// animator.GetBoneTransform (HumanBodyBones.RightLowerArm).localRotation;

		animator.SetBoneLocalRotation (HumanBodyBones.LeftHand, leftHandRotation);
		animator.SetBoneLocalRotation (HumanBodyBones.RightHand, rightHandRotation);
    }

    void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
        
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandPos);
		//animator.SetIKRotation (AvatarIKGoal.RightHand, leftHandRotation);//rightHandTarget.rotation);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos);
		//animator.SetIKRotation (AvatarIKGoal.LeftHand, rightHandRotation);//leftHandTarget.rotation);

		// knee hints
		animator.SetIKHintPosition(AvatarIKHint.LeftKnee, leftKneeHint.position);
		animator.SetIKHintPosition(AvatarIKHint.RightKnee, rightKneeHint.position);
		animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, leftFootExtension);
		animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, rightFootExtension);

		// elbow hints
		animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftKneeHint.position);
		animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightKneeHint.position);
		animator.SetIKHintPositionWeight (AvatarIKHint.LeftElbow, 1.0f);//0.5f);
		animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1.0f);//0.5f);

		animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos);
		//animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootTarget.rotation);

		animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos);
		//animator.SetIKRotation (AvatarIKGoal.LeftFoot, leftFootTarget.rotation);
    }
}

internal class auto
{
}