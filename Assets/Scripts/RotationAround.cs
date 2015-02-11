using UnityEngine;
using System.Collections;

public class RotationAround : MonoBehaviour
{ 
	public float rotationAngle;
	Transform trParent; 

	private float [] timeAccel;
	private float realAccel;

	void Awake ()
	{ 
		trParent = this.transform.parent;
		realAccel = 0;
	} 

	void Update ()
	{ 
		realAccel = (float) GameController.timeLevel * 0.3f;

		transform.RotateAround(trParent.transform.position,Vector3.forward, rotationAngle * realAccel * Time.deltaTime);

	} 
}