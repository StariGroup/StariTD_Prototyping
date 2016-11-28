using UnityEngine;
using System.Collections;

public class RemoveGameObject : MonoBehaviour {

    void RemoveObject()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }
	
	
}
