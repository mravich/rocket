using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour {

	public Sprite fbButton, profileButton;


	public void changeSprite(){
	
		if (gameObject.GetComponent<Image> ().sprite == fbButton) {
		
			gameObject.GetComponent<Image> ().sprite = profileButton;
		} else {
		
			gameObject.GetComponent<Image> ().sprite = fbButton;
		}

	}
}
