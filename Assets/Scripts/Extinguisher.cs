using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Extinguisher : MonoBehaviour 
{
	public Button button;
	public Text count;
	// Use this for initialization
	void Start () 
	{
		count.text = GameController.extinguisherNumber.ToString();
	}

	void Update()
	{
		if(GameController.extinguisherNumber > 0 && GameController.fireLevel > 0)
		{
			count.text = GameController.extinguisherNumber.ToString();
			button.interactable = true;
		}
		else
		{
			count.text = GameController.extinguisherNumber.ToString();
			button.interactable = false;
		}
	}

}
