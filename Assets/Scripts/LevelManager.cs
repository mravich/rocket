using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour {


	public float autoLoadNextLevelAfter;

	void Start(){


		if (autoLoadNextLevelAfter == 0) {
		

		} else {
		
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);

		}
	}
	public void LoadLevel(string name){


		SceneManager.LoadScene (name);
	}

	public void QuitRequest(){
	

		Application.Quit ();
	}

	public void LoadNextLevel(){
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


	}



}
