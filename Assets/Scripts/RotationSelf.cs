using UnityEngine;
using System.Collections;

public class RotationSelf : MonoBehaviour
{
	public float rotationSpeed;

//	private float currentx;

	private float mAngle = 0.0f;

	void Update()
	{
		
		mAngle += 360.0f * Time.deltaTime * rotationSpeed; 
		
		if (mAngle > 360)
		{
			mAngle -= 360;
		}
		
		this.gameObject.transform.localRotation = Quaternion.Euler(0.0f, mAngle, 0.0f);
	}
}