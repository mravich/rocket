using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InfoLoader : MonoBehaviour
{

	public profileInfo profileInfo;
    public RocketsInfo rocektsInfo;
    void Awake()
    {
    
        string profileInfoFromJson = profileJSONlogic.LoadJSONasResource("Data/profile.json");
        string rocketsInfoFromJson = profileJSONlogic.LoadJSONasResource("Data/shop.json");


        profileInfo = JsonUtility.FromJson<profileInfo>(profileInfoFromJson);
        rocektsInfo = JsonUtility.FromJson<RocketsInfo>(rocketsInfoFromJson);

        Debug.Log("Profile: " + profileInfoFromJson);
        Debug.Log("Rockets: " + rocketsInfoFromJson);


    }
	// Use this for initialization
	void Start () {

        //  string saveProfileInfo =  JsonUtility.ToJson(profileInfo);
        //   profileJSONlogic.WriteToJson("Data/profile.json",saveProfileInfo);


        string json = JsonUtility.ToJson(profileInfo);
        profileJSONlogic.WriteJSONasResource(json);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
