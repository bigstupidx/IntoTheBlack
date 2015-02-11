using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PathologicalGames;

public class MarsSkill : MonoBehaviour 
{
		public float coolTime;
		public Button skillButton;
		public Slider coolTimeSlider;
		private float timer;
		//	public GameObject moon;
		
		public Transform moon;
		private Transform xForm;
		public GameObject shotPrefab;
		public float fireDelay;
		
		private Transform spawner;

		void Awake()
		{
			skillButton.interactable = false;
			coolTimeSlider.maxValue = coolTime;
			xForm = moon.transform;
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
			
			StartCoroutine(MoonLightShot());
		}
		
		IEnumerator MoonLightShot()
		{
			
			for (int i =0; i < 20; i++)
			{
				Vector3 realShotPos = xForm.position;
				realShotPos.y = 0;
				
				SoundManager.Instance.PlaySound(2);
				spawner = PoolManager.Pools["PlayerPool"].Spawn(shotPrefab, realShotPos, Quaternion.identity);
				
				yield return new WaitForSeconds(fireDelay);
			}
		}
	}