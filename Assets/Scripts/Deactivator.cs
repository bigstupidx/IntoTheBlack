using UnityEngine;
using System.Collections;

public class Deactivator : MonoBehaviour 
{
	void OnTriggerEnter (Collider other)
	{
		other.gameObject.SetActive(false);
	}
}
