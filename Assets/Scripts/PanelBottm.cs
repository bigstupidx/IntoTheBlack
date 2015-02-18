using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Prime31;
using SmartLocalization;

public class PanelBottm : MonoBehaviour 
{

	public GameObject storeWindow;

	public Animator panelAni;
	public Animator cameraAni;
//	public Animator upgradeAni;
//	public Animator SNSAni;
//
//	public GameObject upgradeMenu;
//	public GameObject SNSMenu;
//	public GameObject starAlbum;

	public GameObject tutorialArrow;

	//public GameObject[] bottomMenu;
	public GameObject[] menuWindow;
	public Animator[] menuAnimator;

	// initialize selecte menu is none;
	private int selectedMenu = -1;

	public Text upgrade;
	public Text spaceChat;
	public Text constellation;
	public Text SNS;
	public Text store;
	public Text setting;
	public Text autoplay;

	public GameObject quitWindow;


	void Start()
	{
		quitWindow.SetActive(false);

		// initialize menu object to false;
		for (int i = 0; i < menuWindow.Length; i++)
		{
			menuWindow[i].SetActive(false);
		}

		LanguageManager thisLanguageManager = LanguageManager.Instance;

		upgrade.text = thisLanguageManager.GetTextValue("Menu.Upgrade");
		spaceChat.text = thisLanguageManager.GetTextValue("Menu.Message");
		constellation.text = thisLanguageManager.GetTextValue("Menu.Album");
		SNS.text = thisLanguageManager.GetTextValue("Menu.Sns");
		store.text = thisLanguageManager.GetTextValue("Menu.Store");
		setting.text = thisLanguageManager.GetTextValue("Menu.Setting");
		autoplay.text = thisLanguageManager.GetTextValue("UI.AutoPlay");
			
	}

	void Update()
	{
		if ( Application.platform == RuntimePlatform.Android )
		{
			if( Input.GetKeyDown( KeyCode.Escape ))
			{ 
				if(selectedMenu != -1)
				{
					PopMenu(selectedMenu);
				}
				else
				{
					quitWindow.SetActive(true);
				}
			}
		}
	}

	public void PopMenu(int _menuNumber)
	{
		SoundManager.Instance.PlaySound(0);

		if (selectedMenu == _menuNumber)
		{
			menuAnimator[_menuNumber].SetBool ("isHidden",false);
			menuWindow[_menuNumber].SetActive(false); // if wanna to show disappear animation. disable this code
			selectedMenu = -1; // all menu are closed;
			
		}
		else
		{
			if (selectedMenu != -1)
			{
				menuAnimator[selectedMenu].SetBool ("isHidden",false);
				menuWindow[selectedMenu].SetActive(false); // active false is more safe than just disapear?
			}

			if (menuWindow[_menuNumber].activeSelf == false)
				menuWindow[_menuNumber].SetActive(true);
			
			menuAnimator[_menuNumber].SetBool ("isHidden",true);
			selectedMenu = _menuNumber;
		}
		
	}

	public void ToggleWindow()
	{
		if(panelAni.GetBool("isHidden"))
		{
			panelAni.SetBool("isHidden", false);
			cameraAni.SetBool("isShiftDown",false);
		}
		else
		{
			panelAni.SetBool("isHidden", true);
			cameraAni.SetBool("isShiftDown",true);
		}
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void CancelWindow()
	{
		quitWindow.SetActive(false);
	}

}
