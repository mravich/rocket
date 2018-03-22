using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class getImage : MonoBehaviour {


 void Awake()
{
	Image imageComponent = gameObject.GetComponent<Image>();
	imageComponent.sprite = FacebookManager.Instance.profilePicture;
}

}
