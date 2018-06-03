using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUtilities {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static List<int> FillShopItemsList(List<int> shopItems, int lenght)
	{
		for (var i = 0; i < lenght; i++)
		{
			shopItems.Add(i);
		}

		return shopItems;
	}

	public static Dictionary<string, Vector3> FillStartPositionsDictionary(Dictionary<string, Vector3> dictionary)
	{
		dictionary.Add("Left_Outer", new Vector3(-8f, -0.5f, 0f));
		dictionary.Add("Left", new Vector3(-2.4f, -0.5f, 0f));
		dictionary.Add("Middle", new Vector3(0f, -0.5f, 0f));
		dictionary.Add("Right", new Vector3(2.4f, -0.5f, 0f));
		dictionary.Add("Right_Outer", new Vector3(8f, -0.5f, 0f));
		return dictionary;
	}
	
	
	public static Dictionary<string, Vector3> FillEndPositionsDictionary(Dictionary<string, Vector3> dictionary)
	{
		dictionary.Add("Left_Outer",new Vector3(-8f,-0.5f,0f));
		dictionary.Add("Left",new Vector3(-2.4f,-0.5f,0f));
		dictionary.Add("Middle",new Vector3(0f,-0.5f,0f));
		dictionary.Add("Right",new Vector3(2.4f,-0.5f,0f));
		dictionary.Add("Right_Outer",new Vector3(8f,-0.5f,0f));
		return dictionary;
	}
	
	
}
