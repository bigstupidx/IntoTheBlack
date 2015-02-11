using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour 
{
	public float tumble;

	void OnEnable()
	{
		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
	}
}

