using UnityEngine;
using System.Collections;

public class EnegyField : MonoBehaviour 
{
	private Transform xForm;
	public float size;
	private float sizeFactor;
	private Vector3 startScale;
	void Awake()
	{
		xForm = this.transform;
		startScale = this.transform.localScale;
	}
	void OnEnable()
	{
		xForm.localScale = startScale;
	//	sizeFactor =  0.6f; // (GameController.engineLevel * 0.3f + 1) * 0.04f; // 0.05 is basic
		StartCoroutine(EmpScale());
	}

	IEnumerator EmpScale()
	{
//		for (int i = 3; i > 0; i--)
//		{
//			xForm.localScale += new Vector3(0.05f,0.05f,0.05f) * i;
//			//yield return new WaitForSeconds(Time.deltaTime);
//		}

		xForm.localScale += new Vector3(0.35f,0.35f,0.35f);

		yield return new WaitForSeconds(1f);
		gameObject.SetActive(false);
	}
}
