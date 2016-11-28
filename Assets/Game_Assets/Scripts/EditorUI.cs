using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EditorUI : MonoBehaviour {

    public Text buildingName;
    public string name;

    void Start()
    {
        name = "BRAK";
    }

    void Update ()
    {
            buildingName.text = name;
	}
}
