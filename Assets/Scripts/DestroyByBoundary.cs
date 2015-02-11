using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour 
{
	void OnTriggerExit (Collider other)
	{
		if(other.tag == "Bolt" || other.tag == "Asteroid")
		{
			other.gameObject.SetActive(false);
		}
	}
}
