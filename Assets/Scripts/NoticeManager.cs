using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoticeManager : MonoBehaviour
{
	private static NoticeManager _instance;
	public static NoticeManager Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType<NoticeManager>();

				if(!_instance)
				{
					Debug.Log ("when is this called?");
//					GameObject container = new GameObject();
//					container.name = "NoticeManagerContainer";
//					_instance = container.AddComponent(typeof(NoticeManager)) as NoticeManager;
				}
			}
			
			return _instance;
		}
	}

	public GameObject NoticePanel;
	public Text noticeText;

	void Start()
	{
		NoticePanel.SetActive(false);
		noticeText.text ="";
	}


	public void SetNotice(string _notice, float _timer)
	{
		noticeText.text = _notice;
		NoticePanel.SetActive(true);

		StartCoroutine(NoticeBlink(_timer));
	}

	IEnumerator NoticeBlink (float _timer)
	{
		yield return new WaitForSeconds(_timer);

		NoticePanel.SetActive(false);
	}

//	void OnDsable()
//	{
//		noticeText.text="";
//	}
}
