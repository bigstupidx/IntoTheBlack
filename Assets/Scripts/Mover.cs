using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
	[Range(0,2)]
	public  int direction;
	public float speed;


	void OnEnable()
	{

		switch(direction)
		{
		case 0:
			rigidbody.velocity = transform.forward * speed;
			break;
		case 1:
			rigidbody.velocity = transform.up * speed;
			break;
		case 2:
			rigidbody.velocity = transform.right * speed;
			break;
		default:
			rigidbody.velocity = transform.forward * speed;
			break;
		}
	}
}
