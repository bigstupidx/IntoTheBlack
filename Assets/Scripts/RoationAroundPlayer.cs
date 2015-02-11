using UnityEngine;
using System.Collections;

public class RoationAroundPlayer : MonoBehaviour 
{
		public float rotationAngle;
		Transform trParent; 
		
		void Awake ()
		{ 
			trParent = transform.parent;
		} 
		
		void Update ()
		{ 
			transform.RotateAround(trParent.transform.position,Vector3.left, rotationAngle * Time.deltaTime); 
		} 
	}