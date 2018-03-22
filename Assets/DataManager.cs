using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour {

	
	
	private static DataManager _instance;
	
	public static DataManager Instance{
		get{ 
			// IF THE MANAGER HASN'T BEEN INSTANTIATED BEFORE CREATE IT
			if (_instance == null) {
				GameObject dataManager = new GameObject ("DataManager");
				dataManager.AddComponent<DataManager> ();
			}

			// RETURN FacebookManager instance(Persistant through scenes)
			return _instance;
		}
	}
	
	public profileInfo profileInfo { get; set; }
	public RocketsInfo rocketsInfo { get; set; }
	
	void Awake()
	{
   
		string profileInfoFromJson = profileJSONlogic.LoadJSONasResource("Data/profile.json");
		string rocketsInfoFromJson = profileJSONlogic.LoadJSONasResource("Data/shop.json");


		profileInfo = JsonUtility.FromJson<profileInfo>(profileInfoFromJson);
		rocketsInfo = JsonUtility.FromJson<RocketsInfo>(rocketsInfoFromJson);
		_instance = this;

	}
}
