using UnityEngine;
using System.Collections;

public class CharSelWay : MonoBehaviour{

public GUIStyle labelStyle;

	void OnGUI(){
		GUI.Label(new Rect(10,10,50,20), "Choose the Character \n W,S,A,D" ,labelStyle);
	}
}
