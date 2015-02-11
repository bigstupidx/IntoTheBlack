using UnityEngine;
using System.Collections;

public class playerrotation : MonoBehaviour 
{
	
	public float RotateSpeedAlongX;
	public float RotateSpeedAlongY;
	public float RotateSpeedAlongZ;

	// Use this for initialization

	void Update()
	{
		// Slowly rotate the object around its axis at 1 degree/second * variable.
		transform.Rotate(Vector3.up * Time.deltaTime * RotateSpeedAlongY);
		transform.Rotate(Vector3.forward * Time.deltaTime * RotateSpeedAlongZ);
		transform.Rotate(Vector3.right * Time.deltaTime * RotateSpeedAlongX);
	}
}
