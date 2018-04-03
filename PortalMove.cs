using UnityEngine;
using System.Collections;

public class PortalMove : MonoBehaviour {

	public Transform Out;

	void OnTriggerEnter(Collider _col)
	{
		_col.transform.position = Out.position;
		_col.transform.rotation = Out.rotation;
	}
}


	