using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Linq;
using UnityEngine.UI;

public class SNSManager : MonoBehaviour
{
	protected string lastResponse = "";
	protected Texture2D lastResponseTexture;

	public Text statusText;
	public string status;

	public Text facebookButtonDesc;
	public Text leaderBoardButtonDesc;
	public Text achievmentButtonDesc;

	public GameObject screenShotPanel;
//	public GameObject SNSWindow;
	public RawImage screenShot;
	public WWWForm screenWWWForm;

	public Button uploadButton;
	public Button closeButton;
	public Text screenshotDesc;
	public Text distanceText;

	public Button screenShotButton;

	void Start()
	{
		uploadButton.onClick.AddListener(() => { UploadScreenShot(); });  
		closeButton.onClick.AddListener(() => { CloseWindow(); });
	}

	void CloseWindow()
	{
		screenShotButton.enabled = true;
		Time.timeScale = 1;
		screenShotPanel.SetActive(false);
	}

	public void ScreenShotFeed()
	{
		if (FB.IsInitialized)
		{
			if (!FB.IsLoggedIn)
			{
				CallFBLogin();
				StartCoroutine(TakeScreenshot());
				status = "called screenshot After login";
			}
			else 
			{
				StartCoroutine(TakeScreenshot());
				status = "called screenshot";
			}
		}
		else
		{
			NoticeManager.Instance.SetNotice("페이스북에 사진을 공유하기 위해서는 인터넷 연결이 필요합니다.", 3);
			status = "you cannot connect internet";
		}
	}

//	void Awake()
//	{
//		status = "Ready";
//	}

	void OnEnable()
	{


		Social.localUser.Authenticate((bool success) =>	
		                              {
			if(success)
			{
				leaderBoardButtonDesc.text = "구글 플레이에서 순위를 확인하세요.";
				achievmentButtonDesc.text = "구글 플레이에서 업적을 확인하세요.";

			}
			else
			{
				leaderBoardButtonDesc.text = "구글 플레이에서 순위를 확인하세요. \n 구글 플레이에 로그인되어 있지 않습니다.";
				achievmentButtonDesc.text = "구글 플레이에서 업적을 확인하세요. \n 구글 플레이에 로그인되어 있지 않습니다."; 
			}
		});

	}

	void Update()
	{
		statusText.text = status;
	}

	public void FaceBook()
	{
		if (FB.IsInitialized)
		{
			if (!FB.IsLoggedIn)
			{
				CallFBLogin();
				SoundManager.Instance.PlaySound(8);
				CallFBFeed();
				status = "Login called";
			}
			else 
			{
				SoundManager.Instance.PlaySound(8);
				CallFBFeed();            
				status = "Called FB Feed";
			}
		}
		else
		{
			SoundManager.Instance.PlaySound(10);
			status = "you cannot connect internet";
		}
	}



	#region FB.Init() example

