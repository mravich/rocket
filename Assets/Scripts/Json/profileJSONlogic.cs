using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


public class profileJSONlogic : MonoBehaviour {

    private string jsonProfileString;
   

	
	void Start () {

       jsonProfileString = File.ReadAllText(Application.dataPath + "/Resources/Data/profile.json");
     //   Debug.Log("There : " + jsonProfileString);
	}
	
	
	void Update () {
		
	}

    public static string LoadJSONasResource(string path)
    {
        string jsonFilePath = path.Replace(".json", "");
        TextAsset loadedJsonFile = Resources.Load<TextAsset>(jsonFilePath);
        return loadedJsonFile.text;
    }



    public static void WriteJSONasResource(string objectJson)
    {
        
        File.WriteAllText(Application.dataPath + "/Resources/Data/profile.json", objectJson);
       
    }


    //External which we dont use
    public static void WriteToJson(string path, string content)
    {
        path = Application.persistentDataPath + "/Resources/" + path;
        FileStream  stream = File.Create(path);
        byte[] contentBytes = new UTF8Encoding(true).GetBytes(content);
        stream.Write(contentBytes, 0, contentBytes.Length);
    }
}
