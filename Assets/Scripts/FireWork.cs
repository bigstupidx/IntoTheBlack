using UnityEngine;
using System.Collections;

public class FireWork : MonoBehaviour
{

	void OnEnable()
	{
		NoticeManager.Instance.SetNotice("나쁜지 착한지 알수 없는 외계 생명체를 처치했습니다.\n 일단 신나게 축포를 쏘아 봅시다.", 5f);
		// do smothing;
	}
	
}