using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
	float deltaTime = 0.0f;
	GUIStyle style;
	Rect rect;
	int w = Screen.width, h = Screen.height;
	float msec,fps;
	string text;
	void Start(){
		
		style = new GUIStyle();
		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.fontSize = h * 2 / 100;
		style.normal.textColor = Color.black;
		style.alignment = TextAnchor.UpperLeft;
	}

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		fps = 1.0f / deltaTime;
		msec = deltaTime * 1000.0f;

		text = "Ms: " + msec.ToString("F0") + " Fps: " + fps.ToString("F0");
	}

	void OnGUI()
	{


		//text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);


	}
}