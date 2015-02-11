using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{

//	void OnTriggerEnter(Collider other)
//	{
//		if (other.CompareTag("Bolt") || other.CompareTag("Player"))
//		{
//
//			if (gameObject.name == "White_Skull(Clone)")
//			{
//				SpawnController.Instance.PopRedDust(transform.position);
//
//			}
//			else if (gameObject.name == "Yellow_Star(Clone)")
//			{
//				SpawnController.Instance.PopYellowDust(transform.position);
//				Debug.Log (gameObject.name);
//			}
//			else
//			{
//				SpawnController.Instance.PopDust(transform.position);
//			}
//
//			gameObject.SetActive(false); // using memorypool
//
//			if (other.tag == "Bolt")
//			{
//				other.gameObject.SetActive(false); // using memorypool
//			}
//
//		}
//
//	}
}
