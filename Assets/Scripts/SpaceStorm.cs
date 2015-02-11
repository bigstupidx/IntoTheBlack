using UnityEngine;
using System.Collections;

public class SpaceStorm : MonoBehaviour 
{
	void OnEnable()
	{
		NoticeManager.Instance.SetNotice("살아남은 외계 생명체가 우주 폭풍을 일으켰습니다.\n 탐사선의 속도가 줄어 듭니다.", 5);
		GameController.Instance.SlowMotion();
	}

}
