using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class EventDialog : MonoBehaviour 
{

	public Image character;
	public Text dialog;
	public float _closeTime;


	void OnEnable()
	{
		DialogManager.isDialogPop = true;
		StartCoroutine(ClosebyTime());
	}

	IEnumerator ClosebyTime()
	{
		yield return new WaitForSeconds(_closeTime);
		DialogManager.isDialogPop = false;
		gameObject.SetActive(false);
	}

}
