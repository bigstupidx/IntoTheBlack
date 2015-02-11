using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class FireButton : MonoBehaviour
{
	public void ButtonPressed()
	{
		PlayerController.isFireButton = true;

		Debug.Log ("FireButton : " + PlayerController.isFireButton);
		Debug.Log ("FireRate : " + PlayerController.fireRate);
	}

	public void ButtonUp()
	{
		PlayerController.isFireButton = false;

		Debug.Log ("FireButton : " + PlayerController.isFireButton);
		Debug.Log ("FireRate : " + PlayerController.fireRate);
	}


}
