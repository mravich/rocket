using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FbScript : MonoBehaviour {


	//FACEBOOK
	public GameObject DialogLoggedOut;
	public GameObject DialogLoggedIn;
	public GameObject FacebookUserName;
	public GameObject loginButton;
	public GameObject FacebookProfileImage;

	void Awake(){

		FacebookManager.Instance.initFB ();
		DealWithFbMenus (FB.IsLoggedIn);
	}



	public void FBlogin(){

		if (FB.IsLoggedIn) {
		
			SceneManager.LoadScene ("Profile");

		} else {
			List<string> permissions = new List<string> ();
			permissions.Add ("public_profile");
			// HERE WE LOGIN WITH PERMISSIONS AND AFTER LOGGED IN GO TO AuthCallback
			FB.LogInWithReadPermissions (permissions, AuthCallback);
		}

	}

	void AuthCallback(IResult result){

		if (result.Error != null) {
		
			Debug.Log (result.Error);

		} else {
			if (FB.IsLoggedIn) {
				FacebookManager.Instance.isLoggedIn = true;
				FacebookManager.Instance.getProfile ();
				Debug.Log ("Facebook is logged in");
			} else {
				Debug.Log ("Facebook is not logged in");
			}

			DealWithFbMenus (FB.IsLoggedIn);
		}

	}


	void DealWithFbMenus(bool isLoggedIn){

		if (isLoggedIn) {
			DialogLoggedOut.SetActive (false);
			DialogLoggedIn.SetActive (true);
			if (FacebookManager.Instance.profileName != null) {
				Text userName = FacebookUserName.GetComponent<Text> ();
				userName.text = "" + FacebookManager.Instance.profileName;
			} else {

				StartCoroutine ("waitForProfileName");
			}

			if (FacebookManager.Instance.profilePicture != null) {
				Debug.Log ("Promijeni sliku");
				Image profilePic = FacebookProfileImage.GetComponent<Image> ();
				profilePic.sprite = FacebookManager.Instance.profilePicture;
			} else {

				StartCoroutine ("waitForProfilePicture");
			}


		} else {
			DialogLoggedOut.SetActive (true);
			DialogLoggedIn.SetActive (false);
		}
	}

	IEnumerator waitForProfileName(){
	
		while (FacebookManager.Instance.profileName == null) {
			yield return null;
		}
		DealWithFbMenus (FB.IsLoggedIn);
	}

	IEnumerator waitForProfilePicture(){

		while (FacebookManager.Instance.profilePicture == null) {
			yield return null;
		}
		DealWithFbMenus (FB.IsLoggedIn);
	}

	public void Share(){
	
		FacebookManager.Instance.Share ();
	}

	public void Invite(){
		FacebookManager.Instance.Invite ();
	}

	public void ShareWithUsers(){
		FacebookManager.Instance.ShareWithUsers ();
	}

}
