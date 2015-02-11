using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrollableList : MonoBehaviour
{
    public GameObject itemPrefab;
	public Sprite locker;
	bool initialized = false;
	private Vector3 scrollPosition;
	public Transform contentPanel;
	public Text unlockAlbum;


    void Start()
    {
		scrollPosition = new Vector3 (0f, 0f, 0f);
		AlbumInit();
	}


	void Update()
	{

		unlockAlbum.text = string.Concat("[ ", StarLoader.unlockLevel, " Level]", "Total : ", StarLoader.albumCount, " Ea");

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
				album.border.color = new Vector4 (1f, 0.5f, 0f, 1f);
				album.starSprite.color = album.border.color;
				album.starSprite.sprite = StarLoader.stars[i].sprite;
			}
			else if(StarLoader.starAlbum[i].isEpic)
			{
				album.border.color = new Vector4 (0.66f, 0f, 1f, 1f);
				album.starSprite.color = album.border.color;
				album.starSprite.sprite = StarLoader.stars[i].sprite;
			}
			else if (StarLoader.starAlbum[i].isCommon)
			{
				album.border.color = new Vector4 (1f, 1f, 1f, 1f);
				album.starSprite.color = album.border.color;
				album.starSprite.sprite = StarLoader.stars[i].sprite;
			}
			else
			{
				album.border.color = new Vector4 (1f, 1f, 1f, 1f);
				album.starSprite.sprite = locker;
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
			
			album.starName.text = string.Concat ("[", StarLoader.stars[i].level, "] " , StarLoader.stars[i].name);
			album.starDesc.text = StarLoader.stars[i].desc;
			
			newItem.name = "star" + StarLoader.stars[i].id;
			
			if (StarLoader.starAlbum[i].isLegend)
			{
				album.border.color = new Vector4 (1f, 0.5f, 0f, 1f);
				album.starSprite.color = album.border.color;
				album.starSprite.sprite = StarLoader.stars[i].sprite;
			}
			else if(StarLoader.starAlbum[i].isEpic)
			{
				album.border.color = new Vector4 (0.66f, 0f, 1f, 1f);
				album.starSprite.color = album.border.color;
				album.starSprite.sprite = StarLoader.stars[i].sprite;
			}
			else if (StarLoader.starAlbum[i].isCommon)
			{
				album.border.color = new Vector4 (1f, 1f, 1f, 1f);
				album.starSprite.color = album.border.color;
				album.starSprite.sprite = StarLoader.stars[i].sprite;
			}
			else
			{
				album.border.color = new Vector4 (1f, 1f, 1f, 1f);
				album.starSprite.sprite = locker;
			}
			
			newItem.transform.SetParent(contentPanel);
			newItem.transform.localScale = new Vector3(1,1,1); // i donno why scale is scalable :(
		}		
	}
	

}
