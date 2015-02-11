using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JupiterSkill : MonoBehaviour 
{
		public float coolTime;
		public Button skillButton;
		public Slider coolTimeSlider;
		private float timer;
		//	public GameObject moon;
		
		void Awake()
		{
			skillButton.interactable = false;
			coolTimeSlider.maxValue = coolTime;
			timer = 100f;
		}
		
		void Update()
		{
			timer += Time.deltaTime;
			coolTimeSlider.value = timer;
			
			if (timer >= coolTime && !skillButton.interactable)
			{
				skillButton.interactable = true;
			}

			if(GameController.autoFight == true && skillButton.interactable)
				FireShot();

			
		}
		
		
		public void FireShot()
		{
			skillButton.interactable = false;
			timer = 0;

			PlayerController.Instance.TurnOnShield(45f);
			
		}


	}