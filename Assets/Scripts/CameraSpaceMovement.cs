using UnityEngine;
using System.Collections;

public class CameraSpaceMovement : MonoBehaviour 
{

	public float xspeed = 1f;
	public float yspeed = 1.5f;
	public float zspeed = 2f;

	public float speedDeviation = 0;

	public float xDim = 0.3f;
	public float yDim = 0.3f;
	public float zDim = 0.3f;

	private float xPos;
	private float yPos;
	private float zPos;

	private Vector3 moveVector;
	
	void Start ()
	{
//		transform.position=Vector3(0, 0, 0);
		xPos = transform.position.x;
		yPos = transform.position.y;
		zPos = transform.position.z;

		xspeed+=(Random.Range(-1, 1)*speedDeviation);
		yspeed+=(Random.Range(-1, 1)*speedDeviation);
		zspeed+=(Random.Range(-1, 1)*speedDeviation);

	}
	
	void Update ()
	{

		moveVector.x = xspeed;
		moveVector.y = zspeed; // cus 90 degreen rotated
		moveVector.z = yspeed;

		transform.Translate(moveVector * Time.deltaTime);
//
		//transform.Translate(
		
		if(transform.position.x > (xPos +xDim))
		{
			xspeed=-(Mathf.Abs(xspeed));
		}
		
		if(transform.position.x < (xPos-xDim))
		{
			xspeed=Mathf.Abs(xspeed);
		}
		
		if(transform.position.y > (yPos+yDim))
		{
			yspeed=-(Mathf.Abs(yspeed));
		}
		
		if(transform.position.y < (yPos-yDim))
		{
			yspeed=Mathf.Abs(yspeed);
		}
		
		if(transform.position.z > (zPos+zDim))
		{
			zspeed=-(Mathf.Abs(zspeed));
		}
		
		if(transform.position.z < (zPos-zDim))
		{
			zspeed=Mathf.Abs(zspeed);
		}
	}
}