	private void CallFBInit()
	{
		FB.Init(OnInitComplete, OnHideUnity);
	}

	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);

		if (FB.IsLoggedIn)
		{
			facebookButtonDesc.text = "우주 여행의 경험을 친구들과 공유하세요.";
		}
		else
		{
			facebookButtonDesc.text = "우주 여행의 경험을 친구들과 공유하세요. \n (페이스북 계정이 필요합니다.)";
		}
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}
	
	#endregion
	
	#region FB.Login() example
	
	private void CallFBLogin()
	{
		FB.Login("email,publish_actions", LoginCallback);

	}
	
	void LoginCallback(FBResult result)
	{
		if (result.Error != null)
		{
			lastResponse = "Error Response:\n" + result.Error;
			Debug.Log (lastResponse);
		}
		else if (!FB.IsLoggedIn)
		{
			lastResponse = "Login cancelled by Player";
			//NoticeManager.Instance.SetNotice(lastResponse, 5);
		}
		else
		{
			lastResponse = "Login was successful!";
		}
	}
	#endregion

	#region FB.Feed() example
	
	public string FeedToId = "";
	public string FeedLink = "";
	public string FeedLinkName = "";
	public string FeedLinkCaption = "";
	public string FeedLinkDescription = "";
	public string FeedPicture = "";
	public string FeedMediaSource = "";
	public string FeedActionName = "";
	public string FeedActionLink = "";
	public string FeedReference = "";
	public bool IncludeFeedProperties = false;
	private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();
	
	private void CallFBFeed()
	{
		Dictionary<string, string[]> feedProperties = null;
		if (IncludeFeedProperties)
		{
			feedProperties = FeedProperties;
		}
		FB.Feed(
			toId: FeedToId,
			link: FeedLink,
			linkName: FeedLinkName,
			linkCaption: FeedLinkCaption,
			linkDescription: FeedLinkDescription,
			picture: FeedPicture,
			mediaSource: FeedMediaSource,
			actionName: FeedActionName,
			actionLink: FeedActionLink,
			reference: FeedReference,
			properties: feedProperties,
			callback: Callback
			);
	}
	
	#endregion

	protected void Callback(FBResult result)
	{
		lastResponseTexture = null;
		// Some platforms return the empty string instead of null.
		if (!String.IsNullOrEmpty (result.Error))
		{
			lastResponse = "Error Response:\n" + result.Error;
			Debug.Log (lastResponse);
		}
		else if (!String.IsNullOrEmpty (result.Text))
		{
			lastResponse = "Success Response:\n" + result.Text;
			Debug.Log (lastResponse);
			NoticeManager.Instance.SetNotice("당신의 멋진 여행을 페이스북에 공유했습니다.", 5);
		}
		else if (result.Texture != null)
		{
			lastResponseTexture = result.Texture;
			Debug.Log (lastResponse);
		}
		else
		{
			lastResponse = "Empty Response\n";
		}

		screenShotButton.enabled = true;


	}

	private IEnumerator TakeScreenshot()
	{
		audio.Play();
		yield return new WaitForEndOfFrame();
		
		var width = Screen.width;
		var height = Screen.height;
		var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		// Read screen contents into the texture
		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();
		byte[] screenshot = tex.EncodeToPNG();

		var wwwForm = new WWWForm();
		wwwForm.AddBinaryData("image", screenshot, "intotheblack.png");

		screenWWWForm = wwwForm;

		screenShot.texture = tex;
		screenShotPanel.SetActive(true);
		screenShotButton.enabled = false;

		Time.timeScale = 0;

		//FB.API("me/photos", Facebook.HttpMethod.POST, Callback, wwwForm);
	}

	public void UploadScreenShot()
	{	
		 string _description = string.Concat (screenshotDesc.text, "\n" , distanceText.text);

		//string _description = screenshotDesc.text;

		if (FB.IsInitialized)
		{
			if (!FB.IsLoggedIn)
			{
				CallFBLogin();
				
				screenWWWForm.AddField("message", _description);
				FB.API("me/photos", Facebook.HttpMethod.POST, Callback, screenWWWForm);

			}
			else 
			{
				screenWWWForm.AddField("message", _description);
				FB.API("me/photos", Facebook.HttpMethod.POST, Callback, screenWWWForm);

			}
		}

		Time.timeScale = 1;
		screenShotPanel.SetActive(false);
	}

	public void ShowAchievements ()
	{

//		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate((bool success) =>	
		        {
					if(success)
					{
						SoundManager.Instance.PlaySound(8);
						Social.ShowAchievementsUI();
					}
					else
					{
						SoundManager.Instance.PlaySound(10);
						achievmentButtonDesc.text = "구글 플레이에서 업적을 확인하세요. \n 구글 플레이에 로그인 할 수 없습니다.."; 
					}
				});

	}
	
	public void ShowLeaderboard ()
	{
//		PlayGamesPlatform.Activate();
			Social.localUser.Authenticate((bool success) =>	
			                              {
				if(success)
				{
					SoundManager.Instance.PlaySound(8);
					Social.ShowLeaderboardUI();
				}
				else
				{
					SoundManager.Instance.PlaySound(10);
					leaderBoardButtonDesc.text = "구글 플레이에서 순위를 확인하세요. \n 구글 플레이에 로그인 할 수 없습니다.";
				}
			});
	}


//	public void Facebook()
//	{
//		FB.Init (SetInit, OnHideUnity);
//	}
//	
//
//	private void SetInit() 
//	{
//		enabled = true; 
//		// "enabled" is a magic global; this lets us wait for FB before we start rendering
//	}
//	
//	private void OnHideUnity(bool isGameShown)
//	{
//		if (!isGameShown) {
//			// pause the game - we will need to hide
//			Time.timeScale = 0;
//		} else {
//			// start the game back up - we're getting focus again
//			Time.timeScale = 1;
//		}
//	}

}

