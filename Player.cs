using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent( typeof(MouseLook) )]
[RequireComponent( typeof(CharacterController) )]
public class Player : MonoBehaviour
{
	[Tooltip("Can player use objects, that have Usable component (like doors)")]
	public bool canUseUsables = true;
	public bool isDead = false;

	[Header("Movement")]
	public float forwardSpeed = 5;
	public float sidewaySpeed = 3;

	[HideInInspector]
	public float verticalSpeed = 0;
	[Tooltip("Gravity power")]
	public float gravity = Physics.gravity.y;
	public float jumpPower = 20;
	[Tooltip("Speed of player's landing after jump. The smaller value is, the longer and higher player's jump is")]
	public float jumpDamping = 50;
	
	[HideInInspector]
	//public Hand[] hands;
	
	public float maxHealth = 100;
	[SerializeField]
	private float health = 100;


	private CharacterController controller;
	private Camera fpsCamera;
	private Animator fpsCameraAnimator;

	private Slider healthSlider;
	private Text useHint;

	void Awake()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		verticalSpeed = gravity;

		InitHud(); // Getting Health Slider and other UI game objects
		Health = health; // Small "hack" for calling setter for value entered in inspector


		// Getting instances of some components
		controller = GetComponent<CharacterController>();
		fpsCamera = GetComponentInChildren<Camera>(); 
		//hands = GetComponentsInChildren<Hand>();
		/*if( hands == null )
		{
			Debug.LogWarning("Player doesn't have any hands, that makes him/her unable to use any weapons. Add a child object with Hand component to player");
		}*/

		if(fpsCamera == null)
		{
			throw new MissingComponentException("Player object must have a child-object with Camera component on it");
		} else
		{
			fpsCameraAnimator = fpsCamera.gameObject.GetComponent<Animator>();
		}
	}

	void Update()
	{
		if( isDead ) return;

		if( canUseUsables ) 
		{
			ScanForUsables ();
		}

		PerformMovement ();

	}

	void PerformMovement ()
	{
		// Adding "gravity" effect to verticalSpeed to prevent infinite jumps
		if (verticalSpeed > gravity) verticalSpeed -= jumpDamping * Time.deltaTime;
		if (verticalSpeed < gravity) verticalSpeed = gravity;


		// Get forward movement vector in local space
		Vector3 forward = Vector3.forward * forwardSpeed * Input.GetAxis ("Vertical") * Time.deltaTime;
		// Transforming it to world space
		forward = transform.TransformDirection (forward);
		// And removing Y (up) movement from it. It's done to prevent flying
		forward.Set (forward.x, 0, forward.z);

		// As we can't rotate our camera at such direction
		// That makes transform.right facing up or down, 
		// We don't need as complex operations as for forward vector
		// And we just use right direction vector in world space without any transformations
		Vector3 right = transform.right * sidewaySpeed * Input.GetAxis ("Horizontal") * Time.deltaTime;
		Vector3 result = forward + right; // Getting result movement vector

		// We use sqrMagnitude here because we just need to compare magnitude
		// And sqrMagnitude is much faster, that magnitude
		if (result.sqrMagnitude > 0)
		{
			if (fpsCameraAnimator != null) fpsCameraAnimator.SetBool ("IsWalking", true);
		}
		else
		{
			if (fpsCameraAnimator != null) fpsCameraAnimator.SetBool ("IsWalking", false);
		}

		if (Input.GetKey (KeyCode.Space) && controller.isGrounded) verticalSpeed = jumpPower;// * Time.deltaTime;

		// Adding gravity and jump to resulting vector
		result += (Vector3.up * verticalSpeed * Time.deltaTime);
		controller.Move (result);
	}

	/// <summary>
	/// This method performs scanning for reachable Usable objects, displaying "Press ... key to use it" message
	/// and using them on pressing "Use" button (E by default)
	/// </summary>
	void ScanForUsables ()
	{
		bool enableUseHint = false;
		// Doing a raycast in player look direction to get object that's player is looking at
		// And checking if that object has Usable component (or any of Usable derivatives, like UsableDoor)
		RaycastHit hit;
		if (Physics.Raycast (new Ray (transform.position, transform.forward), out hit, 3))
		{
//			Usable usable = hit.collider.gameObject.GetComponent<Usable> ();
/*			if (usable != null)
			{
				enableUseHint = true;
				if (Input.GetButtonDown ("Use")) usable.Use ();
			}*/
		}
		if (useHint != null) useHint.enabled = enableUseHint;
	}

	/// <summary>
	/// Performs searching for "Health Slider" (health indicator) and "Use Hint" ("Press E to use it" message) objects, 
	/// And displaying a warning if there aren't any of them in scene.
	/// </summary>
	private void InitHud()
	{
		GameObject goHealthSlider = GameObject.FindGameObjectWithTag("Health Slider");
		GameObject goUseHint = GameObject.FindGameObjectWithTag("Use Hint");

		// (healthSlider = goHealthSlider.GetComponent<Slider>()) == null
		// means, that such actions must be done:
		//  1. healthSlider = goHealthSlider.GetComponent<Slider>();
		//  2. if( healthSlider == null )
		// It's a very useful form of writing conditions in such situations :)
		if( goHealthSlider == null || (healthSlider = goHealthSlider.GetComponent<Slider>()) == null)
		{
			Debug.LogWarning("There is no Health Slider on the scene. If you have it, make sure that it has \"Health Slider\" tag.");
		}
		if( goUseHint == null || (useHint = goUseHint.GetComponent<Text>()) == null)
		{
			Debug.LogWarning("There is no Use Hint on the scene. If you have it, make sure that it has \"Use Hint\" tag");
		}
	}

	private void ReloadLevel()
	{
		Application.LoadLevel( Application.loadedLevel );
	}

	public float Health
	{
		get { return health; }
		set
		{
			if( value < health )
			{
				if( fpsCameraAnimator != null ) fpsCameraAnimator.SetTrigger("Damaged"); 
			}
			health = value;

			if( health <= 0 && !isDead )
			{
				if( fpsCameraAnimator != null ) fpsCameraAnimator.SetBool("IsDead", true);
				isDead = true;
				// Once player is dead - he/she can't lookup and use his/her hands
				// So we need to disable neccessary components
				GetComponent<MouseLook>().enabled = false;  
			//	foreach(Hand hand in hands) hand.enabled = false;
				Invoke("ReloadLevel", 5); // Reload level after 5 seconds
			}
			if( health > maxHealth) health = maxHealth;
			if( healthSlider != null ) healthSlider.value = health;
		}
	}	

}
