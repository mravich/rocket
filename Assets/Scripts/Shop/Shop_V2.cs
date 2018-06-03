using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Shop_V2 : MonoBehaviour
{



	// PUBLIC OBJECTS USED TO IMPORT COMPONENTS FROM THEM
	public GameObject profileNameText, profileCoinsText, currentItem, currentItemSmall;

	public GameObject selectedItemInfo;
	// PRIVATE COMPONENTS 
	public GameObject DefaultRocket;
	public List<GameObject> ShopRockets;
	private Dictionary<string, Vector3> _startPositions;
	private Dictionary<string, Vector3> _endPositions;

	private Quaternion _rocketStartRotation;

	public Vector3 Rotation;

	// main profile data 
	private profileInfo profile;
	private ShopData rockets;

	private Text profileName, profileCoins, currentItemName, currentItemSmallName;

	private string currentRocket;
	public GameObject currentRocketModel;

	private int currentRocketId;
	// LOAD ALL ROCKETS FROM shop.json and store their properties into something?what


	// LOAD ALL MATERIALS FROM resources/rockets/ by the requirement from shopData
	public Dictionary<int, List<string>> _requiredMaterials;

	public Dictionary<int, List<Material>> loadedRequiredMaterials;

	private List<Material> _lockedRocketMaterials;
	// PREPARE ALL ROCKETS AS GAMEOBJECTS and call public void setRocketProps()


	// Scroll variables
	public bool canScroll = true;
	public float scrollTime;
	private int scrollCounter;
	public int middleRocket;

	private void Awake()
	{
		// Get profile info from datamanager instance
		profile = DataManager.Instance.profileInfo;
		rockets = DataManager.Instance.shopData;
		// GET ALL COMPONENTS NEEDED
		profileName = profileNameText.GetComponent<Text>();
		profileCoins = profileCoinsText.GetComponent<Text>();
		currentItemName = currentItem.GetComponent<Text>();
		currentItemSmallName = currentItemSmall.GetComponent<Text>();
		// GETTING THE NAME OF THE CURRENT ROCKET
		currentRocket = profile.currentRocket;

		// GET THE ID OF THE CURRENT ROCKET
		currentRocketId = getRocketId(currentRocket);
		

		// LOADING LOCKED ROCKETS MATERIALS
		loadLockedMaterials();
		// LOADING ALL REQUIRED MATERIALS
		loadAllMaterialsForShopRockets();

		// FILL start positions dictionary
		fillStartPostionsDictionary();

		//FILL end positions dictionary
		fillEndPostionsDictionary();
		// SPAWN ANY NUMBER OF ROCKETS - need to add more start positions if you want more rockets
		spawnShopRockets(5);

		// SET LOCKED MATERIAL TO ALL ROCKETS
		setLockedMaterialForAllRockets();

		// SET MATERIAL FOR MIDDLE CURRENT ROCKET

	}


	private void Start()
	{

		// Set values of text components
		profileName.text = "Welcome: " + profile.name;
		profileCoins.text = profile.coins.ToString();
		
		// Sort out names of current rocket for the  player and for the shop
		GameObject defaultRocket = Instantiate(currentRocketModel);
		defaultRocket.GetComponent<ShopRocket_V2>().receiveProps(rockets.rockets[currentRocketId-1]);
		currentItemName.text = currentItemSmallName.text = rockets.rockets[currentRocketId-1].name;
		selectedItemInfo.GetComponent<SelectedItemStats>().setSelectedItemInfo(rockets.rockets[currentRocketId-1]);
		// Sort shop
		sortShopRockets(currentRocketId);
		middleRocket = currentRocketId;
		setInitialShopRocketMaterials();
		
		// Scroll
	}


	void Update()
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
				middleRocket--;
				selectedItemInfo.GetComponent<SelectedItemStats>().setSelectedItemInfo(rockets.rockets[middleRocket]);


			}
			else if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				ScrollShopItems(1);
				canScroll = false;
				middleRocket++;
				selectedItemInfo.GetComponent<SelectedItemStats>().setSelectedItemInfo(rockets.rockets[middleRocket]);

			}

		}
	}



	//////////////////// HELPER FUNCTIONS /////////////////////

	// get material for one rocket from _requiredMaterials print(_requiredMaterials[currentRocketId][0]);


	// Sort rockets with the currentRocket in the middle
	void sortShopRockets(int currentRocketId)
	{
		//print("Put this rocket with this id in the middle : " + currentRocketId);
		//print("These are the materials for the middle rocket : " + _requiredMaterials[currentRocketId][0] + " and " +
		     // _requiredMaterials[currentRocketId][1]);
	}



	void spawnShopRockets(int ammount)
	{

		ShopRockets = new List<GameObject>();
		Rotation = new Vector3(180f, -90f, 90f);
		_rocketStartRotation = Quaternion.Euler(Rotation);

		for (int i = 0; i <= 4; i++)
		{
			ShopRockets.Add(DefaultRocket);
			ShopRockets[i].gameObject.SetActive(false);
		}

		for (int i = 0; i < ammount; i++)
		{
			GameObject shopRocket = Instantiate(ShopRockets[i], transform.position, _rocketStartRotation);
			shopRocket.transform.SetParent(transform);
			switch (i)
			{
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

	}


	// Get rocket id by the provided name (string) returns int
	int getRocketId(string rocketName)
	{
		var rocketId = 0;
		for (int i = 0; i < rockets.rockets.Length; i++)
		{
			if (rockets.rockets[i].name == rocketName)
			{
				rocketId = rockets.rockets[i]._id;
			}
		}
		return rocketId;
	}

	// Make list with start positions
	void fillStartPostionsDictionary()
	{
		_startPositions = new Dictionary<string, Vector3>();
		_startPositions.Add("Left_Outer", new Vector3(-8f, -0.5f, 0f));
		_startPositions.Add("Left", new Vector3(-2.4f, -0.5f, 0f));
		_startPositions.Add("Middle", new Vector3(0f, -0.5f, 0f));
		_startPositions.Add("Right", new Vector3(2.4f, -0.5f, 0f));
		_startPositions.Add("Right_Outer", new Vector3(8f, -0.5f, 0f));
	}
	
	// Make list with end positions
	void fillEndPostionsDictionary()
	{
		_endPositions = new Dictionary<string, Vector3>();
		_endPositions.Add("Left_Outer",new Vector3(-8f,-0.5f,0f));
		_endPositions.Add("Left",new Vector3(-2.4f,-0.5f,0f));
		_endPositions.Add("Middle",new Vector3(0f,-0.5f,0f));
		_endPositions.Add("Right",new Vector3(2.4f,-0.5f,0f));
		_endPositions.Add("Right_Outer",new Vector3(8f,-0.5f,0f));
	}
	// Make list with start rotations

	// Make list with locked materials
	void loadLockedMaterials()
	{
		_lockedRocketMaterials = new List<Material>();
		_lockedRocketMaterials.Add(AssetDatabase.LoadAssetAtPath<Material>("Assets/Resources/Rockets/99/1.mat"));
		_lockedRocketMaterials.Add(AssetDatabase.LoadAssetAtPath<Material>("Assets/Resources/Rockets/99/2.mat"));
	}

	// Give all rockets locked materials
	void setLockedMaterialForAllRockets()
	{
		print("tu sam");
		foreach (Transform obj in transform)
		{
			obj.gameObject.GetComponent<ShopRocket_V2>().setLockedMaterial(_lockedRocketMaterials);
		}
	}

	// Make list with all required shop materials
	void loadAllMaterialsForShopRockets()
	{
		_requiredMaterials = new Dictionary<int, List<string>>();
		for (int i = 0; i < rockets.rockets.Length; i++)
		{
			List<string> _materials = new List<string>();
			_materials.Add(rockets.rockets[i].materials[0]);
			_materials.Add(rockets.rockets[i].materials[1]);
			_requiredMaterials.Add(rockets.rockets[i]._id, _materials);
		}

		loadRequiredMaterials();

	}

	void loadRequiredMaterials()
	{
		loadedRequiredMaterials = new Dictionary<int, List<Material>>();
		for (int i = 0; i < _requiredMaterials.Count; i++)
		{
			/*print(i + ". Material is : " + _requiredMaterials[i]);
			print("Materials for this rocket is : " + _requiredMaterials[i][0]);
			print("Materials for this rocket is : " + _requiredMaterials[i][1]);*/
			string path_1 = "Assets/Resources/Rockets/" + i + "/" + 1 + ".mat";
			string path_2 = "Assets/Resources/Rockets/" + i + "/" + 2 + ".mat";

			List<Material> loadedMaterials = new List<Material>();
			loadedMaterials.Add(AssetDatabase.LoadAssetAtPath<Material>(path_1));
			loadedMaterials.Add(AssetDatabase.LoadAssetAtPath<Material>(path_2));
			loadedRequiredMaterials.Add(i, loadedMaterials);
		}


	}

	// TODO replace currentRocketId with another variable which is by default currentRocketId at start then we just increment/decrement it based on scroll directiona
	void setInitialShopRocketMaterials()
	{
		
		transform.Find("Middle").GetComponent<ShopRocket_V2>().receiveProps(rockets.rockets[currentRocketId-1]);

		
		/*
		if (currentRocketId == 1)
		{
			transform.Find("Left_Outer").GetComponent<ShopRocket_V2>().receiveProps(rockets.rockets[rockets.rockets.Length-2]);
			transform.Find("Left").GetComponent<ShopRocket_V2>().receiveProps(rockets.rockets[rockets.rockets.Length-1]);
			transform.Find("Middle").GetComponent<ShopRocket_V2>().receiveProps(rockets.rockets[currentRocketId-1]);
			transform.Find("Right").GetComponent<ShopRocket_V2>().receiveProps(rockets.rockets[currentRocketId]);
			transform.Find("Right_Outer").GetComponent<ShopRocket_V2>().receiveProps(rockets.rockets[currentRocketId+1]);

		}else if (currentRocketId == 2)
		{
			transform.Find("Left_Outer").GetComponent<ShopRocket_V2>().receiveProps(rockets.rockets[rockets.rockets.Length-1]);
			transform.Find("Left").GetComponent<ShopRocket_V2>().receiveProps(rockets.rockets[currentRocketId-1]);
			transform.Find("Middle").GetComponent<ShopRocket_V2>().receiveProps(rockets.rockets[currentRocketId-1]);
			transform.Find("Right").GetComponent<ShopRocket_V2>().receiveProps(rockets.rockets[currentRocketId +1]);
			transform.Find("Right_Outer").GetComponent<ShopRocket_V2>().receiveProps(rockets.rockets[currentRocketId+2]);
		}*/
				

		/*// Set middle rocket material
		
		// If middle rocket id is 1 then set left & left_outer materials to 10 & 9 
		if (middleRocket == 1)
		{
	
			transform.Find("Left_Outer").GetComponent<ShopRocket_V2>().setMaterial(loadedRequiredMaterials[loadedRequiredMaterials.Count-1]);
			transform.Find("Left").GetComponent<ShopRocket_V2>().setMaterial(loadedRequiredMaterials[loadedRequiredMaterials.Count]);
		}
		
		transform.Find("Right").GetComponent<ShopRocket_V2>().setMaterial(loadedRequiredMaterials[middleRocket + 1]);
		transform.Find("Right_Outer").GetComponent<ShopRocket_V2>().setMaterial(loadedRequiredMaterials[middleRocket + 2]);
		*/


	}
	
	
	
	
	/////////////ROTATION OF THE SHOP////////////
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
							child.gameObject.GetComponent<ShopRocket_V2>().MoveLeft(rocketName,_endPositions["Right_Outer"]);
							//child.name = "Right_Outer";
							break;
						case "Left":
							child.gameObject.GetComponent<ShopRocket_V2>().MoveLeft(rocketName,_endPositions["Left_Outer"]);
							//child.name = "Left_Outer";
							break;
						case "Middle":
							child.gameObject.GetComponent<ShopRocket_V2>().MoveLeft(rocketName,_endPositions["Left"]);
							//child.name = "Left";
							break;
						case "Right":
							child.gameObject.GetComponent<ShopRocket_V2>().MoveLeft(rocketName,_endPositions["Middle"]);
							//child.name = "Middle";
							break;
						case "Right_Outer":
							child.transform.localScale = new Vector3(100f, 100f, 100f);
							child.gameObject.GetComponent<ShopRocket_V2>().MoveLeft(rocketName,_endPositions["Right"]);
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
							child.gameObject.GetComponent<ShopRocket_V2>().MoveRight(rocketName,_endPositions["Left"]);
							//child.name = "Right_Outer";
							break;
						case "Left":
							child.gameObject.GetComponent<ShopRocket_V2>().MoveRight(rocketName,_endPositions["Middle"]);
							//child.name = "Left_Outer";
							break;
						case "Middle":
							child.gameObject.GetComponent<ShopRocket_V2>().MoveRight(rocketName,_endPositions["Right"]);
							//child.name = "Left";
							break;
						case "Right":
							child.gameObject.GetComponent<ShopRocket_V2>().MoveRight(rocketName,_endPositions["Right_Outer"]);
							//child.name = "Middle";
							break;
						case "Right_Outer":
							child.transform.localScale = new Vector3(0f, 0f, 0f);
							child.gameObject.GetComponent<ShopRocket_V2>().MoveRight(rocketName,_endPositions["Left_Outer"]);
							//child.name = "Right";
							break;
					}
				}
				break;
		}
		
	}
}
