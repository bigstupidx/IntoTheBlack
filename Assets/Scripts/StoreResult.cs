using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreResult : MonoBehaviour
{
	public Text resultText;
	public string result;

	void Awake()
	{
		gameObject.SetActive(false);
	}
	// Use this for initialization
	void Start () 
	{
		resultText.text = result;
	}
	
	// Update is called once per frame

	public void CloseWindow()
	{
		gameObject.SetActive(false);
	}
}
