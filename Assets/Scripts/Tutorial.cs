using UnityEngine;
using System.Collections;

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
		string[] tr = new string[] {
			"인류의 미래를 건 여행이 시작 되었네요!!!\n 탐사선을 향상 시켜 더 멀리 나아 가요.\n 날아오는 운석을 조심하세요.", 
			"외계 기술이라도 얻지 않는한 저 운석들을 어쩔수가 없는건가? ",
			"우주에는 우리 말고도 다른 생명체가 존재할 확률이 아주 높죠. 진실성 85% "};

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

