using UnityEngine;
using System.Collections;
using Prime31;

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

	void Start()
	{
		// initialize menu object to false;
		for (int i = 0; i < menuWindow.Length; i++)
		{
			menuWindow[i].SetActive(false);
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

}
