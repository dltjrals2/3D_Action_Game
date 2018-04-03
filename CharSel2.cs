using UnityEngine;
using System.Collections;

public class CharSel2 : MonoBehaviour {

	public Transform Out;
	
	void OnTriggerEnter(Collider _col)
	{
		Application.LoadLevel ("GameScene2");
	}
}