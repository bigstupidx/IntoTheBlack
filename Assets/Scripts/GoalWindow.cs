using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmartLocalization;

public class GoalWindow : MonoBehaviour
{
	public Text goalTitle;
	public Text goalDistance;
	public Text reward;
	public Text rewardText;

	public Animator windowAni;
	public Image rewardImage;

	public Text close;

	void Start()
	{
		LanguageManager thisLanguageManager = LanguageManager.Instance;
		close.text = thisLanguageManager.GetTextValue("UI.Close");
	}
}