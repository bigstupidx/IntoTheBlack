using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterLoadPage : MonoBehaviour {
	
	public Slider  loadingSlider;
	public Text loadingPercent;
	public Image loadingText;
	public GameObject ship;

	AsyncOperation   async;
	
	bool IsLoadGame = false;
	
	public IEnumerator StartLoad( string strSceneName )
	{
		if (IsLoadGame == false) 
		{
			IsLoadGame = true;
			
			async = Application.LoadLevelAsync ( strSceneName );
			
			while(async.isDone == false) 
			{
				float p = async.progress *100f;
//				int pRounded = Mathf.RoundToInt(p);

//				loadingPercent.text = string.Concat(pRounded.ToString(), " %");
//				Debug.Log (pRounded.ToString());

				//progress 변수로 0.0f ~ 1.0f로 넘어 오기에 이용하면 됩니다.
				loadingSlider.value = p;

				yield return true;
			}

			Shader.WarmupAllShaders();
		}
	}
	
	void Start()
	{
		StartCoroutine( "StartLoad", "Main" );
	}
	
	float fTime = 0.0f;
	float angle = 0.0f;
	//로딩 페이지에서 연속으로 애니메이션 만들때 Update 함수 내에서 만들면 됩니다.
	void Update () {
		fTime += Time.deltaTime;

		angle += 180f * Time.deltaTime;
		ship.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);

		if( fTime >= 1f )
		{
			if(loadingText.enabled)
				loadingText.enabled = false;
			else
				loadingText.enabled = true;


			fTime = 0.0f; 
		}


	//	Script.sliderValue = fTime;
	}
}