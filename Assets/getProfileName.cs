using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getProfileName : MonoBehaviour {

 void Awake()
	{
		Text textComponent = gameObject.GetComponent<Text>();
		textComponent.text = FacebookManager.Instance.profileName;
	}
}
