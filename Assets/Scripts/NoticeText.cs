using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NoticeText : MonoBehaviour
{
	// Update is called once per frame
	private Text _noticeText;

	void Start()
	{
		_noticeText = this.GetComponent<Text>();
		_noticeText.text = null;
	}

	public void SetNotice(string _notice, int _timer)
	{
		_noticeText.text = _notice;
		Invoke("DisableNotice", _timer);
	}

	void DisableNotice()
	{
		_noticeText.text = null;
	}
}
