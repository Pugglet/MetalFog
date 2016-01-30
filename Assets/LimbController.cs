using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    int limbLossFailCount = 2;

    int limbLossCount = 0;

    GameObject lastHitObject = null;

    private Animator animator;
	private Collider coll;

    private Vector3 rightHandPos;
	private Vector3 leftHandPos;
	private Vector3 rightFootPos;
	private Vector3 leftFootPos;

	private Transform core;
	private Vector3 corePos;

	private float leftFootExtension;
	private float rightFootExtension;

	private Quaternion leftHandRotation;
	private Quaternion rightHandRotation;

	private bool isDead;

	public bool GetIsDead()
	{
		return isDead;
	}

	public Vector3 GetCorePos ()
	{
		return corePos;
	}


    // Use this for initialization
    void Start()
    {
		animator = GetComponent<Animator>();

		//coll = GetComponent<Collider>();

		//animator.enabled = false;
        //leftHandTarget.localPosition = new Vector3(0, 0, 0);
        //rightHandTarget.localPosition = new Vector3(0, 0, 0);

        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            //collider.enabled = false;
			//collider.isTrigger = true;
			//collider.enabled = true;
        }

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.useGravity = false;
        }             

		core = FindChild (transform, "Root_M");
		if (core == null) {
			print ("Root_M doesn't exist!");
		}
    }

	Transform FindChild( Transform target, string name)
	{
		if (target.name == name)
			return target;

		for (int i = 0; i < target.GetChildCount(); ++i)
		{
			return FindChild(target.GetChild(i), name);
		}

		return null;
	}

    // Update is called once per frame
    void Update()
	{
		corePos = core.transform.position;
		if (isDead)
		{
			if (Input.GetButtonUp ("Submit")) {
				SceneManager.LoadScene ("Temp"); 
			} else if (Input.GetButtonUp ("Cancel")) {
				SceneManager.LoadScene ("exterior_scene"); 
			}

			return;
		}

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
    }

    void OnAnimatorIK()
    {
        //Transform leftWristTransform = 
        leftHandRotation = Quaternion.identity;//animator.GetBoneTransform (HumanBodyBones.LeftLowerArm).localRotation;
        rightHandRotation = Quaternion.identity;// animator.GetBoneTransform (HumanBodyBones.RightLowerArm).localRotation;

        animator.SetBoneLocalRotation(HumanBodyBones.LeftHand, leftHandRotation);
        animator.SetBoneLocalRotation(HumanBodyBones.RightHand, rightHandRotation);

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

	public void NotifyCollision(Collider bodyPart)
	{
		if (isDead)
			return;
		
		print ("My " + bodyPart.name + "Collided with an obstacle!");

        if (bodyPart.name == "Elbow_L" || bodyPart.name == "Elbow_R" ||
            bodyPart.name == "Shoulder_L" || bodyPart.name == "Shoulder_R" ||
            bodyPart.name == "Hip_L" || bodyPart.name == "Hip_R" ||
            bodyPart.name == "Knee_L" || bodyPart.name == "Knee_R") {

            // Lost a limb!
            //bodyPart.transform.localScale = Vector3.zero;
            //bodyPart.enabled = false;
            bodyPart.gameObject.GetComponent<LimbDestruction>().CollisionCallback();

            if (lastHitObject == null)
            {
                limbLossCount++;
                lastHitObject = bodyPart.gameObject;
                return;
            }

            string hitID = bodyPart.transform.name;
            string hitParentID = bodyPart.transform.parent.name;
            string lastHitParentID = lastHitObject.transform.parent.name;

            if (hitID != lastHitParentID &&
                hitParentID != lastHitObject.name &&
                hitID != lastHitObject.name)
            {
                limbLossCount++;
            }

            lastHitObject = bodyPart.gameObject;

            if (limbLossCount >= limbLossFailCount)
            {
                // You Lose!!
				isDead = true;
                animator.enabled = false;

                foreach (Collider collider in GetComponentsInChildren<Collider>())
                {
                    if (bodyPart.transform.localScale != Vector3.zero)
                    {
                        collider.isTrigger = false;
                    }
                    //collider.enabled = true;
                }

                foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
                {
                    rb.useGravity = true;
                }

                //coll.attachedRigidbody.useGravity = true;
                //Vector3 pushForce = new Vector3(0.0f, 10.0f, 0.0f);
                //GameObject obj = GameObject.Find ("Root_M");
                //Rigidbody pelvis = obj.GetComponent<Rigidbody> (); //transform.Find<Rigidbody> ("Root_M");//.GetComponent<Rigidbody> ();
                //pelvis.AddForce(pushForce);
            }
        }
    }
}

internal class auto
{
}