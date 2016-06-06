using UnityEngine;
using System.Collections;

/// <summary>
/// FPS style mouse look
/// </summary>
public class MouseLook : MonoBehaviour
{	
	public enum RotationAxes
	{
		MouseXAndY = 0, MouseX = 1, MouseY = 2
	}

	public RotationAxes axes = RotationAxes.MouseXAndY;
	[Header("Sensitivity")]
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	[Header("Restrictions")]
	[Range(-360f, 360f)]
	public float minimumX = -360F;
	[Range(-360f, 360f)]
	public float maximumX = 360F;
	[Range(-360f, 360f)]
	public float minimumY = -60F;
	[Range(-360f, 360f)]
	public float maximumY = 60F;

	private float rotationX = 0F;
	private float rotationY = 0F;
	private Quaternion originalRotation;
	
	void Start ()
	{
		Rigidbody body = GetComponent<Rigidbody>();
		if ( body != null ) body.freezeRotation = true; 

		originalRotation = transform.localRotation; 
	}

	void Update ()
	{
		rotationX += Input.GetAxis("Mouse X") * sensitivityX;
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		rotationX = ClampAngle (rotationX, minimumX, maximumX); // Don't let our rotations violate our restrictions 
		rotationY = ClampAngle (rotationY, minimumY, maximumY);

		if (axes == RotationAxes.MouseXAndY)
		{
			// Quaternion.AngleAxis(X, Y) returns a rotation on X degrees around Y axis
			// Our X quaternion = rotation on rotationX degress around Y (up) axis
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
			// Combine our rotations with original rotation
			// quaternionA * quaternonB is same as applying the two rotations in sequence
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		} else if (axes == RotationAxes.MouseX)
		{
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
		} else
		{
			Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
			transform.localRotation = originalRotation * yQuaternion;
		}
	}

	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F) angle += 360F;
		if (angle > 360F) angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
}