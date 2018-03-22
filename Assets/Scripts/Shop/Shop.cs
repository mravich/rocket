using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using UnityEditor;
using UnityEngine;


public class Shop : MonoBehaviour {

	public GameObject DefaultRocket;
	public List<GameObject> ShopRockets;
	public Vector3 Rotation;

	private Transform _thisTransform;
	private Vector3 _position;
	private Quaternion _rocketStartRotation;
	private Dictionary<string, Vector3> _startPositions;
	private Dictionary<string, Vector3> _endPositions;
	
	public List<Material> mainMaterialList; 

	public bool canScroll = true;
	public float scrollTime;

	public int middleRocket = 3;
	
	
	
	// Material path
	private string path;

	void Awake(){
		
		// TODO HERE GOES THE CODE FOR LOADING ROCKETS FROM JSON FILE -> THEN STORING THAT DATA INTO "SHOP_ITEMS" - we will have to change loading of assets after that 
		
		// LOAD RESOURCES
		for (int i = 1; i <= 10; i++)
		{
			path = "Assets/Resources/Rockets/" + i + "/" + i + ".mat";
			mainMaterialList.Add(AssetDatabase.LoadAssetAtPath<Material>(path));
		}
		
		
		
		
		
		 ShopRockets = new List<GameObject>();
		 _thisTransform = transform;
		 Rotation = new Vector3(180f,-90f,90f);
		 _rocketStartRotation = Quaternion.Euler(Rotation);
		_startPositions = new Dictionary<string, Vector3>();
		_endPositions = new Dictionary<string, Vector3>();

		FillDictionaryWithPositions(_startPositions);
		
		 for(int i = 0; i<=4; i++){
			ShopRockets.Add(DefaultRocket);
			ShopRockets[i].gameObject.SetActive(false);
		}
		_endPositions.Add("Left_Outer",new Vector3(-8f,0f,0f));
		_endPositions.Add("Left",new Vector3(-2.4f,0f,0f));
		_endPositions.Add("Middle",new Vector3(0f,0f,0f));
		_endPositions.Add("Right",new Vector3(2.4f,0f,0f));
		_endPositions.Add("Right_Outer",new Vector3(8f,0f,0f));

		_position = transform.position;
		spawnRockets(5);

	}

	
	// Use this for initialization
	void Start ()
	{

		String test = JsonUtility.ToJson(DataManager.Instance.profileInfo);
		print(test);
		
		 profileInfo myObject = JsonUtility.FromJson<profileInfo>(test);
		print(myObject.name);
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		scrollTime += Time.deltaTime;
		if (scrollTime >= 0.8f)
		{
			canScroll = true;
			scrollTime = 0f;
		}
		if (canScroll)
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				ScrollShopItems(0);
				canScroll = false;

			}
			else if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				ScrollShopItems(1);
				canScroll = false;

			}

		}

		
		
	}

	void spawnRockets(int ammount){
		for(int i = 0;i<ammount;i++){
			GameObject shopRocket = Instantiate(ShopRockets[i],_position,_rocketStartRotation) as GameObject;
			shopRocket.transform.SetParent(_thisTransform);
			switch(i){
				case 0:
					shopRocket.name = "Left_Outer";
					shopRocket.transform.position = _startPositions["Left_Outer"];
					shopRocket.SetActive(true);
					break;
				case 1:
					shopRocket.name = "Left";
					shopRocket.transform.position = _startPositions["Left"];

					shopRocket.SetActive(true);
					break;
				case 2:
					shopRocket.name = "Middle";
					shopRocket.transform.position = _startPositions["Middle"];

					shopRocket.SetActive(true);
					break;
				case 3:
					shopRocket.name = "Right";
					shopRocket.transform.position = _startPositions["Right"];

					shopRocket.SetActive(true);
					break;
				case 4:
					shopRocket.name = "Right_Outer";
					shopRocket.transform.position = _startPositions["Right_Outer"];

					shopRocket.SetActive(true);
					break;

			}
		}
		sortShopRockets(middleRocket);

	}

	void FillDictionaryWithPositions(Dictionary<string, Vector3> dictionary)
	{
		dictionary.Add("Left_Outer", new Vector3(-8f,0f,0f));
		dictionary.Add("Left", new Vector3(-2.4f,0f,0f));
		dictionary.Add("Middle", new Vector3(0f,0f,0f));
		dictionary.Add("Right", new Vector3(2.4f,0f,0f));
		dictionary.Add("Right_Outer", new Vector3(8f,0f,0f));

	}

	void ScrollShopItems(int direction)
	{
		switch (direction)
		{
			case 0 :
				Debug.Log("Scroll Left");
				foreach (Transform child in transform)
				{
					String rocketName = child.name;
					switch (rocketName)
					{
						case "Left_Outer":
							child.transform.localScale = new Vector3(0f, 0f, 0f);
							//child.transform.position = _endPositions["Right_Outer"];
							child.gameObject.GetComponent<ShopROcket>().MoveLeft(rocketName,_endPositions["Right_Outer"]);
							//child.name = "Right_Outer";
							break;
						case "Left":
							child.gameObject.GetComponent<ShopROcket>().MoveLeft(rocketName,_endPositions["Left_Outer"]);
							//child.name = "Left_Outer";
							break;
						case "Middle":
							child.gameObject.GetComponent<ShopROcket>().MoveLeft(rocketName,_endPositions["Left"]);
							//child.name = "Left";
							break;
						case "Right":
							child.gameObject.GetComponent<ShopROcket>().MoveLeft(rocketName,_endPositions["Middle"]);
							//child.name = "Middle";
							break;
						case "Right_Outer":
							child.transform.localScale = new Vector3(100f, 100f, 100f);
							child.gameObject.GetComponent<ShopROcket>().MoveLeft(rocketName,_endPositions["Right"]);
							//child.name = "Right";
							break;
					}
				}
				break;
			case 1 : 
				Debug.Log(("Scroll Right"));
				foreach (Transform child in transform)
				{
					String rocketName = child.name;
					switch (rocketName)
					{
						case "Left_Outer":
							child.transform.localScale = new Vector3(100f,100f, 100f);
							//child.transform.position = _endPositions["Right_Outer"];
							child.gameObject.GetComponent<ShopROcket>().MoveRight(rocketName,_endPositions["Left"]);
							//child.name = "Right_Outer";
							break;
						case "Left":
							child.gameObject.GetComponent<ShopROcket>().MoveRight(rocketName,_endPositions["Middle"]);
							//child.name = "Left_Outer";
							break;
						case "Middle":
							child.gameObject.GetComponent<ShopROcket>().MoveRight(rocketName,_endPositions["Right"]);
							//child.name = "Left";
							break;
						case "Right":
							child.gameObject.GetComponent<ShopROcket>().MoveRight(rocketName,_endPositions["Right_Outer"]);
							//child.name = "Middle";
							break;
						case "Right_Outer":
							child.transform.localScale = new Vector3(0f, 0f, 0f);
							child.gameObject.GetComponent<ShopROcket>().MoveRight(rocketName,_endPositions["Left_Outer"]);
							//child.name = "Right";
							break;
					}
				}
				break;
		}
		
	}



	void sortShopRockets(int centerRocket)
	{
		foreach (Transform child in transform)
		{
			switch (child.name)
			{
				case "Left_Outer":
					child.gameObject.GetComponent<ShopROcket>().setRocketBodyMaterial(mainMaterialList[centerRocket - 3]);
					break;
				case "Left":
					child.gameObject.GetComponent<ShopROcket>().setRocketBodyMaterial(mainMaterialList[centerRocket - 2]);
					break;
				case "Middle":
					child.gameObject.GetComponent<ShopROcket>().setRocketBodyMaterial(mainMaterialList[centerRocket - 1]);
					break;
				case "Right":
					child.gameObject.GetComponent<ShopROcket>().setRocketBodyMaterial(mainMaterialList[centerRocket]);
					break;
				case "Right_Outer":
					child.gameObject.GetComponent<ShopROcket>().setRocketBodyMaterial(mainMaterialList[centerRocket + 1]);
					break;
			}
		}
	}
}
