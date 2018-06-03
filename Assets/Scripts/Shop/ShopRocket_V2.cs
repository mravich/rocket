using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;


public class ShopRocket_V2 : MonoBehaviour {
	
	
	
	private GameObject raketaFinal,rocket,body,addons;
	private Renderer _meshBody,_meshAddons;

	
	public bool move;
	public bool scale;
	
	public float scrollSpeed = 8f;
	public float scaleSpeed = 8f;
	
	private Vector3 _currentScale;
	private readonly Vector3 _sideRocketScale = new Vector3(80f, 80f, 80f);
	private readonly Vector3 _middleRocketScale = new Vector3(120f, 120f, 120f);

	public int _id,_price;
	public string _name, _specialPower;
	
	private Vector3 _endPosition,_currentPosition;
	private void Awake()
	{
		raketaFinal = transform.Find("RaketaFinal").gameObject;
		rocket = raketaFinal.transform.Find("Rocket").gameObject;
		body = rocket.transform.Find("Body").gameObject;
		addons = rocket.transform.Find("Addons").gameObject;
		_meshBody = body.GetComponent<Renderer>();
		_meshAddons = addons.GetComponent<Renderer>();
	}


	public void setLockedMaterial(List<Material> materials)
	{
		foreach (Material mat in _meshBody.materials)
		{
			mat.color = materials[0].color;
		}
		_meshAddons.material.color = materials[0].color;
	}

	private void setMaterial(String[] materials)
	{
		
		string path_1 = "Assets/Resources/Rockets/" + (_id) + "/" + materials[0] + ".mat";
		string path_2 = "Assets/Resources/Rockets/" + (_id) + "/" + materials[1] + ".mat";


		Material mat1 = AssetDatabase.LoadAssetAtPath<Material>(path_1);
		Material mat2 = AssetDatabase.LoadAssetAtPath<Material>(path_2);

		_meshBody.materials[0].color = mat1.color;
		_meshBody.materials[1].color = mat2.color;
		_meshAddons.material.color = mat2.color;
	}

	public void receiveProps(Rocket rocket)
	{
		_id = rocket._id;
		_name = rocket.name;
		setMaterial(rocket.materials);
		_price = rocket.price;
		_specialPower = rocket.specialPower;
		print("This rocket price is: " + _price + " name is: " + _name + " id is: " +_id);
	}
	
	/////////////// MOVEMENT ///////////////
	
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

				if (gameObject.name == "Left" || gameObject.name == "Right")
					transform.localScale = Vector3.Lerp(transform.localScale, _sideRocketScale, Time.deltaTime * scaleSpeed);

				if (gameObject.name == "Middle")
					transform.localScale = Vector3.Lerp(transform.localScale, _middleRocketScale, Time.deltaTime * scaleSpeed);

				_currentPosition = transform.position;
				transform.position = Vector3.Lerp(_currentPosition, _endPosition, Time.deltaTime * scrollSpeed);

			}

		}
	}
	
	void changeScale(bool enlarge)
	{
		if (!enlarge)
		{
			transform.localScale = Vector3.Lerp(_currentScale,_sideRocketScale,Time.deltaTime);
		}
		else
		{
		}
	}
}
