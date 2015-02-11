using UnityEngine;
using System.Collections;

public class CutSceneCamera : MonoBehaviour
{
	void Start()
	{
	//		rigidbody.velocity = transform.forward * speed;
	
	iTween.MoveTo(gameObject, 
	              iTween.Hash(
		"time", 45, 
		"path", iTweenPath.GetPath ("CameraPath"),
		"easetype",iTween.EaseType.easeInOutQuad,
		"looptype",iTween.LoopType.pingPong
		));
	}
}