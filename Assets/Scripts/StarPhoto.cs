using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarPhoto : MonoBehaviour {
	public GameObject itemPrefab;
	public Sprite[] locker;
	bool initialized = false;
	private Vector3 scrollPosition;
	public Transform contentPanel;
	public Text unlockAlbum;
	public GameObject bigPhoto;
	public Text starBigName;
	public Text starBigStat;
	public Text starBigDesc;
	public Image starBigSprite;


//	private int currentId;

	void Start()
	{
		scrollPosition = new Vector3 (0f, -810f, 0f);
		AlbumInit();
	}
	
	public void OpenBigPhoto(int _id)
	{
		BigPhoto script = bigPhoto.GetComponent<BigPhoto>();

		script.currentId = _id;

		bigPhoto.SetActive(true);
//
//
//		string swingTime = "";
//		string type;
//		string swingSpeed;
//
//		switch(StarLoader.stars[_id].level)
//		{
//		case 1 :
//			swingTime = " 스윙 타임 : 10 초 ";
//			break;
//		case 2 :
//			swingTime = " 스윙 타임 : 20 초 ";
//			break;
//		case 3 :
//			swingTime = " 스윙 타임 : 30 초 ";
//			break;
//		case 4 :
//			swingTime = " 스윙 타임 : 40 초 ";
//			break;
//		case 5 :
//			swingTime = " 스윙 타임 : 60 초 ";
//			break;
//		}
//
//
//		if(StarLoader.starAlbum[_id].isLegend)
//		{
//			starBigSprite.color = new Vector4 (1f, 0.5f, 0f, 1f);
//			starBigName.color = starBigSprite.color;
//			type = "[전설]";
//			swingSpeed = " 스윙 속도 :  3 배,";
//
//		}
//		else if(StarLoader.starAlbum[_id].isEpic)
//		{
//			starBigSprite.color = new Vector4 (0.66f, 0f, 1f, 1f);
//			starBigName.color = starBigSprite.color;
//			type = "[영웅]";
//			swingSpeed = " 스윙 속도 :  2 배,";
//
//		}
//		else
//		{
//			starBigSprite.color = new Vector4 (1f, 1f, 1f, 1f);
//			starBigName.color = starBigSprite.color;
//			type = "[일반]";
//			swingSpeed = "스윙 속도 :  1 배,";
//		}
//		
//
//		starBigSprite.overrideSprite = StarLoader.stars[_id].sprite;
//
//		starBigName.text = StarLoader.stars[_id].name;
//
//		starBigStat.color = starBigName.color;
//		starBigStat.text = string.Concat(type, "[", StarLoader.stars[_id].level, "레벨] ", swingSpeed, swingTime);
//
//		starBigDesc.text = StarLoader.stars[_id].desc;
//
//		currentId = _id;
//
//		Debug.Log (_id);
	}
	public void CloseBigPhoto()
	{
		bigPhoto.SetActive(false);
	}


	void Update()
	{
		
		unlockAlbum.text = string.Concat("Total : ", StarLoader.albumCount, " / ", StarLoader.stars.Length);
		
		if (!initialized)
		{
			RefreshAlbum();
		}
	}
	void RefreshAlbum()
	{
		initialized = true;
		
		Debug.Log ("Refresh");
		
		string name;
		for (int i =0; i < StarLoader.starAlbum.Length; i++)
		{
			name = string.Concat("star", i);
			GameObject albumItem = GameObject.Find (name);
			SampleAlbum album = albumItem.GetComponent<SampleAlbum>();


			if (StarLoader.starAlbum[i].isLegend)
			{
				//album.border.color = new Vector4 (1f, 0.5f, 0f, 1f);
				album.starSprite.color = new Vector4 (1f, 0.5f, 0f, 1f);
				album.starSprite.sprite = StarLoader.stars[i].sprite;
				album.button.GetComponent<StarPhotoButton>().unlocked = true;
			}
			else if(StarLoader.starAlbum[i].isEpic)
			{
				//album.border.color = new Vector4 (0.66f, 0f, 1f, 1f);
				album.starSprite.color = new Vector4 (1f, 0.11f, 0.39f, 1f);
				album.starSprite.sprite = StarLoader.stars[i].sprite;
				album.button.GetComponent<StarPhotoButton>().unlocked = true;
			}
			else if (StarLoader.starAlbum[i].isCommon)
			{
				//album.border.color = new Vector4 (1f, 1f, 1f, 1f);
				album.starSprite.color = new Vector4 (1f, 1f, 1f, 1f);
				album.starSprite.sprite = StarLoader.stars[i].sprite;
				album.button.GetComponent<StarPhotoButton>().unlocked = true;
			}
			else
			{
				//album.border.color = new Vector4 (1f, 1f, 1f, 1f);
				album.starSprite.sprite = locker[StarLoader.stars[i].level-1];
				album.button.GetComponent<StarPhotoButton>().unlocked = false;
			}

		}
	}
	
	void OnDisable ()
	{
		initialized = false;
		scrollPosition = transform.localPosition;
	}
	
	void AlbumInit()
	{
		initialized = true;
		transform.localPosition = scrollPosition;
		
		for (int i =0; i < StarLoader.starAlbum.Length; i++)
		{
			GameObject newItem = Instantiate(itemPrefab) as GameObject;
			SampleAlbum album = newItem.GetComponent<SampleAlbum>();
			
			//album.starName.text = string.Concat ("[", StarLoader.stars[i].level, "] " , StarLoader.stars[i].name);
			//album.starDesc.text = StarLoader.stars[i].desc;
			
			newItem.name = "star" + StarLoader.stars[i].id;
			album.button.GetComponent<StarPhotoButton>().id = StarLoader.stars[i].id;

			if (StarLoader.starAlbum[i].isLegend)
			{
//				album.border.color = new Vector4 (1f, 0.5f, 0f, 1f);
				album.starSprite.color = new Vector4 (1f, 0.5f, 0f, 1f);
				album.starSprite.sprite = StarLoader.stars[i].sprite;
				album.button.GetComponent<StarPhotoButton>().unlocked = true;
			}
			else if(StarLoader.starAlbum[i].isEpic)
			{
//				album.border.color = new Vector4 (0.66f, 0f, 1f, 1f);
				album.starSprite.color = new Vector4 (1f, 0.11f, 0.39f, 1f);
				album.starSprite.sprite = StarLoader.stars[i].sprite;
				album.button.GetComponent<StarPhotoButton>().unlocked = true;
			}
			else if (StarLoader.starAlbum[i].isCommon)
			{
//				album.border.color = new Vector4 (1f, 1f, 1f, 1f);
				album.starSprite.color = new Vector4 (1f, 1f, 1f, 1f);
				album.starSprite.sprite = StarLoader.stars[i].sprite;
				album.button.GetComponent<StarPhotoButton>().unlocked = true;
			}
			else
			{
//				album.border.color = new Vector4 (1f, 1f, 1f, 1f);
				album.starSprite.sprite = locker[StarLoader.stars[i].level-1];
				album.button.GetComponent<StarPhotoButton>().unlocked = false;
			}


			
			newItem.transform.SetParent(contentPanel);
			newItem.transform.localScale = new Vector3(1,1,1); // i donno why scale is scalable :(
		}		
	}
	
	
}