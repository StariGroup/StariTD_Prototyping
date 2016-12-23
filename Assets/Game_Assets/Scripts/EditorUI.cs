using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EditorUI : MonoBehaviour {

    public Text buildingName;
    public string appellation;

    void Start()
    {
        appellation = "BRAK";

    }

    void Update ()
    {
        buildingName.text = appellation;
	}
}
