using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using System;

public class FacebookManager : MonoBehaviour {

	private static FacebookManager _instance;


	public static FacebookManager Instance{
		get{ 
			// IF THE MANAGER HASN'T BEEN INSTANTIATED BEFORE CREATE IT
			if (_instance == null) {
				GameObject fbm = new GameObject ("FbManager");
				fbm.AddComponent<FacebookManager> ();
			}

			// RETURN FacebookManager instance(Persistant through scenes)
			return _instance;
		}
	}

	public bool isLoggedIn{ get; set; }
	public string profileName{ get; set; }
	public Sprite profilePicture{ get; set; }
	public string AppLinkURL{ get; set; }

	void Awake(){
		DontDestroyOnLoad (this.gameObject);
		_instance = this;
		isLoggedIn = true;
	}
		
	public void initFB(){
		if (!FB.IsInitialized) {
			//INITIALIZE FACEBOOK 
			FB.Init (SetInit, OnHideUnity);
		} else {
			isLoggedIn = FB.IsLoggedIn;
		}
	}

	void SetInit(){
		// CHECK TO SEE IF USER IS ALREADY LOGGED IN
		if (FB.IsLoggedIn) {	
			Debug.Log ("FB is logged in");
			getProfile ();
		} else {
			Debug.Log ("FB is not logged in");
		}
		isLoggedIn = FB.IsLoggedIn;
	}

	void OnHideUnity(bool isGameShown){
		// PAUSE THE GAME IF UNITY IS HIDDEN
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}

	}

	public void getProfile(){
		// GET USERNAME 
		FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUserName);
		// GET PICTURE
		FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayUserPicture);
		// GET APPLINK
		FB.GetAppLink(DealWithAppLink);

	}

	void DisplayUserName(IResult result){
		if (result.Error == null) {
			if (result.ResultDictionary ["first_name"] == null) {
				return;
			} else {
				profileName = result.ResultDictionary ["first_name"] + "";
				//loginButton.GetComponent<ChangeSprite> ().changeSprite ();
			}
		} else {

			Debug.Log (result.Error);
		}
	}

 void DisplayUserPicture(IGraphResult result){

		if (result.Error == null) {

			if (result.Texture != null) {
				profilePicture = Sprite.Create (result.Texture, new Rect (0, 0, 128 , 128), new Vector2 ());
			}
		}
	}

	void DealWithAppLink(IAppLinkResult result){
	
		if (!String.IsNullOrEmpty (result.Url)) {
			AppLinkURL = "" + result.Url + "";
		} else {
		
			AppLinkURL = "http://google.com";
		}
	}

	public void Share(){
	
		FB.FeedShare (
		
			string.Empty,
			new Uri(AppLinkURL),
			"Hello this is the title",
			"This is the caption",
			"Check out this game",
			new Uri("https://www.google.hr/search?q=cat&source=lnms&tbm=isch&sa=X&ved=0ahUKEwjP1ev3sOLZAhUIJpoKHaqLB_QQ_AUICigB&biw=950&bih=929#imgrc=Mp6U7RbYNomHIM:"),
			string.Empty,
			ShareCallback
		);
	}

	void ShareCallback(IResult result){
	
		// HERE WE CAN GIVE OUT REWARDS FOR SHARING 
		if (result.Cancelled) {
		
			Debug.Log ("Share cancelled");
		} else if (!string.IsNullOrEmpty (result.Error)) {
		
			Debug.Log ("Error on share");
		}else if (!string.IsNullOrEmpty (result.RawResult)){
			Debug.Log ("Success share");
		}
	}

	public void Invite(){
	
		FB.Mobile.AppInvite (
		
			new Uri(AppLinkURL),
			new Uri("https://www.google.hr/search?q=cat&source=lnms&tbm=isch&sa=X&ved=0ahUKEwjP1ev3sOLZAhUIJpoKHaqLB_QQ_AUICigB&biw=950&bih=929#imgrc=Mp6U7RbYNomHIM:"),
			InviteCallback
		);
	}

	void InviteCallback(IResult result){
		// HERE WE CAN GIVE OUT REWARDS FOR SHARING 
		if (result.Cancelled) {

			Debug.Log ("Invite cancelled");
		} else if (!string.IsNullOrEmpty (result.Error)) {

			Debug.Log ("Error on invite");
		}else if (!string.IsNullOrEmpty (result.RawResult)){
			Debug.Log ("Success invite");
		}

	}

	public void ShareWithUsers(){
		FB.AppRequest(
			"Come and beat my score",
			null,
			new List<object> () {"app_users"},
			null,
			null,
			null,
			null,
			ShareWithUsersCallback
		);
	}

		void ShareWithUsersCallback(IAppRequestResult result){
		// HERE WE CAN GIVE OUT REWARDS FOR SHARING 
		if (result.Cancelled) {

			Debug.Log ("Challenge cancelled");
		} else if (!string.IsNullOrEmpty (result.Error)) {

			Debug.Log ("Error on challenge");
		}else if (!string.IsNullOrEmpty (result.RawResult)){
			Debug.Log ("Success challenge");
		}

		}
}
