using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItemStats : MonoBehaviour
{

	public GameObject price, power, name, shop;

	private Text priceText, powerText, nameText;
	// Use this for initialization
	void Start ()
	{
		priceText = price.GetComponent<Text>();
		powerText = power.GetComponent<Text>();
		nameText = name.GetComponent<Text>();
	}

	public void setSelectedItemInfo(Rocket item)
	{
		priceText.text = item.price.ToString();
		powerText.text = item.specialPower;
		nameText.text = item.name;
	}
}
