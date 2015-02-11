using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class BigPhoto : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	public Image leftStarSprite;
	public Image rightStarSprite;
	public Image starSprite;
	public Text starName;
	public Text starStat;
	public Text starDesc;
	public int currentId;
	public int previousId;
	public int nextId;
	public GameObject panel;
	public ScrollRect scroll;

	bool isLeft;
	bool isCenter;
	bool isRight;

	public float snapSpeed;

	void OnEnable()
	{
		OpenBigPhoto();
	}

	void Update()
	{
		if (isLeft)
		{
			scroll.horizontalNormalizedPosition -= 0.04f;

			if (scroll.horizontalNormalizedPosition < 0)
			{
				scroll.horizontalNormalizedPosition = 0;
				isLeft = false;

				if(previousId != -1)
				{
					currentId = previousId;
					OpenBigPhoto();
				}
			}
		}

		if (isCenter)
		{
			if (scroll.horizontalNormalizedPosition > 0.5f)
			{
				scroll.horizontalNormalizedPosition -= 0.04f;
			}
			else
			{
				scroll.horizontalNormalizedPosition += 0.04f;
			}

			if (scroll.horizontalNormalizedPosition > 0.46f && scroll.horizontalNormalizedPosition < 0.54f)
			{
				scroll.horizontalNormalizedPosition = 0.5f;
				isCenter = false;
			}

		}

		if (isRight)
		{
			scroll.horizontalNormalizedPosition += 0.04f;
			
			if (scroll.horizontalNormalizedPosition > 1f)
			{
				scroll.horizontalNormalizedPosition = 1f;
				isRight = false;

				if(nextId != -1)
				{
					currentId = nextId;
					OpenBigPhoto();
				}

			}
		}

	}


	public void OnBeginDrag (PointerEventData eventData)
	{

	}

	public void OnDrag(PointerEventData eventData)
	{

	}

	public void OnEndDrag(PointerEventData eventData)
	{

		if (previousId != -1 && scroll.horizontalNormalizedPosition < 0.35f)
		{
			isLeft = true;
		}
		else if (scroll.horizontalNormalizedPosition < 0.65f)
		{
			isCenter = true;
		}
		else if (nextId != -1)
		{
			isRight = true;
		}
		else
		{
			isCenter = true;
		}
	}


	public void OpenBigPhoto()
	{
		scroll.horizontalNormalizedPosition = 0.5f;
//		string swingTime = "";
//		string type;
//		string swingSpeed;
		
//		switch(StarLoader.stars[currentId].level)
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
		
		
		if(StarLoader.starAlbum[currentId].isLegend)
		{
			starSprite.color = new Vector4 (1f, 0.5f, 0f, 1f);
			starName.color = starSprite.color;
//			type = "[전설]";
//			swingSpeed = " 스윙 속도 :  3 배,";
			
		}
		else if(StarLoader.starAlbum[currentId].isEpic)
		{
			starSprite.color = new Vector4 (1f, 0.11f, 0.39f, 0.3f);
			starName.color = starSprite.color;
//			type = "[영웅]";
//			swingSpeed = " 스윙 속도 :  2 배,";
			
		}
		else
		{
			starSprite.color = new Vector4 (1f, 1f, 1f, 1f);
			starName.color = starSprite.color;
//			type = "[일반]";
//			swingSpeed = "스윙 속도 :  1 배,";
		}
		
		
		starSprite.overrideSprite = StarLoader.stars[currentId].sprite;

		// set left sprite
		for (int i = currentId; i >= 0; i--)
		{
			if (StarLoader.starAlbum[i].isCommon == true && i != currentId)
			{
				leftStarSprite.overrideSprite = StarLoader.stars[i].sprite;
				previousId = i;
				break;
			}
			previousId = -1; // not exist
		}

		if (previousId == -1)
			leftStarSprite.overrideSprite = null;

		for (int i = currentId; i < StarLoader.stars.Length; i++)
		{
			if (StarLoader.starAlbum[i].isCommon == true  && i != currentId)
			{
				rightStarSprite.overrideSprite = StarLoader.stars[i].sprite;
				nextId = i;
				break;
			}
			nextId = -1; // not exist;
		}

		if (nextId == -1)
			rightStarSprite.overrideSprite = null;

		starName.text = StarLoader.stars[currentId].name;
		
//		starStat.color = starName.color;
//		starStat.text = string.Concat(type, "[", StarLoader.stars[currentId].level, "레벨] ", swingSpeed, swingTime);
		
		starDesc.text = StarLoader.stars[currentId].desc;

	}

}

