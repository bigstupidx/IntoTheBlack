using UnityEngine;
using System.Collections;
using SmartLocalization;

public class Tutorial : MonoBehaviour
{
	public GameObject dialogWindow;
	public GameObject arrow;
	private int isTutorialDone;

	void Start()
	{
		isTutorialDone = PlayerPrefs.GetInt("isTutorialDone");

		if (isTutorialDone != 1)
		{
			StartCoroutine (TutorialPop());
		}

	}


	IEnumerator  TutorialPop()
	{
		LanguageManager thisLanguageManager = LanguageManager.Instance;

		string[] tr = new string[] {
			thisLanguageManager.GetTextValue("Tutorial.1"), 
			thisLanguageManager.GetTextValue("Tutorial.2"),
			thisLanguageManager.GetTextValue("Tutorial.3")};

//		EventDialog dialogScript = dialogWindow.GetComponent<EventDialog>();

		yield return new WaitForSeconds(3.0f);

		dialogWindow.SetActive(true);
		DialogManager.Instance.SetDialog(1,tr[0]);

		SoundManager.Instance.PlaySound(3);

		yield return new WaitForSeconds(8.0f);

		dialogWindow.SetActive(true);
		DialogManager.Instance.SetDialog(3,tr[1]);


		yield return new WaitForSeconds(8.0f);

		dialogWindow.SetActive(true);
		DialogManager.Instance.SetDialog(4,tr[2]);


		PlayerPrefs.SetInt ("isTutorialDone",1);

	}
}

