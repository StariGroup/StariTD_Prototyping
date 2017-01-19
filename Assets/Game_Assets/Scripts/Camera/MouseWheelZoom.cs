using UnityEngine;
using System.Collections;

public class MouseWheelZoom : MonoBehaviour {

	float maxHeight = 36f;
	float minHeight = 10f;

	void Update ()
	{
		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && transform.position.y > minHeight) //Rolka do góry i ograniczenie Wysokości
		{ 
			GetComponent<Transform> ().position = new Vector3 (transform.position.x, transform.position.y - 1f, transform.position.z + .5f);
			transform.Rotate (-.3f, 0, 0);
		} else {
			//nie robi nic
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && transform.position.y < maxHeight) //Rolka w dół i ograniczenie wysokości
		{ 
			GetComponent<Transform> ().position = new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z - .5f);
			transform.Rotate (.3f, 0, 0);
		} else 
		{
			//nie robi nic
		}
	}
}