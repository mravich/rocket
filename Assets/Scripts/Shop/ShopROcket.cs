using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ShopROcket : MonoBehaviour
{


	private Vector3 _endPosition,_currentPosition;
	public bool move;
	public bool scale;
	public float scrollSpeed = 8f;
	public float scaleSpeed = 8f;

	public GameObject raketaFinal,rocket,body;

	private Renderer _mesh;
	private Vector3 _currentScale;
	private Vector3 _sideRocketScale = new Vector3(80f, 80f, 80f);
	private Vector3 _middleRocketScale = new Vector3(120f, 120f, 120f);


	void Awake()
	{
		
		raketaFinal = transform.Find("RaketaFinal").gameObject;
		rocket = raketaFinal.transform.Find("Rocket").gameObject;
		body = rocket.transform.Find("Body").gameObject;
		_mesh = body.GetComponent<Renderer>();
		if(gameObject.name =="Left" || gameObject.name =="Right")
			transform.localScale = _sideRocketScale;
		if(gameObject.name =="Middle")
			transform.localScale = _middleRocketScale;
	}


	public void setRocketBodyMaterial(Material material)
	{
		
		//print("Set :" + gameObject.name + " body material to : " + material);
		_mesh.material = material;

	}

	public void MoveLeft(string rocketName, Vector3 endPosition)
	{
		
		_endPosition = endPosition;	
		switchNamesLeft(rocketName);
		move = true;
		scale = true;
	}

	public void MoveRight(string rocketName, Vector3 endPosition)
	{
		_endPosition = endPosition;	
		switchNamesRight(rocketName);
		move = true;
		scale = true;
	}


	void switchNamesLeft(string name)
	{
		switch (name)
		{
			case "Left_Outer":
				gameObject.name = "Right_Outer";
				break;
			case "Left":
				gameObject.name = "Left_Outer";
				break;
			case "Middle":
				gameObject.name = "Left";
				break;
			case "Right":
				gameObject.name = "Middle";
				break;
			case "Right_Outer":
				gameObject.name = "Right";
				break;
		}
	}
	void switchNamesRight(string name)
	{
		switch (name)
		{
			case "Left_Outer":
				gameObject.name = "Left";
				break;
			case "Left":
				gameObject.name = "Middle";
				break;
			case "Middle":
				gameObject.name = "Right";
				break;
			case "Right":
				gameObject.name = "Right_Outer";
				break;
			case "Right_Outer":
				gameObject.name = "Left_Outer";
				break;
		}
	}

	void Update()
	{
		
		if (move)
		{
			if (transform.position == _endPosition)
			{
				move = false;
				scale = false;

			}
			else
			{

				if(gameObject.name =="Left" || gameObject.name =="Right")
				transform.localScale = Vector3.Lerp(transform.localScale,_sideRocketScale,Time.deltaTime*scaleSpeed);
				
				if(gameObject.name =="Middle")
					transform.localScale = Vector3.Lerp(transform.localScale,_middleRocketScale,Time.deltaTime*scaleSpeed);

				_currentPosition = transform.position;
				transform.position = Vector3.Lerp(_currentPosition, _endPosition, Time.deltaTime*scrollSpeed);

			}

		}

	}

	void changeScale(bool enlarge)
	{
		if (!enlarge)
		{
			 print("Scale down the rocket: " + gameObject.name);
			transform.localScale = Vector3.Lerp(_currentScale,_sideRocketScale,Time.deltaTime);
		}
		else
		{
			print("Scale up the rocket: " + gameObject.name);
		}
	}
}
