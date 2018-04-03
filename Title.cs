using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	public GUIStyle labelStyle;

	void OnGUI(){
		GUI.Label(new Rect(10,10,150,40), "3D Action Game" , labelStyle);
	}
}